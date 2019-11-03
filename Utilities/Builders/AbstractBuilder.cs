namespace Utilities.Builders
{
    /// <summary>
    /// Creates an object using an abstract factory
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class AbstractBuilder<T> : IBuilder<T>
    {
        public T Build()
        {
            Validate();

            var obj = Create();

            Initialize(obj);

            return obj;
        }

        public virtual void Validate()
        {
            // Do nothing
        }

        /// <summary>
        /// The abstract factory to implement
        /// </summary>
        /// <returns></returns>
        public abstract T Create();

        public abstract void Initialize(T obj);

    }
}
