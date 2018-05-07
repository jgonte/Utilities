using System;

namespace Utilities
{
    public static class NullableExtensions
    {
        /// <summary>
        /// Converts to nullable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o"></param>
        /// <returns></returns>
        public static Nullable<T> ToNullable<T>(this T o) where T : struct
        {
            return new Nullable<T>(o);
        }
    }
}
