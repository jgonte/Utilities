using System;
using System.Linq.Expressions;

namespace Utilities
{
    public class PropertyBinding
    {
        public PropertyBinding(object o, string propertyName)
        {
            _object = o;

            _accessor = new PropertyAccessor(o.GetType().GetProperty(propertyName));
        }

        public PropertyBinding(object o, Expression<Func<object>> property)
        {
            _object = o;

            _accessor = PropertyAccessor.GetAccessor(property);
        }

        public void SetValue(object value)
        {
            _accessor.SetValue(_object, value);
        }

        public object GetValue()
        {
            return _accessor.GetValue(_object);
        }

        private object _object; // The object to be bound to
        private PropertyAccessor _accessor; // The property accessor to set and get the value from
    }
}
