using System.Collections;
using System.Collections.Generic;

namespace Inlinq.Impl
{
    public struct DefaultIfEmptyEnumerator<T, TEnumerator> : IEnumerator<T>
        where TEnumerator : IEnumerator<T>
    {
        private enum State { Initial = 0, NotEmpty = 1, CurrentEmpty = 2, Ended = 3 }
        private TEnumerator source;
        private T defaultValue;
        private State state;

        public T Current
            => state != State.CurrentEmpty ? source.Current : defaultValue;

        public DefaultIfEmptyEnumerator(TEnumerator source, T defaultValue)
        {
            this.source = source;
            this.defaultValue = defaultValue;
            state = State.Initial;
        }

        public void Dispose() => source.Dispose();

        public bool MoveNext()
        {
            switch (state)
            {
                default:
                case State.Initial:
                    state = source.MoveNext() ? State.NotEmpty : State.CurrentEmpty;
                    return true;

                case State.NotEmpty:
                    if (source.MoveNext())
                        return true;

                    state = State.Ended;
                    break;

                case State.CurrentEmpty:
                    state = State.Ended;
                    break;
            }

            return false;
        }

        public void Reset()
        {
            source.Reset();
            state = State.Initial;
        }

        object IEnumerator.Current => Current;

    }
}
