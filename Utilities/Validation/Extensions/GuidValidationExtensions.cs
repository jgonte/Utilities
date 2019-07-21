using System;

namespace Utilities.Validation
{
    public static class GuidValidationExtensions
    {
        public static void ValidateNotEmpty(this Guid value, ValidationResult result, string propertyName, string message = null)
        {
            if (value == Guid.Empty)
            {
                result.Errors.Add(new ValidationError {
                    PropertyName = propertyName,
                    Message = message ?? $"Value of '{propertyName}' cannot be empty"
                });
            }
        }

    }
}
