using System.Collections.Generic;

namespace Inlinq.Cmp
{
    public struct ReverseComparer<T, TComparer> : IComparer<T>
        where TComparer : IComparer<T>
    {
        private TComparer comparer;
        public ReverseComparer(TComparer comparer) => this.comparer = comparer;
        public int Compare(T x, T y) => comparer.Compare(y, x);
    }
}
