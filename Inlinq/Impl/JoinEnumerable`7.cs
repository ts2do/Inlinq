using System;
using System.Collections.Generic;

namespace Inlinq.Impl
{
    public sealed class JoinEnumerable<TOuter, TOuterEnumerator, TInner, TInnerEnumerator, TKey, TResult, TEqualityComparer>
        : Enumerable<TResult, JoinEnumerator<TOuter, TOuterEnumerator, TInner, TInnerEnumerator, TKey, TResult, TEqualityComparer>>
        where TOuterEnumerator : IEnumerator<TOuter>
        where TInnerEnumerator : IEnumerator<TInner>
        where TEqualityComparer : IEqualityComparer<TKey>
    {
        private IEnumerable<TOuter, TOuterEnumerator> outer;
        private IEnumerable<TInner, TInnerEnumerator> inner;
        private Func<TOuter, TKey> outerKeySelector;
        private Func<TInner, TKey> innerKeySelector;
        private Func<TOuter, TInner, TResult> resultSelector;
        private TEqualityComparer comparer;

        public JoinEnumerable(IEnumerable<TOuter, TOuterEnumerator> outer, IEnumerable<TInner, TInnerEnumerator> inner,
            Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector, TEqualityComparer comparer)
        {
            this.outer = outer;
            this.inner = inner;
            this.outerKeySelector = outerKeySelector;
            this.innerKeySelector = innerKeySelector;
            this.resultSelector = resultSelector;
            this.comparer = comparer;
        }

        public override bool GetCount(out int count) => outer.GetCount(out count);

        public override JoinEnumerator<TOuter, TOuterEnumerator, TInner, TInnerEnumerator, TKey, TResult, TEqualityComparer> GetEnumerator()
        {
            return new JoinEnumerator<TOuter, TOuterEnumerator, TInner, TInnerEnumerator, TKey, TResult, TEqualityComparer>(
                outer.GetEnumerator(), inner, outerKeySelector, innerKeySelector, resultSelector, comparer);
        }
    }
}
