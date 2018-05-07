namespace Utilities
{
    public static class IDescribedElementBuilderExtensions
    {
        /// <summary>
        /// Sets the description of the model element
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public static T Description<T>(this T builder, string description) where T : IDescribed
        {
            builder.Description = description;

            return builder;
        }
    }
}
