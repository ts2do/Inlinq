using System;
using System.Collections;
using System.Collections.Generic;

namespace Inlinq.Impl
{
    public struct ArrayEnumerator<T> : IEnumerator<T>
    {
        private readonly T[] array;
        private int offset;
        private int length;
        private int index;
        
        public T Current
            => index.LtUn(length) ? array[index] : throw Error.EnumerableStateException(index.GtUn(length));

        public ArrayEnumerator(T[] array, int offset, int length)
        {
            this.array = array;
            this.offset = offset;
            this.length = length;
            index = 1 + length;
        }

        public bool MoveNext()
        {
            switch (1 + length - index)
            {
                default:
                    ++index;
                    return true;

                case 0:
                    index = offset;
                    return offset < length;

                case 1:
                    return false;

                case 2:
                    ++index;
                    return false;
            }
        }

        public void Reset()
            => index = 1 + length;

        object IEnumerator.Current => Current;

        void IDisposable.Dispose() { }
    }
}
