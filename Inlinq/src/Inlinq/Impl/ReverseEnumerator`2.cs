using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

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
            index = -1;
        }

        public T Current
        {
            get
            {
                if (index.GeUn(buffer.count)) ThrowStateException();
                return buffer.items[index];
            }
        }

        public bool MoveNext()
        {
            if (buffer.items == null)
                buffer.Fill(source);
            switch (++index - buffer.count)
            {
                case 0: return false;
                case 1: --index; return false;
                default: return true;
            }
        }

        public void Reset()
        {
            buffer.Clear();
            index = -1;
        }

        object IEnumerator.Current => Current;

        void IDisposable.Dispose() { }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void ThrowStateException()
        {
            throw Error.EnumerableStateException(index < 0);
        }
    }
}
