using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Inlinq.Impl
{
    public struct Repeat64Enumerator<T> : IEnumerator<T>
    {
        private T element;
        private long count;
        private long i;
        
        public T Current
        {
            get
            {
                if ((ulong)i >= (ulong)count)
                    ThrowStateException();

                return element;
            }
        }

        public Repeat64Enumerator(T element, long count)
        {
            this.element = element;
            this.count = count;
            i = -1;
        }
        
        public bool MoveNext()
        {
            if (++i < count)
                return true;

            i = count;
            return false;
        }

        public void Reset()
            => i = -1;

        object IEnumerator.Current => Current;

        void IDisposable.Dispose() { }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void ThrowStateException()
        {
            throw Error.EnumerableStateException(i < 0);
        }
    }
}
