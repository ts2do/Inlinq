using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Inlinq.Impl
{
    public struct Range64Enumerator : IEnumerator<long>
    {
        private long first;
        private long current;
        private long last;
        
        public long Current
        {
            get
            {
                if ((ulong)(current - first) > (ulong)last)
                    ThrowStateException();

                return current;
            }
        }

        public Range64Enumerator(long first, long last)
        {
            this.first = first;
            this.last = last;
            current = first - 1;
        }

        public bool MoveNext()
        {
            if (current >= last)
                return false;

            ++current;
            return true;
        }

        public void Reset()
            => current = first - 1;

        object IEnumerator.Current => Current;

        void IDisposable.Dispose() { }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void ThrowStateException()
        {
            throw Error.EnumerableStateException(current - first < 0); // also handles first == long.MinValue
        }
    }
}
