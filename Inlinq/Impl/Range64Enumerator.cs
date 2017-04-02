using System;
using System.Collections;
using System.Collections.Generic;

namespace Inlinq.Impl
{
    public struct Range64Enumerator : IEnumerator<long>
    {
        private long start;
        private long count;
        private long index;

        public long Current
            => index.LtUn(count) ? start + index : throw Error.EnumerableStateException(index.GtUn(count));

        public Range64Enumerator(long start, long count)
        {
            this.start = start;
            this.count = count;
            index = 1 + count;
        }

        public bool MoveNext()
        {
            if (index.LtUn(count))
            {
                ++index;
                return true;
            }
            return false;
        }

        public void Reset()
            => index = 1 + count;

        object IEnumerator.Current => Current;

        void IDisposable.Dispose() { }
    }
}
