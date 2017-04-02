using System.Collections;
using System.Collections.Generic;

namespace Inlinq.Impl
{
    public struct JoinEnumerator<TOuter, TOuterEnumerator, TOuterKeySelector, TInner, TInnerKeyEnumerator, TInnerSelector, TKey, TResultSelector, TResult, TEqualityComparer> : IEnumerator<TResult>
        where TOuterEnumerator : IEnumerator<TOuter>
        where TOuterKeySelector : IFunctor<TOuter, TKey>
        where TInnerKeyEnumerator : IEnumerator<TInner>
        where TInnerSelector : IFunctor<TInner, TKey>
        where TResultSelector : IFunctor<TOuter, TInner, TResult>
        where TEqualityComparer : IEqualityComparer<TKey>
    {
        private TOuterEnumerator outerEnumerator;
        private IEnumerable<TInner, TInnerKeyEnumerator> innerEnumerable;
        private TOuterKeySelector outerKeySelector;
        private TInnerSelector innerKeySelector;
        private TResultSelector resultSelector;
        private TEqualityComparer comparer;
        private Lookup<TKey, TInner, TEqualityComparer> lookup;
        private TOuter outer;
        private TInner[] innerGrouping;
        private int innerGroupingIndex, innerGroupingCount;
        private EnumeratorState state;

        public TResult Current
            => state.IsStarted() ? resultSelector.Invoke(outer, innerGrouping[innerGroupingIndex]) : throw Error.EnumerableStateException(state);
        
        public JoinEnumerator(TOuterEnumerator outerEnumerator, IEnumerable<TInner, TInnerKeyEnumerator> innerEnumerable, TOuterKeySelector outerKeySelector,
            TInnerSelector innerKeySelector, TResultSelector resultSelector, TEqualityComparer comparer)
        {
            this.outerEnumerator = outerEnumerator;
            this.innerEnumerable = innerEnumerable;
            this.outerKeySelector = outerKeySelector;
            this.innerKeySelector = innerKeySelector;
            this.resultSelector = resultSelector;
            this.comparer = comparer;
            lookup = null;
            outer = default(TOuter);
            innerGrouping = null;
            innerGroupingIndex = -1;
            innerGroupingCount = 0;
            state = EnumeratorState.Initial;
        }

        public void Dispose() => outerEnumerator.Dispose();

        public bool MoveNext()
        {
            switch (state)
            {
                default:
                    if (++innerGroupingIndex < innerGroupingCount)
                        return true;

                    Grouping<TKey, TInner> g;
                    do
                    {
                        if (!outerEnumerator.MoveNext())
                        {
                            state = EnumeratorState.Ended;
                            return false;
                        }

                        g = lookup[outerKeySelector.Invoke(outer = outerEnumerator.Current)];
                    } while (g.Count == 0);

                    innerGrouping = g.elements;
                    innerGroupingIndex = 0;
                    innerGroupingCount = g.Count;
                    return true;

                case EnumeratorState.Initial:
                    state = EnumeratorState.Started;
                    lookup = Lookup<TKey, TInner, TEqualityComparer>.CreateForJoin(innerEnumerable, innerKeySelector, comparer);
                    goto default;

                case EnumeratorState.Ended:
                    return false;
            }
        }

        public void Reset()
        {
            outerEnumerator.Reset();
            lookup = null;
            outer = default(TOuter);
            innerGrouping = null;
            innerGroupingIndex = -1;
            innerGroupingCount = 0;
        }

        object IEnumerator.Current => Current;
    }
}
