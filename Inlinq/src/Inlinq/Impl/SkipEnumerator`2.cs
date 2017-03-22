using System.Collections;
using System.Collections.Generic;

namespace Inlinq.Impl
{
    public struct SkipEnumerator<T, TEnumerator> : IEnumerator<T>
        where TEnumerator : IEnumerator<T>
    {
        private TEnumerator source;
        private int count;
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

        public SkipEnumerator(TEnumerator source, int count)
        {
            this.source = source;
            this.count = count;
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
                    while (++index < count)
                    {
                        if (!source.MoveNext())
                        {
                            state = EnumeratorState.Ended;
                            return false;
                        }
                    }

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
