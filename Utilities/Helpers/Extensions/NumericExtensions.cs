
namespace Utilities
{
    public static class NumericExtensions
    {
        public static bool IsBetween(this int number, int minValue, int maxValue)
        {
            return (number >= minValue && number <= maxValue);
        }

        public static bool IsBetween(this uint number, uint minValue, uint maxValue)
        {
            return (number >= minValue && number <= maxValue);
        }

        public static bool IsBetween(this long number, long minValue, long maxValue)
        {
            return (number >= minValue && number <= maxValue);
        }
    }
}
