namespace Utilities
{
    public static class INamedElementBuilderExtensions
    {
        /// <summary>
        /// Sets the name of the object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T Name<T>(this T builder, string name) where T : INamed
        {
            builder.Name = name;

            return builder;
        }

        /// <summary>
        /// Validates the name if required
        /// </summary>
        /// <param name="named"></param>
        public static void NameIsRequired(this INamed named, string message = null)
        {
            if (string.IsNullOrWhiteSpace(named.Name))
            {
                throw new System.InvalidOperationException(
                    string.IsNullOrWhiteSpace(message) ?
                        "Name is required" :
                        message
                );
            }
        }
    }
}
