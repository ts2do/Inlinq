using System.Collections;
using System.Collections.Generic;

namespace Inlinq.Impl
{
    public struct TakeEnumerator<T, TEnumerator> : IEnumerator<T>
        where TEnumerator : IEnumerator<T>
    {
        private TEnumerator source;
        private int count;
        private int index;

        public T Current
            => index.LtUn(count) ? source.Current : throw Error.EnumerableStateException(index < 0);

        public TakeEnumerator(TEnumerator source, int count)
        {
            this.source = source;
            this.count = count;
            index = -1;
        }

        public void Dispose() => source.Dispose();

        public bool MoveNext()
        {
            if (index != count)
            {
                if (source.MoveNext())
                {
                    ++index;
                    return true;
                }

                index = count;
            }

            return false;
        }

        public void Reset()
        {
            source.Reset();
            index = -1;
        }

        object IEnumerator.Current => Current;
    }
}
