using System.Collections.Generic;

namespace Inlinq.Impl
{
    public sealed class GroupByEnumerable<TSource, TEnumerator, TKey, TElement, TKeySelector, TElementSelector, TEqualityComparer> : Enumerable<Grouping<TKey, TElement>, GroupingEnumerator<TKey, TElement>>
        where TEnumerator : IEnumerator<TSource>
        where TKeySelector : IFunctor<TSource, TKey>
        where TElementSelector : IFunctor<TSource, TElement>
        where TEqualityComparer : IEqualityComparer<TKey>
    {
        private IEnumerable<TSource> source;
        private TKeySelector keySelector;
        private TElementSelector elementSelector;
        private TEqualityComparer comparer;

        public GroupByEnumerable(IEnumerable<TSource> source, TKeySelector keySelector, TElementSelector elementSelector, TEqualityComparer comparer)
        {
            this.source = source;
            this.keySelector = keySelector;
            this.elementSelector = elementSelector;
            this.comparer = comparer;
        }

        public override GroupingEnumerator<TKey, TElement> GetEnumerator()
            => Lookup<TKey, TElement, TEqualityComparer>.Create(source, keySelector, elementSelector, comparer).GetEnumerator();
    }
}
