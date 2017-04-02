using System.Collections;
using System.Collections.Generic;

namespace Inlinq.Impl
{
    public struct GroupJoinEnumerator<TOuter, TOuterEnumerator, TOuterKeySelector, TInner, TInnerKeyEnumerator, TInnerSelector, TKey, TResultSelector, TResult, TEqualityComparer> : IEnumerator<TResult>
        where TOuterEnumerator : IEnumerator<TOuter>
        where TOuterKeySelector : IFunctor<TOuter, TKey>
        where TInnerKeyEnumerator : IEnumerator<TInner>
        where TInnerSelector : IFunctor<TInner, TKey>
        where TResultSelector : IFunctor<TOuter, InlinqArray<TInner>, TResult>
        where TEqualityComparer : IEqualityComparer<TKey>
    {
        private TOuterEnumerator outer;
        private IEnumerable<TInner, TInnerKeyEnumerator> inner;
        private TOuterKeySelector outerKeySelector;
        private TInnerSelector innerKeySelector;
        private TResultSelector resultSelector;
        private TEqualityComparer comparer;
        private Lookup<TKey, TInner, TEqualityComparer> lookup;

        public TResult Current
        {
            get
            {
                TOuter item;
                return resultSelector.Invoke(item = outer.Current, lookup[outerKeySelector.Invoke(item)].AsInlinqArray());
            }
        }
        
        public GroupJoinEnumerator(TOuterEnumerator outer, IEnumerable<TInner, TInnerKeyEnumerator> inner, TOuterKeySelector outerKeySelector,
            TInnerSelector innerKeySelector, TResultSelector resultSelector, TEqualityComparer comparer)
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
