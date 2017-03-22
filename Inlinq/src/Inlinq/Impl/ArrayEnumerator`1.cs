using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Inlinq.Impl
{
    public struct ArrayEnumerator<T> : IEnumerator<T>
    {
        private readonly T[] array;
        private int index;
        private int startIndex;
        private int endIndex;
        
        public T Current
        {
            get
            {
                if (endIndex.LeUn(index - startIndex)) ThrowStateException();
                return array[index];
            }
        }

        public ArrayEnumerator(T[] array, int startIndex, int length)
        {
            this.array = array;
            index = startIndex - 1;
            this.startIndex = startIndex;
            endIndex = length;
        }

        public bool MoveNext()
        {
            switch (++index - endIndex)
            {
                default: return true;
                case 0: return false;
                case 1: --index; return false;
            }
        }

        public void Reset()
            => index = startIndex - 1;

        object IEnumerator.Current => Current;

        void IDisposable.Dispose() { }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void ThrowStateException()
        {
            throw Error.EnumerableStateException(index < 0);
        }
    }
}
