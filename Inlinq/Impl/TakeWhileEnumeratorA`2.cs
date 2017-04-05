using System;
using System.Collections;
using System.Collections.Generic;

namespace Inlinq.Impl
{
    public struct TakeWhileEnumeratorA<T, TEnumerator> : IEnumerator<T>
        where TEnumerator : IEnumerator<T>
    {
        private TEnumerator source;
        private Func<T, bool> predicate;
        private EnumeratorState state;
        private T current;

        public T Current
            => state.IsStarted() ? current : throw Error.EnumerableStateException(state);

        public TakeWhileEnumeratorA(TEnumerator source, Func<T, bool> predicate)
        {
            this.source = source;
            this.predicate = predicate;
            state = EnumeratorState.Initial;
            current = default(T);
        }

        public void Dispose() => source.Dispose();

        public bool MoveNext()
        {
            switch (state)
            {
                case EnumeratorState.Started:
                    if (source.MoveNext() && predicate(current = source.Current))
                        return true;

                    state = EnumeratorState.Ended;
                    break;

                case EnumeratorState.Initial:
                    state = EnumeratorState.Started;
                    goto case EnumeratorState.Started;

                case EnumeratorState.Ended:
                    break;
            }

            return false;
        }

        public void Reset()
        {
            source.Reset();
            state = EnumeratorState.Initial;
        }

        object IEnumerator.Current => Current;
    }
}
