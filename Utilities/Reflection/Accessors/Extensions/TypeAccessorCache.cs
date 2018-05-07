using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;

namespace Utilities
{
    /// <summary>
    /// Caches the type accessors for all the types and bindings combinations
    /// </summary>
    public static class TypeAccessorCache
    {
        static TypeAccessorCache()
        {
            TypeAccessors = new ConcurrentDictionary<string, TypeAccessor>();
        }

        /// <summary>
        /// Shortcut to return the type accessoe from an object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static TypeAccessor GetTypeAccessor(this object obj, BindingFlags bindings = TypeTraversal.DefaultBinding)
        {
            // PreCondition.RequireNotNull(obj, "obj");

            return GetTypeAccessor(obj.GetType(), bindings);
        }

        public static TypeAccessor GetTypeAccessor(this Type type, BindingFlags bindings = TypeTraversal.DefaultBinding)
        {
            // PreCondition.RequireNotNull(type, "type");

            string key = string.Format("{0}:{1}", type.FullName, ((int)bindings).ToString());

            if (!TypeAccessors.ContainsKey(key))
            {
                TypeAccessor typeAccessor = new TypeAccessor(type, bindings);
                TypeAccessors.Add(key, typeAccessor);
                return typeAccessor;
            }

            return TypeAccessors[key];
        }

        /// <summary>
        /// Cached type accessors
        /// </summary>
        public static IDictionary<string, TypeAccessor> TypeAccessors { get; private set; }
    }
}
