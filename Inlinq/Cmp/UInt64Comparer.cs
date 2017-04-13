using System.Collections.Generic;

namespace Inlinq.Cmp
{
    public struct UInt64Comparer : IComparer<ulong>, IEqualityComparer<ulong>
    {
        public int Compare(ulong x, ulong y)
        {
            if (x < y) return -1;
            if (x > y) return 1;
            return 0;
        }

        public bool Equals(ulong x, ulong y) => x == y;
        public int GetHashCode(ulong obj) => obj.GetHashCode();
    }
}
