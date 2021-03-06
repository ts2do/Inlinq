﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace Inlinq.Impl
{
    public struct SelectManyEnumeratorB<TSource, TResult, TEnumerator1, TEnumerator2> : IEnumerator<TResult>
        where TEnumerator1 : IEnumerator<TSource>
        where TEnumerator2 : IEnumerator<TResult>
    {
        private TEnumerator1 source;
        private Func<TSource, int, IEnumerable<TResult, TEnumerator2>> selector;
        private TEnumerator2 resultEnumerator;
        private EnumeratorState state;
        private int index;

        public TResult Current
            => state.IsStarted() ? resultEnumerator.Current : throw Error.EnumerableStateException(state);

        public SelectManyEnumeratorB(TEnumerator1 source, Func<TSource, int, IEnumerable<TResult, TEnumerator2>> selector)
        {
            this.source = source;
            this.selector = selector;
            resultEnumerator = default(TEnumerator2);
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
                    if (resultEnumerator.MoveNext())
                        return true;

                    resultEnumerator.Dispose();

                    while (source.MoveNext())
                    {
                        // keep querying source until a non-empty IEnumerator<> is acquired
                        var re1 = selector(source.Current, checked(++index));
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
            state = EnumeratorState.Initial;
            index = -1;
        }

        object IEnumerator.Current => Current;
    }
}