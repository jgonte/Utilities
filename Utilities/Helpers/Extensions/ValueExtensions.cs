using System.Linq;

namespace Utilities
{
    public static class ValueExtensions
    {
        public static bool IsIn(this object o, params object[] values)
        {
            return values.Contains(o);
        }
    }
}
