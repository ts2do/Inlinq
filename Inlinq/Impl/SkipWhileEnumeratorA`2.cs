using System.Collections;
using System.Collections.Generic;

namespace Inlinq.Impl
{
    public struct SkipWhileEnumeratorA<T, TEnumerator, TPredicate> : IEnumerator<T>
        where TEnumerator : IEnumerator<T>
        where TPredicate : IFunctor<T, bool>
    {
        private TEnumerator source;
        private TPredicate predicate;
        private EnumeratorState state;

        public T Current
            => state.IsStarted() ? source.Current : throw Error.EnumerableStateException(state);

        public SkipWhileEnumeratorA(TEnumerator source, TPredicate predicate)
        {
            this.source = source;
            this.predicate = predicate;
            state = EnumeratorState.Initial;
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
                        if (!predicate.Invoke(source.Current))
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
        }

        object IEnumerator.Current => Current;
    }
}
