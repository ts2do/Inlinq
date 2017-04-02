using System.Collections;
using System.Collections.Generic;

namespace Inlinq.Impl
{
    public struct ExceptEnumerator<T, TEnumerator1, TEnumerator2, TEqualityComparer> : IEnumerator<T>
        where TEnumerator1 : IEnumerator<T>
        where TEnumerator2 : IEnumerator<T>
        where TEqualityComparer : IEqualityComparer<T>
    {
        private TEnumerator1 first;
        private TEnumerator2 second;
        private T current;
        private Set<T, TEqualityComparer> set;
        private EnumeratorState state;

        public ExceptEnumerator(TEnumerator1 first, TEnumerator2 second, TEqualityComparer comparer)
        {
            this.first = first;
            this.second = second;
            current = default(T);
            set = new Set<T, TEqualityComparer>(comparer);
            state = EnumeratorState.Initial;
        }

        public T Current
            => state.IsStarted() ? current : throw Error.EnumerableStateException(state);

        public void Dispose()
        {
            first.Dispose();
            second.Dispose();
        }

        public bool MoveNext()
        {
            switch (state)
            {
                default:
                    while (first.MoveNext())
                    {
                        var x = first.Current;
                        if (set.Add(x))
                        {
                            current = x;
                            return true;
                        }
                    }
                    state = EnumeratorState.Ended;
                    return false;

                case EnumeratorState.Initial:
                    while (second.MoveNext())
                        set.Add(second.Current);
                    state = EnumeratorState.Started;
                    goto default;

                case EnumeratorState.Ended:
                    return false;
            }
        }

        public void Reset()
        {
            first.Reset();
            second.Reset();
            set.Clear();
            state = EnumeratorState.Initial;
        }

        object IEnumerator.Current => Current;
    }
}
