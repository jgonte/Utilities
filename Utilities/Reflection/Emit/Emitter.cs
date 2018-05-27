using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Utilities
{
    public static class Emitter
    {
        public static Func<object, object> EmitPropertyGetter(this PropertyInfo propertyInfo)
        {
            // Create the target parameter and cast it to its type
            Type targetType = propertyInfo.DeclaringType;

            ParameterExpression target = Expression.Parameter(typeof(object), "target");

            UnaryExpression resultCast = GetPropertyGetterExpression(propertyInfo, targetType, target);

            // Create the lambda
            LambdaExpression lambda = Expression.Lambda(resultCast, target);

            return (Func<object, object>)lambda.Compile();
        }

        public static Action<object, object> EmitPropertySetter(this PropertyInfo propertyInfo)
        {
            // Create the target parameter and cast it to its type
            Type targetType = propertyInfo.DeclaringType;

            if (targetType.IsValueType) // Check if there is an interface that contains the property info
            {
                Type interfaceType = targetType.GetInterfaceForProperty(propertyInfo.Name);

                if (interfaceType != null) // Use the interface type
                {
                    targetType = interfaceType;
                    propertyInfo = interfaceType.GetProperty(propertyInfo.Name);
                }
            }

            ParameterExpression target = Expression.Parameter(typeof(object), "target");
            UnaryExpression targetCast = (!targetType.IsValueType) ? Expression.TypeAs(target, targetType) : Expression.Convert(target, targetType);

            // Create the value parameter and cast it to its type
            Type propertyType = propertyInfo.PropertyType;
            ParameterExpression value = Expression.Parameter(typeof(object), "value");
            UnaryExpression valueCast = (!propertyType.IsValueType) ? Expression.TypeAs(value, propertyType) : Expression.Convert(value, propertyType);

            // Create the setter call
            MethodCallExpression setterCall = Expression.Call(targetCast, propertyInfo.GetSetMethod(), valueCast);

            // Create the lambda
            LambdaExpression lambda = Expression.Lambda(setterCall, new ParameterExpression[] { target, value });

            return (Action<object, object>)lambda.Compile();
        }

        public static Func<object, object, List<string>> EmitObjectComparer(this Type type)
        {
            // Create the parameters
            ParameterExpression obj1 = Expression.Parameter(typeof(object), "obj1");
            ParameterExpression obj2 = Expression.Parameter(typeof(object), "obj2");

            // Create the list
            ParameterExpression modifiedProperties = Expression.Variable(typeof(List<string>), "modifiedProperties");

            // Create the list of expressions to pass to the block
            List<Expression> expressions = new List<Expression>();

            // modifiedProperties = new List<string>();
            expressions.Add(Expression.Assign(modifiedProperties, Expression.New(typeof(List<string>))));

            expressions.AddRange(GetPropertyComparerExpressions(type, obj1, obj2, modifiedProperties));

            // Return the list
            expressions.Add(modifiedProperties);

            // Create the block
            Expression block = Expression.Block(new [] { modifiedProperties }, expressions);

            // Create the lambda
            LambdaExpression lambda = Expression.Lambda(block, obj1, obj2);

            return (Func<object, object, List<string>>)lambda.Compile();
        }

        public static Func<object, object> EmitObjectCloner(this Type type)
        {
            // Create the parameters
            ParameterExpression objectToClone = Expression.Parameter(typeof(object), "objectToClone");

            // Create the cloned object
            ParameterExpression clonedObject = Expression.Variable(typeof(object), "clonedObject");

            // Create the list of expressions to pass to the block
            List<Expression> expressions = new List<Expression>();

            // clonedObject = new T();
            NewExpression newClone = Expression.New(type);
            UnaryExpression castClone = (!type.IsValueType) ? Expression.TypeAs(newClone, typeof(object)) : Expression.Convert(newClone, typeof(object));

            expressions.Add(Expression.Assign(clonedObject, castClone));

            IList<Expression> propertyExpressios =  GetPropertyCopierExpressions(type, objectToClone, clonedObject);

            if (propertyExpressios.Count == 0) // No properties to copy
            {
                return null; 
            }

            expressions.AddRange(propertyExpressios);

            // Return the cloned object
            expressions.Add(clonedObject);

            // Create the block
            Expression block = Expression.Block(new[] { clonedObject }, expressions);

            // Create the lambda
            LambdaExpression lambda = Expression.Lambda(block, objectToClone);

            return (Func<object, object>)lambda.Compile();
        }

        #region Helpers

        private static UnaryExpression GetPropertyGetterExpression(PropertyInfo propertyInfo, Type targetType, ParameterExpression target)
        {
            UnaryExpression targetCast = (!targetType.IsValueType) ? Expression.TypeAs(target, targetType) : Expression.Convert(target, targetType);

            // Create the getter call
            MethodCallExpression getterCall = Expression.Call(targetCast, propertyInfo.GetGetMethod());

            // Cast the result of the getter call to an object
            UnaryExpression resultCast = Expression.TypeAs(getterCall, typeof(object));

            return resultCast;
        }

        private static IEnumerable<Expression> GetPropertyComparerExpressions(Type type, ParameterExpression obj1, ParameterExpression obj2, ParameterExpression modifiedProperties)
        {
            List<Expression> expressions = new List<Expression>();

            new TypeTraversal
            {
                OnProperty = (traversal, t, propertyInfo) =>
                    {
                        ConditionalExpression comparer = GetPropertyComparerExpression(type, obj1, obj2, modifiedProperties, propertyInfo);
                        expressions.Add(comparer);
                    }
            }.Traverse(type);

            return expressions;
        }

        private static ConditionalExpression GetPropertyComparerExpression(Type type, ParameterExpression obj1, ParameterExpression obj2, ParameterExpression modifiedProperties, PropertyInfo propertyInfo)
        {
            // Get the values from the objects to compare
            UnaryExpression obj1Getter = GetPropertyGetterExpression(propertyInfo, type, obj1);
            UnaryExpression obj2Getter = GetPropertyGetterExpression(propertyInfo, type, obj2);

            // Compare the values
            ConditionalExpression comparer = Expression.IfThen(
                Expression.NotEqual(obj1Getter, obj2Getter), // If they are not equal
                Expression.Call(modifiedProperties,
                                typeof(List<string>).GetMethod("Add"),
                                new Expression[] { Expression.Constant(propertyInfo.Name) }) // Add the name of the property that has different values
                );

            return comparer;
        }

        private static IList<Expression> GetPropertyCopierExpressions(Type type, ParameterExpression objectToClone, ParameterExpression clonedObject)
        {
            List<Expression> expressions = new List<Expression>();

            new TypeTraversal
            {
                OnProperty = (traversal, t, propertyInfo) =>
                {
                    MethodCallExpression copier = GetPropertyCopierExpression(type, objectToClone, clonedObject, propertyInfo);

                    if (copier != null)
                    {
                        expressions.Add(copier);
                    }
                 }
            }
            .Traverse(type);

            return expressions;
        }

        private static MethodCallExpression GetPropertyCopierExpression(Type type, ParameterExpression objectToClone, ParameterExpression clonedObject, PropertyInfo propertyInfo)
        {
            if (type.IsValueType) // Check if there is an interface that contains the property info
            {
                var interfaceType = type.GetInterfaceForProperty(propertyInfo.Name);

                if (interfaceType != null) // Use the interface type
                {
                    type = interfaceType;

                    propertyInfo = interfaceType.GetProperty(propertyInfo.Name);
                }
                else // The cloning won't work
                {
                    return null;
                }
            }

            // At this point objects are not value types
            // Cast the objects to their respective types
            var objectToCloneCast = Expression.TypeAs(objectToClone, type);

            var clonedObjectCast = Expression.TypeAs(clonedObject, type);

            var setMethod = propertyInfo.GetSetMethod();

            var getMethod = propertyInfo.GetGetMethod();

            if (propertyInfo.CanRead && getMethod != null && propertyInfo.CanWrite && setMethod != null)
            {
                return Expression.Call(clonedObjectCast, setMethod, Expression.Call(objectToCloneCast, getMethod));
            }

            return null;
        }

        public static IList<ParameterExpression> GetMethodParameters(MethodInfo methodInfo)
        {
            var list = new List<ParameterExpression>();

            foreach (ParameterInfo parameterInfo in methodInfo.GetParameters())
            {
                list.Add(Expression.Parameter(parameterInfo.ParameterType, parameterInfo.Name));
            }

            return list;
        }

        public static NewArrayExpression CreateArray<T>(T[] array)
        {
            List<Expression> items = new List<Expression>();

            foreach(T item in array)
            {
                items.Add(Expression.New(typeof(T)));
            }

            return Expression.NewArrayInit(typeof(T), items);
        }

        #endregion
    }
}
