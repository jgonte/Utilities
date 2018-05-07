using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Utilities
{
    /// <summary>
    /// Traverses the object structure using reflection
    /// </summary>
    public class ObjectTraversal
    {
        #region Properties

        /// <summary>
        /// Executed when the object is null
        /// </summary>
        public Action<string> OnNullObject { get; set; }

        /// <summary>
        /// Executed when the object was already visited
        /// </summary>
        public Action<object, object> OnVisitedObject { get; set; }

        /// <summary>
        /// Executed when the object is a primitive
        /// </summary>
        public Action<string, object, object, bool> OnPrimitive { get; set; }

        /// <summary>
        /// Executed when a collection starts
        /// </summary>
        public Func<string, object, object, bool> OnBeginCollection { get; set; }

        /// <summary>
        /// Executed when the collection ends
        /// </summary>
        public Action<object, object> OnEndCollection { get; set; }

        /// <summary>
        /// Executed when a complex object starts
        /// </summary>
        public Func<string, object, object, bool, bool> OnBeginObject { get; set; }

        /// <summary>
        /// Executed when a complex object ends
        /// </summary>
        public Action<object, object, bool> OnEndObject { get; set; } 

        #endregion

        public void Traverse(object o, object parent = null, string propertyName = null, bool isItem = false)
        {
            // Handle when the object is null
            if (o == null)
            {
                if (OnNullObject != null)
                {
                    OnNullObject(propertyName);
                }

                return;
            }

            Type type = o.GetType();

            if (!type.IsPrimitive())
            {
                // It if was already visited then return
                if (_visited.Contains(o))
                {
                    if (OnVisitedObject != null)
                    {
                        OnVisitedObject(o, parent);
                    }

                    return;
                }

                _visited.Add(o); // Mark it as visited
            }

            if (type.IsPrimitive())
            {
                if (OnPrimitive != null)
                {
                    OnPrimitive(propertyName, o, parent, isItem);
                }
            }
            else if (type.IsCollection())
            {
                OnCollection(propertyName, o, parent);
            }
            else // Complex object
            {
                OnComplexObject(propertyName, o, parent, type, isItem);
            }
        }

        #region Helpers

        private void OnCollection(string propertyName, object o, object parent)
        {
            if (OnBeginCollection != null)
            {
                if (!OnBeginCollection(propertyName, o, parent))
                {
                    return;
                }
            }

            foreach (object item in (IEnumerable)o)
            {
                Traverse(item, parent, propertyName: propertyName, isItem: true);
            }

            if (OnEndCollection != null)
            {
                OnEndCollection(o, parent);
            }
        }

        private void OnComplexObject(string propertyName, object o, object parent, Type type, bool isItem = false)
        {
            if (OnBeginObject != null)
            {
                if (!OnBeginObject(propertyName, o, parent, isItem))
                {
                    return;
                }
            }

            foreach (PropertyInfo property in type.GetProperties())
            {
                var value = property.GetValue(o, null);
                Traverse(value, o, propertyName: property.Name);
            }

            if (OnEndObject != null)
            {
                OnEndObject(o, parent, isItem);
            }
        } 

        #endregion

        private HashSet<object> _visited = new HashSet<object>(ReferenceComparer.Instance);
    }
}
