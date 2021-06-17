using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Collections.Concurrent;

namespace Utilities
{
    /// <summary>
    /// Extension methods for the type object
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Unwraps the "clean" type from collections and nullable types
        /// </summary>
        /// <param name="type">The type to unwrap</param>
        /// <returns></returns>
        public static Type UnwrapType(this Type type)
        {
            if (type.IsCollection())
            {
                type = type.GetItemType();
            }

            if (type.IsNullable())
            {
                type = type.GetNullableType();
            }

            return type;
        }

        /// <summary>
        /// Retrieves the interface that has a property info that matches the name of the property name
        /// </summary>
        /// <param name="type">The type to test</param>
        /// <param name="propertyName">The name of the property to find</param>
        /// <returns>The interface type if found, otherwise returns null</returns>
        public static Type GetInterfaceForProperty(this Type type, string propertyName)
        {
            return (from Type @interface in type.GetInterfaces()
                    where @interface.GetProperty(propertyName) != null
                    select @interface).FirstOrDefault();
        }

        /// <summary>
        /// Determines whether the type has an interface (generic or not) of the given name
        /// </summary>
        /// <param name="type"></param>
        /// <param name="interfaceName"></param>
        /// <returns></returns>
        public static bool HasSomeInterfaceOf(this Type type, string interfaceName)
        {
            return (from @interface in type.GetInterfaces()
                    where @interface.Name.StartsWith(interfaceName)
                    select @interface).FirstOrDefault() != null;
        }

        /// <summary>
        /// Tests whether this type has a default constructor
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool HasDefaultConstructor(this Type type)
        {
            return (type.GetConstructor(Type.EmptyTypes) != null);
        }

        /// <summary>
        /// Tests whether the generic value is null for a reference type or the default primitive value for a struct type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsDefault<T>(this T value)
        {
            if (value == null)
            {
                return true;
            }

            var defaultValue = default(T);

            var type = value.GetType();

            if (defaultValue == null && type.IsValueType) // Boxed value
            {
                if (type == typeof(int))
                {
                    return Convert.ToInt32(value) == default(int);
                }

                if (type == typeof(DateTime))
                {
                    return Convert.ToDateTime(value) == default(DateTime);
                }

                if (type == typeof(Guid))
                {
                    return Cast<Guid>(value) == Guid.Empty;
                }

                throw new NotImplementedException("Implement for other value types as needed");
            }
            else
            {
                return EqualityComparer<T>.Default.Equals(value, default(T));
            }       
        }

        private static T Cast<T>(object value)
        {
            return (T)value;
        }

        private static readonly ConcurrentDictionary<Type, object> _defaultValues = new ConcurrentDictionary<Type, object>(); // Cache the default values of the types

        /// <summary>
        /// Retrieves the default value for the type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object GetDefault(this Type type)
        {
            if (type.IsValueType)
            {
                return _defaultValues.GetOrAdd(type, Activator.CreateInstance(type));
            }

            return null;
        }

        /// <summary>
        /// Retrieves an instance of an object from a string
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object FromString(this Type type, string value)
        {
            if (type == typeof(string))
            {
                return value;
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                return type.GetDefault();
            }

            if (type.IsEnum)
            {
                return Enum.Parse(type, value);
            }

            if (type.IsNullable())
            {
                type = Nullable.GetUnderlyingType(type);
            }

            // Try the parse method
            MethodInfo parse = type.GetMethod("Parse", new Type[] { typeof(string) });

            if (parse != null) 
            { 
                return parse.Invoke(null, new object[] { value }); 
            }

            return Convert.ChangeType(value, type);
        }

        /// <summary>
        /// Returns a static property from a given type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        public static object GetStaticProperty(this Type type, string property)
        {
            object result = null;

            try
            {
                result = type.InvokeMember(property, BindingFlags.Static | BindingFlags.Public | BindingFlags.GetField | BindingFlags.GetProperty, null, type, null);
            }
            catch
            {
                return null;
            }

            return result;
        }

        /// <summary>
        /// Helper routine that looks up a type name and tries to retrieve the
        /// full type reference using GetType() and if not found looking 
        /// in the actively executing assemblies and optionally loading
        /// the specified assembly name.
        /// </summary>
        /// <param name="typeName">type to load</param>
        /// <param name="assemblyName">
        /// Optional assembly name to load from if type cannot be loaded initially. 
        /// Use for lazy loading of assemblies without taking a type dependency.
        /// </param>
        /// <returns>null</returns>
        public static Type ToType(this string typeName, string assemblyName = null)
        {
            var type = Type.GetType(typeName, false);

            if (type != null)
            {
                return type;
            }

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            type = assemblies.Select(asm => asm.GetType(typeName, false)).SingleOrDefault(t => t != null);

            if (type != null)
            {
                return type;
            }

            // see if we can load the assembly
            if (!string.IsNullOrWhiteSpace(assemblyName))
            {
                var a = Assembly.Load(assemblyName);

                if (a != null)
                {
                    type = Type.GetType(typeName, false);

                    if (type != null)
                    {
                        return type;
                    }
                }
            }

            return null;
        }
    }
}
