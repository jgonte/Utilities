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
    }
}
