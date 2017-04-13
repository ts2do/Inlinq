using System.Collections.Generic;

namespace Inlinq.Cmp
{
    public struct UInt16Comparer : IComparer<ushort>, IEqualityComparer<ushort>
    {
        public int Compare(ushort x, ushort y) => x - y;
        public bool Equals(ushort x, ushort y) => x == y;
        public int GetHashCode(ushort obj) => obj;
    }
}
