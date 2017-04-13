using System.Collections.Generic;

namespace Inlinq.Cmp
{
    public struct CompositeComparer<TKey1, TComparer1, TKey2, TComparer2> : IComparer<CompositeKey<TKey1, TKey2>>
        where TComparer1 : IComparer<TKey1>
        where TComparer2 : IComparer<TKey2>
    {
        private TComparer1 first;
        private TComparer2 second;

        public CompositeComparer(TComparer1 first, TComparer2 second)
        {
            this.first = first;
            this.second = second;
        }

        public int Compare(CompositeKey<TKey1, TKey2> x, CompositeKey<TKey1, TKey2> y)
        {
            int c = first.Compare(x.first, y.first);
            return c != 0 ? c : second.Compare(x.second, y.second);
        }
    }
}
