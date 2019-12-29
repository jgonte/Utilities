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
        public static T? ToNullable<T>(this T o) where T : struct
        {
            return new T?(o);
        }

        /// <summary>
        /// Tests whether a nullable bool is false
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsFalse(this bool? value)
        {
            return !value.HasValue || value.Value == false;
        }

        /// <summary>
        /// Tests whether a nullable bool is true
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsTrue(this bool? value)
        {
            return value.HasValue && value.Value == true;
        }
    }
}
