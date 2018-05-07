namespace Utilities
{
    public class RegexPatterns
    {
        public const string Email = @"^[\w\.-]+@[\w\.-]+\.[a-zA-Z]+$";
        public const string Ssn = @"^\d{3}-\d{2}-\d{4}$";
        public const string PostalCode = @"^(\d{5}(( |-)\d{4})?)|([A-Za-z]\d[A-Za-z]( |-)\d[A-Za-z]\d)$"; // USA and Canada
        public const string IpAddress = @"\b(?:\d{1,3}\.){3}\d{1,3}\b";
        public const string Guid = @"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$";
    }
}
