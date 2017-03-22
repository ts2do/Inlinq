﻿using System.Collections.Generic;

namespace Inlinq.Impl
{
    public sealed class GroupJoinEnumerable<TOuter, TOuterEnumerator, TOuterKeySelector, TInner, TInnerEnumerator, TInnerKeySelector, TKey, TResultSelector, TResult, TEqualityComparer>
        : Enumerable<TResult, GroupJoinEnumerator<TOuter, TOuterEnumerator, TOuterKeySelector, TInner, TInnerEnumerator, TInnerKeySelector, TKey, TResultSelector, TResult, TEqualityComparer>>
        where TOuterEnumerator : IEnumerator<TOuter>
        where TOuterKeySelector : IFunctor<TOuter, TKey>
        where TInnerEnumerator : IEnumerator<TInner>
        where TInnerKeySelector : IFunctor<TInner, TKey>
        where TResultSelector : IFunctor<TOuter, InlinqArray<TInner>, TResult>
        where TEqualityComparer : IEqualityComparer<TKey>
    {
        private IEnumerable<TOuter, TOuterEnumerator> outer;
        private IEnumerable<TInner, TInnerEnumerator> inner;
        private TOuterKeySelector outerKeySelector;
        private TInnerKeySelector innerKeySelector;
        private TResultSelector resultSelector;
        private TEqualityComparer comparer;

        public GroupJoinEnumerable(IEnumerable<TOuter, TOuterEnumerator> outer, IEnumerable<TInner, TInnerEnumerator> inner,
            TOuterKeySelector outerKeySelector, TInnerKeySelector innerKeySelector, TResultSelector resultSelector, TEqualityComparer comparer)
        {
            this.outer = outer;
            this.inner = inner;
            this.outerKeySelector = outerKeySelector;
            this.innerKeySelector = innerKeySelector;
            this.resultSelector = resultSelector;
            this.comparer = comparer;
        }

        public override bool GetCount(out int count) => outer.GetCount(out count);

        public override GroupJoinEnumerator<TOuter, TOuterEnumerator, TOuterKeySelector, TInner, TInnerEnumerator, TInnerKeySelector, TKey, TResultSelector, TResult, TEqualityComparer> GetEnumerator()
        {
            return new GroupJoinEnumerator<TOuter, TOuterEnumerator, TOuterKeySelector, TInner, TInnerEnumerator, TInnerKeySelector, TKey, TResultSelector, TResult, TEqualityComparer>(
                outer.GetEnumerator(), inner, outerKeySelector, innerKeySelector, resultSelector, comparer);
        }
    }
}
