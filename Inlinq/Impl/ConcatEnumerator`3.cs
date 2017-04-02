using System.Collections;
using System.Collections.Generic;

namespace Inlinq.Impl
{
    public struct ConcatEnumerator<T, TEnumerator1, TEnumerator2> : IEnumerator<T>
        where TEnumerator1 : IEnumerator<T>
        where TEnumerator2 : IEnumerator<T>
    {
        private TEnumerator1 first;
        private TEnumerator2 second;
        private bool firstComplete;

        public T Current
            => firstComplete ? second.Current : first.Current;

        public ConcatEnumerator(TEnumerator1 first, TEnumerator2 second)
        {
            this.first = first;
            this.second = second;
            firstComplete = false;
        }

        public void Dispose()
        {
            first.Dispose();
            second.Dispose();
        }

        public bool MoveNext()
        {
            if (!firstComplete)
            {
                if (first.MoveNext())
                    return true;
                firstComplete = true;
            }
            return second.MoveNext();
        }

        public void Reset()
        {
            first.Reset();
            if (firstComplete)
            {
                second.Reset();
                firstComplete = false;
            }
        }

        object IEnumerator.Current => Current;
    }
}
