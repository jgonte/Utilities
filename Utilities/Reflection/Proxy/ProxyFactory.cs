using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;

namespace Utilities
{
    public class ProxyFactory
    {
        /// <summary>
        /// The type to create the proxy from
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// The 
        /// </summary>
        public IList<Interceptor> Interceptors { get; set; }

        public object CreateProxy()
        {
            if (null == Type)
            {
                throw new ArgumentNullException("Type");
            }

            string typeName = Type.FullName;

            AssemblyBuilder assemblyBuilder = DynamicBuilder.CreateAssembly(string.Format("{0}ProxyAssembly", typeName));

            ModuleBuilder moduleBuilder = assemblyBuilder.CreateModule(string.Format("{0}ProxyModule", typeName));

            TypeBuilder typeBuilder = moduleBuilder.CreateType(string.Format("{0}Proxy", typeName), parent: Type);

            foreach (Interceptor interceptor in Interceptors)
            {
                MethodInfo methodInfo = interceptor.MethodInfo;

                ParameterInfo[] parametersInfo = methodInfo.GetParameters();

                Type returnType = methodInfo.ReturnType;

                //Parameter[] parameters = new Parameter[parametersInfo.Length];
                ParameterExpression parameterInfo = Expression.Parameter(typeof(Parameter[]));


                //// Create the parameters to be passed to the lambdas
                //for (int i = 0; i < parametersInfo.Length; ++i)
                //{
                //    parameters[i] = new Parameter
                //    {
                //         Name = parametersInfo[i].Name,
                //         Value = null
                //    };
                //}

                //bool canCall = true;

                //if (interceptor.BeforeInvoke != null)
                //{
                //    canCall = interceptor.BeforeInvoke(parameters);
                //}

                //object res = null;
                //object instance = null;
                //object[] values = new object[parametersInfo.Length];

                //if (canCall == true)
                //{
                //    res = methodInfo.Invoke(instance, values);
                //}

                //if (interceptor.AfterInvoke != null)
                //{
                //    interceptor.AfterInvoke(parameters, res);
                //}

                //return res;
                
                MethodBuilder methodBuilder = typeBuilder.CreateMethod(methodInfo, attributes: MethodAttributes.Public | MethodAttributes.Static);

                // Instance
                ParameterExpression instance = Expression.Parameter(Type, "instance");

                // Parameters
                IList<ParameterExpression> parameters = Emitter.GetMethodParameters(methodInfo);

                // The base method call
                Expression call = Expression.Call(instance, methodInfo, parameters);

                if (interceptor.BeforeInvoke != null)
                {
                    //call = CreateBeforeInvokeMethodCall(interceptor.BeforeInvoke, (MethodCallExpression)call, parameters);
                }

                if (interceptor.AfterInvoke != null)
                {
                    //call = CreateAfterInvokeMethodCall(interceptor.AfterInvoke, (MethodCallExpression)call, parameters);
                }

                List<ParameterExpression> lambdaParameters = new List<ParameterExpression>
                {
                    instance
                };

                lambdaParameters.AddRange(parameters); // Add the parameters

                // Create the lambda
                LambdaExpression lambda = Expression.Lambda(call, lambdaParameters);

                // Compile the method
                //lambda.CompileToMethod(methodBuilder);  
                throw new NotImplementedException();
            }

            Type dynamicType = typeBuilder.CreateTypeInfo();

            return dynamicType.CreateInstance();
        }

        private BlockExpression CreateBeforeInvokeMethodCall(Func<object[], bool> beforeInvoke, MethodCallExpression call, IList<ParameterExpression> parameters)
        {
            // Create the parameters being passed
            ParameterExpression parameterList = Expression.Parameter(typeof(object[]), "parameters");

            // The result of the method call
            ParameterExpression canCall = Expression.Parameter(typeof(bool), "canCall");

            // The result of the method call
            ParameterExpression result = Expression.Parameter(typeof(bool), "result");

            return Expression.Block(
                new [] { canCall },
                Expression.Assign(canCall, Expression.Call(beforeInvoke.Method, parameterList)),
                Expression.IfThen(
                    Expression.Equal(canCall, Expression.Constant(true)),
                    Expression.Block(
                        new[] { result },
                        Expression.Assign(result, call)
                    )
                )
            );
        }

        /// <summary>
        /// The information about the methods to be intercepted
        /// </summary>
        public class Interceptor
        {
            /// <summary>
            /// The method to intercept
            /// </summary>
            public MethodInfo MethodInfo { get; set; }

            /// <summary>
            /// The functionality to be injected before invoking the method of the base class
            /// </summary>
            public Func<Parameter[], bool> BeforeInvoke { get; set; }

            /// <summary>
            /// The functionality to be injected after invoking the method of the base class
            /// </summary>
            public Action<Parameter[], object> AfterInvoke { get; set; }
        }

        public class Parameter
        {
            public string Name { get; set; }

            public object Value { get; set; }
        }
    }
}
