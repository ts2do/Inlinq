using System.Collections.Generic;

namespace Inlinq.Cmp
{
    public struct Int16Comparer : IComparer<short>, IEqualityComparer<short>
    {
        public int Compare(short x, short y) => x - y;
        public bool Equals(short x, short y) => x == y;
        public int GetHashCode(short obj) => obj;
    }
}
