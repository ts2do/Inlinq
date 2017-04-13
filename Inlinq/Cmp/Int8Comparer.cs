using System.Collections.Generic;

namespace Inlinq.Cmp
{
    public struct Int8Comparer : IComparer<sbyte>, IEqualityComparer<sbyte>
    {
        public int Compare(sbyte x, sbyte y) => x - y;
        public bool Equals(sbyte x, sbyte y) => x == y;
        public int GetHashCode(sbyte obj) => obj;
    }
}
