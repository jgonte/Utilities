using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Utilities
{
    public class ReferenceComparer
            : IEqualityComparer<object>
    {
        static ReferenceComparer()
        {
            Instance = new ReferenceComparer();
        }

        private ReferenceComparer()
        {
        }

        public static ReferenceComparer Instance { get; private set; }

        #region Overridables

        bool IEqualityComparer<object>.Equals(object x, object y)
        {
            return object.ReferenceEquals(x, y);
        }

        int IEqualityComparer<object>.GetHashCode(object obj)
        {
            if (obj == null)
            {
                return 0;
            }

            return RuntimeHelpers.GetHashCode(obj);
        } 

        #endregion
    }
}
