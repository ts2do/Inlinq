using System.Collections.Generic;

namespace Inlinq.Impl
{
    public sealed class OrderedEnumerable<T, TEnumerator, TKey, TKeySelector, TComparer> : Enumerable<T, OrderedEnumerator<T, TEnumerator, TKey, TKeySelector, TComparer>>
        where TEnumerator : IEnumerator<T>
        where TKeySelector : IFunctor<T, TKey>
        where TComparer : IComparer<TKey>
    {
        private IEnumerable<T, TEnumerator> source;
        private TKeySelector keySelector;
        private TComparer comparer;

        public OrderedEnumerable(IEnumerable<T, TEnumerator> source, TKeySelector keySelector, TComparer comparer)
        {
            this.source = source;
            this.keySelector = keySelector;
            this.comparer = comparer;
        }

        public override bool GetCount(out int count) => source.GetCount(out count);

        public override OrderedEnumerator<T, TEnumerator, TKey, TKeySelector, TComparer> GetEnumerator()
            => new OrderedEnumerator<T, TEnumerator, TKey, TKeySelector, TComparer>(source, keySelector, comparer);
    }
}
