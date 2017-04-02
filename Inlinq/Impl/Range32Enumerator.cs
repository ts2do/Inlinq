using System;
using System.Collections;
using System.Collections.Generic;

namespace Inlinq.Impl
{
    public struct Range32Enumerator : IEnumerator<int>
    {
        private int start;
        private int count;
        private int index;

        public int Current
            => index.LtUn(count) ? start + index : throw Error.EnumerableStateException(index.GtUn(count));

        public Range32Enumerator(int start, int count)
        {
            this.start = start;
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
