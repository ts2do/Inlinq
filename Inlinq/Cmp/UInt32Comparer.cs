using System.Collections.Generic;

namespace Inlinq.Cmp
{
    public struct UInt32Comparer : IComparer<uint>, IEqualityComparer<uint>
    {
        public int Compare(uint x, uint y)
        {
            if (x < y) return -1;
            if (x > y) return 1;
            return 0;
        }

        public bool Equals(uint x, uint y) => x == y;
        public int GetHashCode(uint obj) => (int)obj;
    }
}
