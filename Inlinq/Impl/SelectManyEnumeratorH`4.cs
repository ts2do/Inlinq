﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace Inlinq.Impl
{
    public struct SelectManyEnumeratorH<TSource, TCollection, TResult, TEnumerator1> : IEnumerator<TResult>
        where TEnumerator1 : IEnumerator<TSource>
    {
        private TEnumerator1 source;
        private Func<TSource, int, IEnumerable<TCollection>> collectionSelector;
        private Func<TSource, TCollection, TResult> resultSelector;
        private IEnumerator<TCollection> resultEnumerator;
        private TSource currentSource;
        private EnumeratorState state;
        private int index;

        public TResult Current
            => state.IsStarted() ? resultSelector(currentSource, resultEnumerator.Current) : throw Error.EnumerableStateException(state);

        public SelectManyEnumeratorH(TEnumerator1 source, Func<TSource, int, IEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
        {
            this.source = source;
            this.collectionSelector = collectionSelector;
            this.resultSelector = resultSelector;
            resultEnumerator = null;
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
                        var re1 = collectionSelector(currentSource = source.Current, checked(++index));
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
            index = -1;
        }

        object IEnumerator.Current => Current;
    }
}