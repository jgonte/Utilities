using System;
using System.Collections.Generic;
using System.Reflection;
using System.Diagnostics;

namespace Utilities
{
    /// <summary>
    /// Traverses the type tree using reflection
    /// </summary>
    public class TypeTraversal
    {
        public TypeTraversal()
        {
            Traversed = new HashSet<string>();
            Logger = new TraceSource("TypeTraversal");
        }

        #region Constants

        /// <summary>
        /// The default binding for most of the uses
        /// </summary>
        public const BindingFlags DefaultBinding = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static;
        public const BindingFlags DefaultPropertyBinding = DefaultBinding | BindingFlags.DeclaredOnly;

        #endregion

        public HashSet<string> Traversed { get; private set; }

        public static TraceSource Logger { get; private set; }

        #region Methods

        public void Traverse(Type type, 
                             BindingFlags methodsBinding = DefaultBinding,
                             BindingFlags propertiesBinding = DefaultPropertyBinding,
                             BindingFlags fieldsBinding = DefaultBinding,
                             BindingFlags nestedTypesBinding = DefaultBinding)
        {
            if (IsExcludedType != null
                && IsExcludedType(type))
            {
                //Logger.LogInformation("Type: '{0}' is excluded from traversal", type);

                return;
            }

            if (OnBeginType != null)
            {
                // Guard against multiple traversal of the same type
                string fullName = type.FullName;

                if (Traversed.Contains(fullName))
                {
                    return;
                }

                Traversed.Add(fullName);

                if (!OnBeginType(this, type))
                {
                    return; // Do not traverse the properties and attributes of the type
                }
            }

            if (OnNestedType != null)
            {
                foreach (Type nestedType in type.GetNestedTypes(nestedTypesBinding))
                {
                    OnNestedType(this, nestedType);
                }
            }

            if (OnMethod != null)
            {
                foreach (MethodInfo method in type.GetMethods(methodsBinding))
                {
                    OnMethod(this, type, method);
                }
            }

            // One of the following need to be set to traverse the properties
            if (OnProperty != null
                || OnPropertyDelegate != null)
            {
                foreach (PropertyInfo property in type.GetProperties(propertiesBinding))
                {
                    if (!property.PropertyType.IsSubclassOf(typeof(Delegate)))
                    {
                        if (OnProperty != null)
                        {
                            OnProperty(this, type, property);
                        }
                    }
                    else
                    {
                        if (OnPropertyDelegate != null)
                        {
                            OnPropertyDelegate(this, type, property);
                        }
                    }
                }
            }

            if (OnField != null)
            {
                foreach (FieldInfo field in type.GetFields(fieldsBinding))
                {
                    OnField(this, type, field); 
                }
            }

            if (OnEndType != null)
            {
                OnEndType(this, type);
            }
        } 

        #endregion

        #region Handlers

        /// <summary>
        /// Whether the type is excluded from the traversal
        /// </summary>
        public Func<Type, bool> IsExcludedType { get; set; }

        public Func<TypeTraversal, Type, bool> OnBeginType { get; set; }

        public Action<TypeTraversal, Type> OnEndType { get; set; }

        public Action<TypeTraversal, Type, MethodInfo> OnMethod { get; set; }

        public Action<TypeTraversal, Type, PropertyInfo> OnProperty { get; set; }

        /// <summary>
        /// Handler called when the type of the property is a delegate
        /// </summary>
        public Action<TypeTraversal, Type, PropertyInfo> OnPropertyDelegate { get; set; }

        public Action<TypeTraversal, Type, FieldInfo> OnField { get; set; }

        public Action<TypeTraversal, Type> OnNestedType { get; set; }

        #endregion
    }
}
