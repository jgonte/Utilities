namespace Utilities.Builders
{
    /// <summary>
    /// Builds an object
    /// </summary>
    /// <typeparam name="T">The type of the object</typeparam>
    public interface IBuilder<T>
    {
        T Build();
    }
}
