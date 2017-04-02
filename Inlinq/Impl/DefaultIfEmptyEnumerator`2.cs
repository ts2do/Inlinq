using System.Collections;
using System.Collections.Generic;

namespace Inlinq.Impl
{
    public struct DefaultIfEmptyEnumerator<T, TEnumerator> : IEnumerator<T>
        where TEnumerator : IEnumerator<T>
    {
        private TEnumerator source;
        private T defaultValue;
        private EnumeratorState state;
        private bool hasAny;

        public T Current
            => state.IsStarted() ? hasAny ? source.Current : defaultValue : throw Error.EnumerableStateException(state);

        public DefaultIfEmptyEnumerator(TEnumerator source, T defaultValue)
        {
            this.source = source;
            this.defaultValue = defaultValue;
            state = EnumeratorState.Initial;
            hasAny = false;
        }

        public void Dispose() => source.Dispose();

        public bool MoveNext()
        {
            bool hasNext = source.MoveNext();

            switch (state)
            {
                default:
                    if (hasNext)
                        return true;

                    state = EnumeratorState.Ended;
                    return false;

                case EnumeratorState.Initial:
                    state = EnumeratorState.Started;
                    hasAny = hasNext;
                    return true;

                case EnumeratorState.Ended:
                    return false;
            }
        }

        public void Reset()
        {
            source.Reset();
            state = EnumeratorState.Initial;
        }

        object IEnumerator.Current => Current;

    }
}
