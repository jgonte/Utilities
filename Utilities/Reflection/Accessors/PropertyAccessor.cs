using System;
using System.Reflection;
using System.Linq.Expressions;

namespace Utilities
{
    /// <summary>
    /// Represents a fast property accessor for a single property of an object
    /// </summary>
    public class PropertyAccessor
    {
        private Func<object, object> _getter;

        private Action<object, object> _setter;

        /// <summary>
        /// The name of the property to set
        /// </summary>
        public string PropertyName { get; private set; }

        /// <summary>
        /// The type of the property
        /// </summary>
        public Type PropertyType { get; set; }

        /// <summary>
        /// Whether the property type is a primitive as a business object
        /// </summary>
        public bool IsPrimitive { get; private set; }

        /// <summary>
        /// Whether the property accessor can be called to retrieve the value
        /// </summary>
        public bool CanGet { get; private set; }

        /// <summary>
        /// Whether the property accessor can be called to set the value
        /// </summary>
        public bool CanSet { get; private set; }

        public PropertyAccessor(PropertyInfo propertyInfo)
        {
            PropertyName = propertyInfo.Name;

            PropertyType = propertyInfo.PropertyType;

            IsPrimitive = PrimitiveType.IsPrimitive(propertyInfo.PropertyType);

            CreateGetter(propertyInfo);

            CreateSetter(propertyInfo);
        }

        #region Methods

        public static PropertyAccessor GetAccessor(Expression<Func<object>> property)
        {
            PropertyInfo propertyInfo = null;

            if (property.Body is MemberExpression)
            {
                propertyInfo = (property.Body as MemberExpression).Member as PropertyInfo;
            }
            else
            {
                propertyInfo = (((UnaryExpression)property.Body).Operand as MemberExpression).Member as PropertyInfo;
            }

            return new PropertyAccessor(propertyInfo);
        }

        /// <summary>
        /// Sets the property value of the object
        /// </summary>
        /// <param name="target">The object to set the property value</param>
        /// <param name="value">The value to be set</param>
        public void SetValue(object target, object value)
        {
            if (value is string && PropertyType != typeof(string)) // Needs to convert from string
            {
                var s = (string)value;

                if (PropertyType == typeof(char))
                {
                    char[] chars = s.ToCharArray();

                    if (chars.Length > 1)
                    {
                        throw new InvalidOperationException("Cannot convert to character. String has more than one character");
                    }

                    value = chars[0];
                }
                else if (PropertyType == typeof(int))
                {
                    value = int.Parse(s);
                }
                else
                {
                    throw new NotImplementedException($"{nameof(SetValue)} converting from string is not implemented for property type: '{PropertyType.Name}'");
                }
            }

            _setter(target, value);
        }

        public void SetValueAndNotifyChange(object target, object value, IPropertyChangedSuscriber suscriber)
        {
            object oldValue = _getter(target);

            if (oldValue != value)
            {
                _setter(target, value);

                suscriber.OnPropertyChanged(target, PropertyName, oldValue, value);
            }
        }

        /// <summary>
        /// Gets the property value from the object
        /// </summary>
        /// <param name="target">The object to get the property from</param>
        /// <returns>The value of the property of that object</returns>
        public object GetValue(object target)
        {
            return _getter(target);
        }

        #endregion

        #region Helpers

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyInfo"></param>
        private void CreateGetter(PropertyInfo propertyInfo)
        {
            MethodInfo getMethod = propertyInfo.GetGetMethod();

            if (!propertyInfo.CanRead || getMethod == null)
            {
                CanGet = false;

                return;
            }

            _getter = propertyInfo.EmitPropertyGetter();

            CanGet = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyInfo"></param>
        private void CreateSetter(PropertyInfo propertyInfo)
        {
            MethodInfo setMethod = propertyInfo.GetSetMethod();

            if (!propertyInfo.CanWrite || setMethod == null)
            {
                CanSet = false;

                return;
            }

            _setter = propertyInfo.EmitPropertySetter();

            CanSet = true;
        }

        #endregion
    }
}

