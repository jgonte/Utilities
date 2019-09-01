namespace Utilities
{
    public enum CommandOperations
    {
        None,
        Save, // Create or update depending whether the identifier is null or not
        Create,
        Update,
        Delete,
        DeleteCollection,
        AddLinkedEntity,
        ReplaceLinkedEntity,
        AddValueObjects
    }
}