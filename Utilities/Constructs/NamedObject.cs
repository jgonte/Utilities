namespace Utilities
{
    /// <summary>
    /// Base class for persistable objects that have a name and a description
    /// </summary>
    public abstract class NamedObject<T> : IIdentified<T>, INamed, IDescribed
    {
        public T Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
