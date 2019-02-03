namespace Utilities
{
    public enum CommandOperations
    {
        Save = 1, // Create or update depending whether the identifier is null or not
        Create = 2,
        Update = 3,
        Delete = 4,
    }
}