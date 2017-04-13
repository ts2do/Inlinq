using System.Collections.Generic;

namespace Inlinq.Cmp
{
    public struct Int32Comparer : IComparer<int>, IEqualityComparer<int>
    {
        public int Compare(int x, int y)
        {
            if (x < y) return -1;
            if (x > y) return 1;
            return 0;
        }

        public bool Equals(int x, int y) => x == y;
        public int GetHashCode(int obj) => obj;
    }
}
