using System;
using System.Collections;
using System.Collections.Generic;

namespace Inlinq
{
    public struct GroupingEnumerator<TKey, TElement> : IEnumerator<Grouping<TKey, TElement>>
    {
        private Grouping<TKey, TElement> current;
        private Grouping<TKey, TElement> lastGrouping;
        private EnumeratorState state;

        public GroupingEnumerator(Grouping<TKey, TElement> lastGrouping)
        {
            current = null;
            this.lastGrouping = lastGrouping;
            state = EnumeratorState.Initial;
        }

        public Grouping<TKey, TElement> Current
        {
            get
            {
                state.AssertState();
                return current;
            }
        }

        public bool MoveNext()
        {
            switch (state)
            {
                case EnumeratorState.Initial:
                    if (lastGrouping == null)
                    {
                        state = EnumeratorState.Ended;
                        return false;
                    }
                    state = EnumeratorState.Started;
                    current = lastGrouping.next;
                    return true;

                case EnumeratorState.Ended:
                    return false;

                default:
                    current = lastGrouping.next;
                    if (current != lastGrouping)
                        return true;

                    state = EnumeratorState.Ended;
                    return false;
            }
        }

        public void Reset()
        {
            current = lastGrouping;
            state = EnumeratorState.Initial;
        }

        object IEnumerator.Current => Current;

        void IDisposable.Dispose() { }
    }
}
