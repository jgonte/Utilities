namespace Utilities.Validation
{
    public static class CharValidationExtensions
    {
        public static void ValidateNotEmpty(this char value, ValidationResult result, string propertyName, string message = null)
        {
            if (value == default(char))
            {
                result.Errors.Add(new ValidationError {
                    PropertyName = propertyName,
                    Message = message ?? $"Value of '{propertyName}' cannot be empty"
                });
            }
        }

    }
}
