using System;

namespace Utilities
{
    public enum CollectionTypes
    {
        None = 0,
        Enumerable,
        Collection,
        List,
        Array,
        Dictionary,
        Queue,
        Stack,
        HashSet
    };

    /// <summary>
    /// Extension methods to work with collection types
    /// </summary>
    public static class CollectionTypeExtensions
    {
        public static bool IsDictionary(this CollectionTypes collectionType)
        {
            return (collectionType == CollectionTypes.Dictionary
                || collectionType == CollectionTypes.HashSet);
        }
        /// <summary>
        /// Tests whether the type is a collection
        /// </summary>
        /// <param name="type">The type to test</param>
        /// <returns>True if the type is a collection, false otherwise</returns>
        public static bool IsCollection(this Type type)
        {
            if (type == typeof(string)) // The string is also a collection of characters
            {
                return false;
            }

            if (type == typeof(byte[])) // The binary type is a collection of bytes
            {
                return false;
            }

            return type.HasSomeInterfaceOf("IEnumerable");
        }

        /// <summary>
        /// Returns the type of the collection
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static CollectionTypes GetCollectionType(this Type type)
        {
            if (!IsCollection(type))
            {
                return CollectionTypes.None;
            }

            if (type.IsArray)
            {
                return CollectionTypes.Array;
            }

            if (type.Name.StartsWith("IDictionary")
                || type.HasSomeInterfaceOf("IDictionary"))
            {
                return CollectionTypes.Dictionary;
            }

            return CollectionTypes.List;
        }

        /// <summary>
        /// Retrieves the type of the item contained in the collection
        /// </summary>
        /// <param name="type">The collection type</param>
        /// <returns>Returns the type of the item in the collection</returns>
        public static Type GetItemType(this Type type)
        {
            Type elementType = type.GetElementType(); // For arrays and maybe other collections

            if (elementType != null)
            {
                return elementType;
            }

            if (type.IsGenericType)
            {
                Type[] genericArguments = type.GetGenericArguments();

                if ((type.Name.StartsWith("IDictionary") || type.HasSomeInterfaceOf("IDictionary"))
                    && genericArguments.Length > 1) // The NET Dictionary
                {
                    return genericArguments[1]; // Return the type of the value
                }

                return genericArguments[0]; // Any other collection
            }

            // If it is not a generic type it can be any type of object
            // We return the type of object instead of throwing an exception 
            //Logger.LogWarning("Unable to get the item type of the collection: {0}, returning type of object", type.FullName);

            return typeof(object);
        }

        public static Type GetKeyType(this Type type)
        {
            if (type.IsGenericType)
            {
                Type[] genericArguments = type.GetGenericArguments();

                if ((type.Name.StartsWith("IDictionary") || type.HasSomeInterfaceOf("IDictionary"))
                    && genericArguments.Length > 1) // The NET Dictionary
                {
                    return genericArguments[0]; // Return the type of the key
                }
            }

            return null;
        }
    }
}
