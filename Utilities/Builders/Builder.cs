namespace Utilities.Builders
{
    /// <summary>
    /// A builder with a concrete default factory
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Builder<T> : AbstractBuilder<T>
        where T : new()
    {
        /// <summary>
        /// The default factory
        /// </summary>
        /// <returns></returns>
        public override T Create()
        {
            return new T();
        }
    }
}
