namespace Utilities.Validation
{
    public static class IntegerValidationExtensions
    {
        public static void ValidateNotZero(this int value, ValidationResult result, string propertyName, string message = null)
        {
            if (value == 0)
            {
                result.Errors.Add(new ValidationError
                {
                    PropertyName = propertyName,
                    Message = message ?? $"Value of '{propertyName}' cannot be zero"
                });
            }
        }

    }
}
