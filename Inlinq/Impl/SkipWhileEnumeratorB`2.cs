﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace Inlinq.Impl
{
    public struct SkipWhileEnumeratorB<T, TEnumerator> : IEnumerator<T>
        where TEnumerator : IEnumerator<T>
    {
        private TEnumerator source;
        private Func<T, int, bool> predicate;
        private EnumeratorState state;
        private int index;

        public T Current
            => state.IsStarted() ? source.Current : throw Error.EnumerableStateException(state);

        public SkipWhileEnumeratorB(TEnumerator source, Func<T, int, bool> predicate)
        {
            this.source = source;
            this.predicate = predicate;
            state = EnumeratorState.Initial;
            index = -1;
        }

        public void Dispose() => source.Dispose();

        public bool MoveNext()
        {
            switch (state)
            {
                case EnumeratorState.Started:
                    if (source.MoveNext())
                        return true;

                    state = EnumeratorState.Ended;
                    break;

                case EnumeratorState.Initial:
                    state = EnumeratorState.Started;
                    while (source.MoveNext())
                        if (!predicate(source.Current, checked(++index)))
                            goto case EnumeratorState.Started;

                    state = EnumeratorState.Ended;
                    break;

                case EnumeratorState.Ended:
                    break;
            }

            return false;
        }

        public void Reset()
        {
            source.Reset();
            state = EnumeratorState.Initial;
            index = -1;
        }

        object IEnumerator.Current => Current;
    }
}
