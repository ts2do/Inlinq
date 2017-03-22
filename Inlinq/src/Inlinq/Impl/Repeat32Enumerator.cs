using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Inlinq.Impl
{
    public struct Repeat32Enumerator<T> : IEnumerator<T>
    {
        private T element;
        private int count;
        private int i;

        public T Current
        {
            get
            {
                if (i.GeUn(count)) ThrowStateException();
                return element;
            }
        }

        public Repeat32Enumerator(T element, int count)
            : this(element, count, true)
        {
            if (count < 0)
                throw Error.ArgumentOutOfRange(nameof(count));
        }

        internal Repeat32Enumerator(T element, int count, bool @unchecked)
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
