namespace Utilities
{
    public enum ScalarTypes
    {
        Boolean,
        Byte,
        Integer,
        Long,
        Float,
        Decimal,   
        Char,
        String,
        DateTime,
        Date,
        Timestamp,
        Guid,
        Image,                   
        Binary,
        Uri,
        Email,
        UserId, // Special marker type to manage persisting the id of the user to the data store
        // Value objects should belong
        //Money,
        //Enumeration,
        //Phone,
        //Signature,
        //Geography,
        //MonthYear
    }
}
