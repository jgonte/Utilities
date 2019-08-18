namespace Utilities.Validation
{
    public static class RangeValidationExtensions
    {
        public static void ValidateCountIsBetween(this uint value, 
            ValidationResult result, 
            uint minValue, 
            uint maxValue, 
            string propertyName, 
            string message = null)
        {
            if (value < minValue || value > maxValue)
            {
                result.Errors.Add(new ValidationError
                {
                    PropertyName = propertyName,
                    Message = message ?? $"Value of '{propertyName}' must be between: '{minValue}' and: '{maxValue}'. '{value}' was provided"
                });
            }
        }

        public static void ValidateCountIsLessThanOrEqual(this uint value,
            ValidationResult result,
            uint maxValue,
            string propertyName,
            string message = null)
        {
            if (value > maxValue)
            {
                result.Errors.Add(new ValidationError
                {
                    PropertyName = propertyName,
                    Message = message ?? $"Value of '{propertyName}' must be less than or equal to: '{maxValue}'. '{value}' was provided"
                });
            }
        }

        public static void ValidateCountIsEqualOrGreaterThan(this uint value,
            ValidationResult result,
            uint minValue,
            string propertyName,
            string message = null)
        {
            if (value < minValue)
            {
                result.Errors.Add(new ValidationError
                {
                    PropertyName = propertyName,
                    Message = message ?? $"Value of '{propertyName}' must be equal to or greater than: '{minValue}'. '{value}' was provided"
                });
            }
        }
    }
}
