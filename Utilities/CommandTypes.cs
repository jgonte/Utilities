namespace Utilities
{
    /// <summary>
    /// CRUD command operation types that apply to an entity
    /// </summary>
    public enum EntityCommandTypes
    {
        None,
        // Single
        Create,
        Update,
        Delete,
        // Collection
        DeleteCollection,
        Activate,
        Inactivate,
        Custom,
        CustomUpdate,
        CustomDelete
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
        Relink,
        Create
    }
}