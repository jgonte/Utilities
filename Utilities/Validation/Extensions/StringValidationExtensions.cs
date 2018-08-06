namespace Utilities.Validation
{
    public static class StringValidationExtensions
    {
        public static void ValidateNotEmpty(this string value, ValidationResult result, string propertyName, string message = null)
        {
            if (string.IsNullOrEmpty(value))
            {
                result.Errors.Add(new ValidationError {
                    PropertyName = propertyName,
                    Message = message ?? $"Value of '{propertyName}' cannot be empty"
                });
            }
        }

        public static void ValidateMaxLength(this string value, ValidationResult result, string propertyName, uint maxLength, string message = null)
        {
            if (!string.IsNullOrEmpty(value) && value.Length > maxLength)
            {
                result.Errors.Add(new ValidationError
                {
                    PropertyName = propertyName,
                    Message = message ?? $"Value of '{propertyName}' cannot exceed '{maxLength}' characters. Length: '{value.Length}'"
                });
            }
        }

    }
}
