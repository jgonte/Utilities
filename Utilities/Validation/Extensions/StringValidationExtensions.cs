using System.Text.RegularExpressions;

namespace Utilities.Validation
{
    public static class StringValidationExtensions
    {
        private static Regex _emailValidationRegex = new Regex(RegularExpressions.Email, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled);

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

        public static void ValidateEmail(this string value, ValidationResult result, string propertyName, string message = null)
        {
            if (!string.IsNullOrEmpty(value) && !_emailValidationRegex.IsMatch(value))
            {
                result.Errors.Add(new ValidationError
                {
                    PropertyName = propertyName,
                    Message = message ?? $"Value of '{propertyName}' must be a valid email"
                });
            }
        }

        public static void ValidateIsEqual(
            this string value, 
            ValidationResult result, 
            string propertyName,
            string valueToCompare,
            string propertyToCompareName,
            string message = null)
        {
            if (value != valueToCompare)
            {
                result.Errors.Add(new ValidationError
                {
                    PropertyName = propertyName,
                    Message = message ?? $"Value of '{propertyName}' must be equal to value of '{propertyToCompareName}'"
                });
            }
        }

        

    }
}
