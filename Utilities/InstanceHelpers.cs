namespace Utilities
{
    public static class InstanceHelpers
    {
        public static T Coalesce<T>(params object[] instances) where T: class
        {
            foreach (var instance in instances)
            {
                if (instance != null)
                {
                    return instance as T;
                }
            }

            return null;
        }
    }
}
