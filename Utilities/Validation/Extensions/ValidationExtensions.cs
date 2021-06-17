namespace Utilities.Validation
{
    public static class ValidationExtensions
    {
        public static void ValidateNotNull(this object obj, ValidationResult result, string propertyName, string message = null)
        {
            if (obj == null)
            {
                result.Errors.Add(new ValidationError
                {
                    PropertyName = propertyName,
                    Message = message ?? $"Object referenced by '{propertyName}' cannot be null"
                });
            }
        }

        public static void ValidateRequired<T>(this T value, ValidationResult result, string propertyName, string message = null)
        {
            if (value.IsDefault())
            {
                result.Errors.Add(new ValidationError
                {
                    PropertyName = propertyName,
                    Message = message ?? $"Member: '{propertyName}' requires a value"
                });
            }
        }
    }
}
