using System;
using System.Collections;
using System.Collections.Generic;

namespace Inlinq.Impl
{
    public struct Repeat32Enumerator<T> : IEnumerator<T>
    {
        private T element;
        private int count;
        private int index;

        public T Current
            => index.LtUn(count) ? element : throw Error.EnumerableStateException(index.GtUn(count));

        public Repeat32Enumerator(T element, int count)
        {
            this.element = element;
            this.count = count;
            index = 1 + count;
        }
        
        public bool MoveNext()
        {
            switch (1 + count - index)
            {
                default:
                    ++index;
                    return true;

                case 0:
                    index = 0;
                    return count > 0;

                case 1:
                    return false;

                case 2:
                    ++index;
                    return false;
            }
        }

        public void Reset()
            => index = 1 + count;

        object IEnumerator.Current => Current;

        void IDisposable.Dispose() { }
    }
}
