using System;

namespace Utilities.Validation
{
    public static class DateTimeValidationExtensions
    {
        public static void ValidateNotEmpty(this DateTime value, ValidationResult result, string propertyName, string message = null)
        {
            if (value == DateTime.MinValue)
            {
                result.Errors.Add(new ValidationError
                {
                    PropertyName = propertyName,
                    Message = message ?? $"Value of '{propertyName}' cannot be empty"
                });
            }
        }
    }
}
