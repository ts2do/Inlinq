using System;
using System.Collections;
using System.Collections.Generic;

namespace Inlinq.Impl
{
    public struct EmptyEnumerator<T> : IEnumerator<T>
    {
        private bool pastEnd;
        public T Current
            => throw Error.EnumerableStateException(!pastEnd);

        public bool MoveNext()
        {
            pastEnd = true;
            return false;
        }

        public void Reset() => pastEnd = false;

        object IEnumerator.Current => Current;

        void IDisposable.Dispose() { }
    }
}
