using System;
using System.Collections;
using System.Collections.Generic;

namespace Inlinq.Impl
{
    public struct ReverseEnumerator<T, TEnumerator> : IEnumerator<T>
        where TEnumerator : IEnumerator<T>
    {
        private IEnumerable<T, TEnumerator> source;
        private ArrayBuffer<T, TEnumerator> buffer;
        private int index;

        public ReverseEnumerator(IEnumerable<T, TEnumerator> source)
        {
            this.source = source;
            buffer = default(ArrayBuffer<T, TEnumerator>);
            index = 1;
        }

        public T Current
            => index.LtUn(buffer.count) ? buffer.items[index] : throw Error.EnumerableStateException(index.GtUn(buffer.count));

        public bool MoveNext()
        {
            if (buffer.items == null)
                buffer.Fill(source);

            switch (1 + buffer.count - index)
            {
                default:
                    ++index;
                    return true;

                case 0:
                    break;

                case 1:
                    return false;

                case 2:
                    ++index;
                    return false;
            }

            buffer.Fill(source);
            index = 0;
            return buffer.count > 0;
        }

        public void Reset()
        {
            buffer.Clear();
            index = 1;
        }

        object IEnumerator.Current => Current;

        void IDisposable.Dispose() { }
    }
}
