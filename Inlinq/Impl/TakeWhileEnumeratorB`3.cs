using System.Collections;
using System.Collections.Generic;

namespace Inlinq.Impl
{
    public struct TakeWhileEnumeratorB<T, TEnumerator, TPredicate> : IEnumerator<T>
        where TEnumerator : IEnumerator<T>
        where TPredicate : IFunctor<T, int, bool>
    {
        private TEnumerator source;
        private TPredicate predicate;
        private EnumeratorState state;
        private T current;
        private int index;

        public T Current
            => state.IsStarted() ? current : throw Error.EnumerableStateException(state);

        public TakeWhileEnumeratorB(TEnumerator source, TPredicate predicate)
        {
            this.source = source;
            this.predicate = predicate;
            state = EnumeratorState.Initial;
            current = default(T);
            index = -1;
        }

        public void Dispose() => source.Dispose();

        public bool MoveNext()
        {
            switch (state)
            {
                case EnumeratorState.Started:
                    if (source.MoveNext() && predicate.Invoke(current = source.Current, checked(++index)))
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
            index = -1;
        }

        object IEnumerator.Current => Current;
    }
}
