using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Inlinq.Impl
{
    public struct Range32Enumerator : IEnumerator<int>
    {
        private int first;
        private int current;
        private int last;
        
        public int Current
        {
            get
            {
                if (last.LtUn(current - first)) ThrowStateException();
                return current;
            }
        }

        public Range32Enumerator(int first, int last)
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
            throw Error.EnumerableStateException(current - first < 0); // also handles first == int.MinValue
        }
    }
}
