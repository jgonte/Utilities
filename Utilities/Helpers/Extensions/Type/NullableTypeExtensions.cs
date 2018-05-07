using System;

namespace Utilities
{
    /// <summary>
    /// Helper to work with Nullable types
    /// </summary>
    public static class NullableTypeExtensions
    {       
        /// <summary>
        /// Whether the type is a nullable value type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsNullable(this Type type)
        {
            return (type.IsGenericType
                    && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>)));
        }

        /// <summary>
        /// Returns the type wrapped by the nullable one
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Type GetNullableType(this Type type)
        {
            return Nullable.GetUnderlyingType(type);
        }
    }
}
