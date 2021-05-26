using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Utilities
{
    /// <summary>
    /// Provides a fast access to the properties of a type whenever possible
    /// </summary>
    public class TypeAccessor
    {
        #region Constructors

        internal TypeAccessor(Type type, BindingFlags propertiesBinding)
        {
            Type = type;

            // Create the property accessors
            PropertyAccessors = new Dictionary<string, PropertyAccessor>();

            new TypeTraversal
            {
                OnProperty = (trav, t, propertyInfo) =>
                {
                    PropertyAccessors.Add(propertyInfo.Name, new PropertyAccessor(propertyInfo));
                }
            }.Traverse(type, propertiesBinding: propertiesBinding);

            // Create the object comparer
            _objectComparer = Emitter.EmitObjectComparer(type);

            // Create the object cloner
            if (type.HasDefaultConstructor())
            {
                _objectCloner = Emitter.EmitObjectCloner(type);
            }
        } 

        #endregion

        public Type Type { get; private set; }

        #region Methods

        /// <summary>
        /// Testst whether the type accessor has the property name
        /// </summary>
        /// <param name="propertyName">The name of the property to test for</param>
        /// <returns>true if the type accessor contains the property, false otherwise</returns>
        public bool Contains(string propertyName)
        {
            return PropertyAccessors.ContainsKey(propertyName);
        }

        /// <summary>
        /// Sets the value to the property of the target object
        /// </summary>
        /// <param name="target">The object to set the property</param>
        /// <param name="propertyName">The name of the property to set</param>
        /// <param name="value">The value to set</param>
        public void SetValue(object target, string propertyName, object value)
        {
            if (propertyName.Contains(".")) // Support nested properties
            {
                var propertyNames = propertyName.Split('.');

                var typeAccessor = this; // The type accessor needs to change for each new target

                foreach (var name in propertyNames)
                {
                    if (!typeAccessor.PropertyAccessors.ContainsKey(name))
                    {
                        throw new InvalidOperationException($"Property: '{name}' not found for object of type: '{target.GetType().FullName}'");
                    }

                    var propertyAccessor = typeAccessor.PropertyAccessors[name];

                    if (!propertyAccessor.CanSet)
                    {
                        throw new InvalidOperationException($"Can not set the value of property: '{propertyAccessor.PropertyName}'.Verify that the declaring type is not a value type(such as struct)");
                    }

                    if (name == propertyNames.Last()) // Assume the last property as a scalar (primitive) value
                    {
                        propertyAccessor.SetValue(target, value);
                    }
                    else
                    {
                        var t = propertyAccessor.GetValue(target);

                        if (t == null) // Nested property instance does not exist
                        {
                            t = Activator.CreateInstance(propertyAccessor.PropertyType);

                            propertyAccessor.SetValue(target, t);
                        }

                        target = t;

                        typeAccessor = t.GetTypeAccessor();
                    }                   
                }
            }
            else
            {
                if (!PropertyAccessors.ContainsKey(propertyName))
                {
                    throw new InvalidOperationException($"Property: '{propertyName}' not found for object of type: '{target.GetType().FullName}'");
                }

                var propertyAccessor = PropertyAccessors[propertyName];

                if (!propertyAccessor.CanSet)
                {
                    throw new InvalidOperationException($"Can not set the value of property: '{propertyAccessor.PropertyName}'.Verify that the declaring type is not a value type(such as struct)");
                }

                propertyAccessor.SetValue(target, value);
            }            
        }

        /// <summary>
        /// Sets the value to the property of the target object and notifies the subscriber when the property has changed
        /// </summary>
        /// <param name="target">The object to set the property</param>
        /// <param name="propertyName">The name of the property to set</param>
        /// <param name="value">The value to set</param>
        /// <param name="subscriber">The subscriber that gets notified when a property changes in the object</param>
        public void SetValueAndNotifyChange(object target, string propertyName, object value, IPropertyChangedSuscriber subscriber)
        {
            PropertyAccessor propertyAccessor = PropertyAccessors[propertyName];

            if (!propertyAccessor.CanSet)
            {
                throw new InvalidOperationException($"Can not set the value of property: '{propertyAccessor.PropertyName}'.Verify that the declaring type is not a value type(such as struct)");
            }

            propertyAccessor.SetValueAndNotifyChange(target, value, subscriber);
        }

        /// <summary>
        /// Retrieves a value 
        /// </summary>
        /// <param name="target">The object to retrieve the value from</param>
        /// <param name="propertyName">The name of the property to get the value from</param>
        /// <returns>The value of the property</returns>
        public object GetValue(object target, string propertyName)
        {
            if (propertyName.Contains(".")) // Support nested properties
            {
                var propertyNames = propertyName.Split('.');

                var typeAccessor = this; // The type accessor needs to change for each new target

                PropertyAccessor propertyAccessor = null;

                foreach (var name in propertyNames)
                {
                    if (!typeAccessor.PropertyAccessors.ContainsKey(name))
                    {
                        throw new InvalidOperationException($"Property: '{name}' not found for object of type: '{target.GetType().FullName}'");
                    }

                    propertyAccessor = typeAccessor.PropertyAccessors[name];

                    if (!propertyAccessor.CanGet)
                    {
                        throw new InvalidOperationException($"Can not get the value of property: '{propertyAccessor.PropertyName}'");
                    }

                    if (name != propertyNames.Last()) // Assume the last property as a scalar (primitive) value
                    {
                        var t = propertyAccessor.GetValue(target);

                        if (t == null) // Nested property instance does not exist
                        {
                            return null;
                        }

                        target = t;

                        typeAccessor = t.GetTypeAccessor();
                    }
                }

                return propertyAccessor.GetValue(target);
            }
            else
            {
                if (!PropertyAccessors.ContainsKey(propertyName))
                {
                    throw new InvalidOperationException($"Property: '{propertyName}' not found for object of type: '{target.GetType().FullName}'");
                }

                PropertyAccessor propertyAccessor = PropertyAccessors[propertyName];

                if (!propertyAccessor.CanGet)
                {
                    throw new InvalidOperationException($"Can not get the value of property: '{propertyAccessor.PropertyName}'");
                }

                return propertyAccessor.GetValue(target);
            }
        }

        /// <summary>
        /// Compares two objects for equality
        /// </summary>
        /// <param name="o1">The first object</param>
        /// <param name="o2">The second object</param>
        /// <param name="unequalProperties">
        /// The list of the names of the properties that are not equal.
        /// If the types are not equal its value will be null
        /// </param>
        /// <returns>True if the objects are equal, otherwise returns false</returns>
        public bool AreEqual(object o1, object o2, out List<string> unequalProperties)
        {
            unequalProperties = null;

            if (o1.GetType() != o2.GetType())
            {
                return false;
            }

            unequalProperties = _objectComparer(o1, o2);

            return (unequalProperties.Count == 0); // No differences
        }

        public T Clone<T>(T target)
        {
            return (T)Clone((object)target); // Cast to object to avoid calling the same method
        }

        public object Clone(object source)
        {
            if (source is ICloneable)
            {
                return ((ICloneable)source).Clone();
            }
            else if (source is ValueType)
            {
                return source;
            }
            else // Use reflection to create an object and copy all values from the source
            {
                object clone = Type.CreateInstance();

                // TODO: Emit the cloner for the type to make it faster
                foreach (PropertyAccessor accessor in PropertyAccessors.Values)
                {
                    if (accessor.IsPrimitive)
                    {
                        object val = accessor.GetValue(source);
                        accessor.SetValue(clone, val);
                    }

                    // TODO: Clone complex objects (if needed)
                }

                return clone;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// The accessor to the public properties of the object
        /// </summary>
        public IDictionary<string, PropertyAccessor> PropertyAccessors { get; private set; }

        #endregion

        #region Fields

        private Func<object, object, List<string>> _objectComparer;
        private Func<object, object> _objectCloner; 

        #endregion
    }
}
