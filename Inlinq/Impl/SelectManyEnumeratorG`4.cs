using System;
using System.Collections;
using System.Collections.Generic;

namespace Inlinq.Impl
{
    public struct SelectManyEnumeratorG<TSource, TCollection, TResult, TEnumerator> : IEnumerator<TResult>
        where TEnumerator : IEnumerator<TSource>
    {
        private TEnumerator source;
        private Func<TSource, IEnumerable<TCollection>> collectionSelector;
        private Func<TSource, TCollection, TResult> resultSelector;
        private IEnumerator<TCollection> resultEnumerator;
        private TSource currentSource;
        private EnumeratorState state;

        public TResult Current
            => state.IsStarted() ? resultSelector(currentSource, resultEnumerator.Current) : throw Error.EnumerableStateException(state);

        public SelectManyEnumeratorG(TEnumerator source, Func<TSource, IEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
        {
            this.source = source;
            this.collectionSelector = collectionSelector;
            this.resultSelector = resultSelector;
            resultEnumerator = null;
            currentSource = default(TSource);
            state = EnumeratorState.Initial;
        }

        public void Dispose()
        {
            source.Dispose();
            resultEnumerator?.Dispose();
        }

        public bool MoveNext()
        {
            switch (state)
            {
                default:
                    IEnumerator<TCollection> re;
                    if ((re = resultEnumerator).MoveNext())
                        return true;

                    re.Dispose();

                    while (source.MoveNext())
                    {
                        // keep querying source until a non-empty IEnumerator<> is acquired
                        var re1 = collectionSelector(currentSource = source.Current);
                        if (re1 != null)
                        {
                            var re2 = re1.GetEnumerator();
                            if (re2.MoveNext())
                            {
                                resultEnumerator = re2;
                                return true;
                            }

                            re2.Dispose();
                        }
                    }

                    resultEnumerator = null;
                    state = EnumeratorState.Ended;
                    return false;

                case EnumeratorState.Initial:
                    state = EnumeratorState.Started;
                    goto default;

                case EnumeratorState.Ended:
                    return false;
            }
        }

        public void Reset()
        {
            source.Reset();
            if (resultEnumerator != null)
            {
                resultEnumerator.Dispose();
                resultEnumerator = null;
            }
            currentSource = default(TSource);
            state = EnumeratorState.Initial;
        }

        object IEnumerator.Current => Current;
    }
}