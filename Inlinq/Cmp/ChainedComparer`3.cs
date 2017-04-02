using System.Collections.Generic;

namespace Inlinq.Cmp
{
    public struct CompositeComparer<T, TFirst, TSecond> : IComparer<T>
        where TFirst : IComparer<T>
        where TSecond : IComparer<T>
    {
        private TFirst first;
        private TSecond second;

        public CompositeComparer(TFirst first, TSecond second)
        {
            this.first = first;
            this.second = second;
        }

        public int Compare(T x, T y)
        {
            int c = first.Compare(x, y);
            return c != 0 ? c : second.Compare(x, y);
        }
    }
}
