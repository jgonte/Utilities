using System;

namespace Utilities
{
    public static class Range<T> where T : IComparable
    {
        /// <summary>
        /// Determines whether the value is in range (inclusive)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static bool IsInRange(T val, T min, T max)
        {
            return (val.CompareTo(min) >= 0 && val.CompareTo(max) <= 0);
        }
    }
}
