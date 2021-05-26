namespace Utilities
{
    public enum ScalarTypes
    {
        Boolean,
        Byte,
        Integer,
        UnsignedInteger,
        Long,
        Float,
        Decimal,   
        Char,
        String,
        DateTime,
        DateTimeOffset,
        Date,
        Timestamp,
        Guid,
        Image,                   
        Binary,
        Uri,
        Email,
        UserId, // Special marker type to manage persisting the id of the user to the data store
        Object
        // Value objects should belong
        //Money,
        //Enumeration,
        //Phone,
        //Signature,
        //Geography,
        //MonthYear
    }
}
