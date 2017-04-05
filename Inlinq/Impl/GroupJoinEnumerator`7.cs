using System;
using System.Collections;
using System.Collections.Generic;

namespace Inlinq.Impl
{
    public struct GroupJoinEnumerator<TOuter, TOuterEnumerator, TInner, TInnerKeyEnumerator, TKey, TResult, TEqualityComparer> : IEnumerator<TResult>
        where TOuterEnumerator : IEnumerator<TOuter>
        where TInnerKeyEnumerator : IEnumerator<TInner>
        where TEqualityComparer : IEqualityComparer<TKey>
    {
        private TOuterEnumerator outer;
        private IEnumerable<TInner, TInnerKeyEnumerator> inner;
        private Func<TOuter, TKey> outerKeySelector;
        private Func<TInner, TKey> innerKeySelector;
        private Func<TOuter, InlinqArray<TInner>, TResult> resultSelector;
        private TEqualityComparer comparer;
        private Lookup<TKey, TInner, TEqualityComparer> lookup;

        public TResult Current
        {
            get
            {
                TOuter item;
                return resultSelector(item = outer.Current, lookup[outerKeySelector(item)].AsInlinqArray());
            }
        }
        
        public GroupJoinEnumerator(TOuterEnumerator outer, IEnumerable<TInner, TInnerKeyEnumerator> inner, Func<TOuter, TKey> outerKeySelector,
            Func<TInner, TKey> innerKeySelector, Func<TOuter, InlinqArray<TInner>, TResult> resultSelector, TEqualityComparer comparer)
        {
            this.outer = outer;
            this.inner = inner;
            this.outerKeySelector = outerKeySelector;
            this.innerKeySelector = innerKeySelector;
            this.resultSelector = resultSelector;
            this.comparer = comparer;
            lookup = null;
        }

        public void Dispose() => outer.Dispose();

        public bool MoveNext()
        {
            bool succeeded = outer.MoveNext();
            if (lookup != null)
                return succeeded;

            if (succeeded)
                lookup = Lookup<TKey, TInner, TEqualityComparer>.CreateForJoin(inner, innerKeySelector, comparer);

            return succeeded;
        }

        public void Reset()
        {
            outer.Reset();
            lookup = null;
        }

        object IEnumerator.Current => Current;
    }
}
