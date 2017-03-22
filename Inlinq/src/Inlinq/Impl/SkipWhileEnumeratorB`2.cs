﻿using System.Collections;
using System.Collections.Generic;

namespace Inlinq.Impl
{
    public struct SkipWhileEnumeratorB<T, TEnumerator, TPredicate> : IEnumerator<T>
        where TEnumerator : IEnumerator<T>
        where TPredicate : IFunctor<T, int, bool>
    {
        private TEnumerator source;
        private TPredicate predicate;
        private EnumeratorState state;
        private int index;

        public T Current
        {
            get
            {
                state.AssertState();
                return source.Current;
            }
        }

        public SkipWhileEnumeratorB(TEnumerator source, TPredicate predicate)
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
                        if (!predicate.Invoke(source.Current, checked(++index)))
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
