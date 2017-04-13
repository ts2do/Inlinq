using System;
using System.Collections.Generic;

namespace Inlinq.Impl
{
    public sealed class OrderedEnumerable<T, TEnumerator, TKey, TComparer> : Enumerable<T, OrderedEnumerator<T, TEnumerator, TKey, TComparer>>
        where TEnumerator : IEnumerator<T>
        where TComparer : IComparer<TKey>
    {
        private IEnumerable<T, TEnumerator> source;
        private Func<T, TKey> keySelector;
        private TComparer comparer;

        public IEnumerable<T, TEnumerator> Source => source;
        public Func<T, TKey> KeySelector => keySelector;
        public TComparer Comparer => comparer;

        public OrderedEnumerable(IEnumerable<T, TEnumerator> source, Func<T, TKey> keySelector, TComparer comparer)
        {
            this.source = source;
            this.keySelector = keySelector;
            this.comparer = comparer;
        }

        public override bool GetCount(out int count) => source.GetCount(out count);

        public override OrderedEnumerator<T, TEnumerator, TKey, TComparer> GetEnumerator()
            => new OrderedEnumerator<T, TEnumerator, TKey, TComparer>(source, keySelector, comparer);
    }
}
