using System;
using System.Collections.Generic;

namespace Utilities
{
    /// <summary>
    /// Binds the properties of the target object to the properties of the source object
    /// </summary>
    public class PropertyBinder
    {
        public object Source { get; set; }

        public IList<PropertyLink> PropertyLinks { get; set; }

        /// <summary>
        /// Copies the values of the linked properties from the source into the target
        /// </summary>
        /// <param name="target"></param>
        public void Bind(object target)
        {
            if (null == Source)
            {
                throw new ArgumentNullException("Source");
            }

            if (null == target)
            {
                throw new ArgumentNullException("target");
            }

            var sourceAccessor = Source.GetTypeAccessor();

            var targetAccessor = target.GetTypeAccessor();

            foreach (PropertyLink link in PropertyLinks)
            {
                object value = sourceAccessor.GetValue(Source, link.Source);

                targetAccessor.SetValue(target, link.Target, value);
            }
        }

    }
}
