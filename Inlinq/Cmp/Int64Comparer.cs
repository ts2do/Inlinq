using System.Collections.Generic;

namespace Inlinq.Cmp
{
    public struct Int64Comparer : IComparer<long>, IEqualityComparer<long>
    {
        public int Compare(long x, long y)
        {
            if (x < y) return -1;
            if (x > y) return 1;
            return 0;
        }

        public bool Equals(long x, long y) => x == y;
        public int GetHashCode(long obj) => obj.GetHashCode();
    }
}
