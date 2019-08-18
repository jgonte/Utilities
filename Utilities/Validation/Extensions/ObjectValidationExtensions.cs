namespace Utilities.Validation
{
    public static class ObjectValidationExtensions
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
    }
}
