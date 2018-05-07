namespace Utilities
{
    /// <summary>
    /// Whether the object is ordered in a collection
    /// </summary>
    public interface IOrdered
    {
        int Order { get; set; }
    }
}