using System;
using System.Collections.Generic;

namespace Utilities
{
    public static class NumericExtensions
    {        
        public static bool IsGreaterThan<T>(this T value, T other) where T : IComparable<T>
        {
            return Comparer<T>.Default.Compare(value, other) > 0;
        }

        public static bool IsGreaterThanOrEqual<T>(this T value, T other) where T : IComparable<T>
        {
            return Comparer<T>.Default.Compare(value, other) >= 0;
        }

        public static bool IsLessThan<T>(this T value, T other) where T : IComparable<T>
        {
            return Comparer<T>.Default.Compare(value, other) < 0;
        }

        public static bool IsLessThanOrEqual<T>(this T value, T other) where T : IComparable<T>
        {
            return Comparer<T>.Default.Compare(value, other) <= 0;
        }

        public static bool IsBetween<T>(this T value, T minValue, T maxValue) where T : IComparable<T>
        {
            return value.CompareTo(minValue) >= 0 && value.CompareTo(maxValue) <= 0;
        }
    }
}
