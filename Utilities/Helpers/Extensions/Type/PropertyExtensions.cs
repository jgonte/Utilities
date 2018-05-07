using System;
using System.Reflection;

namespace Utilities
{
    public static class PropertyExtensions
    {
        /// <summary>
        /// Determines whether the property info is hiding a a property declared in the base class
        /// </summary>
        /// <param name="property"></param>
        /// <returns>True if the property is hiding a base one, false otherwise</returns>
        public static bool IsHidingProperty(this PropertyInfo property)
        {
            Type baseType = property.DeclaringType.BaseType;
            PropertyInfo baseProperty = baseType.GetProperty(property.Name, property.PropertyType);

            return (baseProperty != null); // Found a property on the base class that matches name and type
        }

        /// <summary>
        /// Whether the property info is virtual
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public static bool IsVirtual(this PropertyInfo property)
        {
            return (property.GetGetMethod().Attributes & MethodAttributes.Virtual) != 0;
        }
    }
}
