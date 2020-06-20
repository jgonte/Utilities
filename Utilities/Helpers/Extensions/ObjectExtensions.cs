using System.Collections.Generic;

namespace Utilities
{
    public static class ObjectExtensions
    {
        public static IDictionary<string, object> ToDictionary(this object o)
        {
            var dictionary = new Dictionary<string, object>();

            var typeAccessor = o.GetTypeAccessor();

            foreach (var propertyAccessor in typeAccessor.PropertyAccessors)
            {
                dictionary.Add(propertyAccessor.Key, typeAccessor.GetValue(o, propertyAccessor.Key));
            };

            return dictionary;
        }
    }
}
