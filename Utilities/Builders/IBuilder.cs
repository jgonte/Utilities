namespace Utilities.Builders
{
    public interface IBuilder
    {
        /// <summary>
        /// The parent builder of this builder
        /// </summary>
        IBuilder ParentBuilder { get; set; }
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
