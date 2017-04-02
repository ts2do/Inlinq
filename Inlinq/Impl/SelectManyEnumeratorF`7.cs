using System.Collections;
using System.Collections.Generic;

namespace Inlinq.Impl
{
    public struct SelectManyEnumeratorF<TSource, TCollection, TResult, TEnumerator1, TEnumerator2, TCollectionSelector, TResultSelector> : IEnumerator<TResult>
        where TEnumerator1 : IEnumerator<TSource>
        where TEnumerator2 : IEnumerator<TCollection>
        where TCollectionSelector : IFunctor<TSource, int, IEnumerable<TCollection, TEnumerator2>>
        where TResultSelector : IFunctor<TSource, TCollection, TResult>
    {
        private TEnumerator1 source;
        private TCollectionSelector collectionSelector;
        private TResultSelector resultSelector;
        private TEnumerator2 resultEnumerator;
        private TSource currentSource;
        private EnumeratorState state;
        private int index;

        public TResult Current
            => state.IsStarted() ? resultSelector.Invoke(currentSource, resultEnumerator.Current) : throw Error.EnumerableStateException(state);

        public SelectManyEnumeratorF(TEnumerator1 source, TCollectionSelector collectionSelector, TResultSelector resultSelector)
        {
            this.source = source;
            this.collectionSelector = collectionSelector;
            this.resultSelector = resultSelector;
            resultEnumerator = default(TEnumerator2);
            currentSource = default(TSource);
            state = EnumeratorState.Initial;
            index = -1;
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
                        var re1 = collectionSelector.Invoke(currentSource = source.Current, checked(++index));
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

                    resultEnumerator = default(TEnumerator2);
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
                resultEnumerator = default(TEnumerator2);
            }
            currentSource = default(TSource);
            state = EnumeratorState.Initial;
            index = -1;
        }

        object IEnumerator.Current => Current;
    }
}