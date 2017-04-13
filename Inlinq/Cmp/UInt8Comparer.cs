using System.Collections.Generic;

namespace Inlinq.Cmp
{
    public struct UInt8Comparer : IComparer<byte>, IEqualityComparer<byte>
    {
        public int Compare(byte x, byte y) => x - y;
        public bool Equals(byte x, byte y) => x == y;
        public int GetHashCode(byte obj) => obj;
    }
}
