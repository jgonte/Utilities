namespace Utilities
{
    /// <summary>
    /// CRUD command operation types that apply to an entity
    /// </summary>
    public enum EntityCommandTypes
    {
        None,
        // Single
        Save,       // Create or update depending whether the identifier is null or not
        Create,
        Update,
        Delete,
        // Collection
        DeleteCollection,
        Activate,
        Inactivate,
        CustomUpdate
    }

    /// <summary>
    /// Command operation types that apply to a link
    /// </summary>
    public enum LinkCommandTypes
    {
        Link,        // Link a linked entity or value object
        Delete,     // Delete linked entities or value objects
        Unlink,     // Unlink any linked entity
        Replace,
        Add,
        Relink
    }
}