namespace Utilities
{
    /// <summary>
    /// Implemented by the objects that want to receive notifications when the value of a property of an object has changed
    /// </summary>
    public interface IPropertyChangedSuscriber
    {
        /// <summary>
        /// Notifies that the value of a property has changed
        /// </summary>
        /// <param name="o">The object whose proerty has changed</param>
        /// <param name="propertyName">The name of the property that changed</param>
        /// <param name="oldValue">The previous value of the property</param>
        /// <param name="newValue">The new value of the property</param>
        void OnPropertyChanged(object o, string propertyName, object oldValue, object newValue);
    }
}
