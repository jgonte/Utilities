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
        UserId, // Special marker type to manage persisting the id of the user to the data store
        // Value objects should belong
        //Money,
        //Enumeration,
        //Email,
        //Phone,
        //Signature,
        //Geography,
        //MonthYear,
        //Password
    }
}
