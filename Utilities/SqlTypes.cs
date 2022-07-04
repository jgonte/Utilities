namespace Utilities
{
    public enum SqlTypes
    {
        Char,
        Varchar,
        NVarchar,
        Boolean,
        TinyInt,
        SmallInt,
        Integer,
        Decimal,
        Numeric,
        Real,
        Float,
        Double,
        Date,
        Time,
        DateTime,
        DateTimeOffset,
        Timestamp,
        Clob,
        Blob,
        Guid,
        Money,
        UserId, // Special marker for the data type to store the identity of the user
        Binary,
        RowVersion
    }
}
