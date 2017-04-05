using System.Collections.Generic;

namespace Inlinq.Cmp
{
    public struct Int64Comparer : IComparer<long>
    {
        public int Compare(long x, long y) => System.Math.Sign(x - y);
    }
}
