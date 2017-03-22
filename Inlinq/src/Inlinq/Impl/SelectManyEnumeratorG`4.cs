using System.Collections;
using System.Collections.Generic;

namespace Inlinq.Impl
{
    public struct SelectManyEnumeratorG<TSource, TCollection, TResult, TEnumerator, TCollectionSelector, TResultSelector> : IEnumerator<TResult>
        where TEnumerator : IEnumerator<TSource>
        where TCollectionSelector : IFunctor<TSource, IEnumerable<TCollection>>
        where TResultSelector : IFunctor<TSource, TCollection, TResult>
    {
        private TEnumerator source;
        private TCollectionSelector collectionSelector;
        private TResultSelector resultSelector;
        private IEnumerator<TCollection> resultEnumerator;
        private TSource currentSource;
        private EnumeratorState state;

        public TResult Current
        {
            get
            {
                state.AssertState();
                return resultSelector.Invoke(currentSource, resultEnumerator.Current);
            }
        }

        public SelectManyEnumeratorG(TEnumerator source, TCollectionSelector collectionSelector, TResultSelector resultSelector)
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
                        var re1 = collectionSelector.Invoke(currentSource = source.Current);
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