using System;
using System.Reflection;

namespace Utilities
{
    /// <summary>
    /// Extension methods to deal with attributes
    /// </summary>
    public static class AttributesExtensions
    {
        public static T[] GetAttributes<T>(this MemberInfo member, bool inherit)
        {
            object[] objects = member.GetCustomAttributes(typeof(T), inherit);

            if (objects == null
                || objects.Length == 0)
            {
                return null;
            }

            T[] attributes = new T[objects.Length];
            objects.CopyTo(attributes, 0);

            return attributes;
        }

        private static object[] GetAttributes(this MemberInfo member, Type attributeType, bool inherit)
        {
            if (attributeType != null)
            {
                return member.GetCustomAttributes(attributeType, inherit);
            }

            return member.GetCustomAttributes(inherit);
        }

        public static T GetAttribute<T>(this MemberInfo member, bool inherit)
        {
            return (T)GetAttribute(member, typeof(T), inherit);
        }

        private static object GetAttribute(this MemberInfo member, Type attributeType, bool inherit)
        {
            object[] attributes = member.GetCustomAttributes(attributeType, inherit);

            switch (attributes.Length)
            {
                case 0: return null;
                case 1: return attributes[0];
                default: throw new InvalidOperationException(
                        string.Format("There are more than one custom attribute of type: '{0}' in member: '{1}'",
                        attributeType,
                        member.Name));
            }
        }

        /// <summary>
        /// Retrieves the attribute attached to the type
        /// </summary>
        /// <typeparam name="T">The type of the attribute to retrieve</typeparam>
        /// <param name="type">The type the attribute is attached to</param>
        /// <param name="inherit">Whether to search for that attribute in the base types</param>
        /// <returns>The attribute if found or null</returns>
        public static T GetAttribute<T>(this Type type, bool inherit)
        {
            return (T)GetAttribute(type, typeof(T), inherit);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="attributeType"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public static object GetAttribute(this Type type, Type attributeType, bool inherit)
        {
            object[] attributes = type.GetCustomAttributes(attributeType, inherit);

            switch (attributes.Length)
            {
                case 0: return null;
                case 1: return attributes[0];
                default: throw new InvalidOperationException(
                        string.Format("There are more than one custom attribute of type: '{0}' in type: '{1}'",
                        attributeType,
                        type.FullName));
            }
        }

        public static T GetAttribute<T>(this Assembly assembly, bool inherit)
        {
            return (T)GetAttribute(assembly, typeof(T), inherit);
        }

        private static object GetAttribute(this Assembly assembly, Type attributeType, bool inherit)
        {
            object[] attributes = assembly.GetCustomAttributes(attributeType, inherit);

            switch (attributes.Length)
            {
                case 0: return null;
                case 1: return attributes[0];
                default: throw new InvalidOperationException(
                        string.Format("There are more than one custom attribute of type: '{0}' in assembly: '{1}'",
                        attributeType,
                        assembly.FullName));
            }
        }
    }
}
