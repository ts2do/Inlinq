using System;
using System.Collections.Generic;

namespace Inlinq.Impl
{
    public sealed class GroupByEnumerable<TSource, TEnumerator, TKey, TElement, TEqualityComparer> : Enumerable<Grouping<TKey, TElement>, GroupingEnumerator<TKey, TElement>>
        where TEnumerator : IEnumerator<TSource>
        where TEqualityComparer : IEqualityComparer<TKey>
    {
        private IEnumerable<TSource> source;
        private Func<TSource, TKey> keySelector;
        private Func<TSource, TElement> elementSelector;
        private TEqualityComparer comparer;

        public GroupByEnumerable(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, TEqualityComparer comparer)
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
