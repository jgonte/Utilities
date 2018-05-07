namespace Utilities
{
    /// <summary>
    /// Defines an object that has an identifier
    /// </summary>
    /// <typeparam name="T">The type of the identifier</typeparam>
    public interface IIdentified<T>
    {
        T Id { get; set; }
    }
}
