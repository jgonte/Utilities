using System;

namespace Utilities.Builders
{
    public static class IBuilderExtensions
    {
        public static T FindParentBuilder<T>(this IBuilder builder, bool throwIfNotFound = true)
        {
            var parentBuilder = builder.ParentBuilder;

            while (parentBuilder != null && !(parentBuilder is T))
            {
                parentBuilder = parentBuilder.ParentBuilder;
            }

            if (parentBuilder == null && throwIfNotFound)
            {
                throw new InvalidOperationException($"Unable to find parent builder of type: '{typeof(T)}'");
            }

            return (T)parentBuilder;
        }
    }
}
