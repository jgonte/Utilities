using System;

namespace Utilities
{
    public static class ObjectFactory
    {
        /// <summary>
        /// Creates an instance of the object from the type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object CreateInstance(this Type type)
        {
            if (type == typeof(string)) 
            { 
                return string.Empty; 
            }

            if (type.IsArray) 
            { 
                return Array.CreateInstance(type.GetElementType(), 0); 
            }

            return Activator.CreateInstance(type);
        }

        public static T CreateInstance<T>(this Type type)
        {
            return (T)type.CreateInstance();
        }
    }
}
