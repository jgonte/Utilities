namespace Utilities.Builders
{
    public interface IBuilder
    {
    }

    /// <summary>
    /// Builds an object
    /// </summary>
    /// <typeparam name="T">The type of the object</typeparam>
    public interface IBuilder<T> : IBuilder
    {
        T Build();
    }
}
