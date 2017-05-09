using Inlinq.Sort;
using System;
using System.Collections.Generic;

namespace Inlinq.Impl
{
    public sealed class OrderedEnumerable<T, TEnumerator> : Enumerable<T, OrderedEnumerator<T, TEnumerator>>
        where TEnumerator : IEnumerator<T>
    {
        private IEnumerable<T, TEnumerator> source;
        private ISortChain<T, IPrimarySort<T>> primarySort;

        internal OrderedEnumerable(IEnumerable<T, TEnumerator> source, ISortChain<T, IPrimarySort<T>> primarySort)
        {
            this.source = source;
            this.primarySort = primarySort;
        }

        internal OrderedEnumerable<T, TEnumerator> ThenByImpl<TKey, TComparer>(Func<T, TKey> keySelector, TComparer comparer)
            where TComparer : IComparer<TKey>
            => new OrderedEnumerable<T, TEnumerator>(source, primarySort.Chain(keySelector, comparer));

        public override bool GetCount(out int count) => source.GetCount(out count);

        public override OrderedEnumerator<T, TEnumerator> GetEnumerator()
            => new OrderedEnumerator<T, TEnumerator>(source, primarySort.Unwrap);
    }
}
