using Inlinq.Sort;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Inlinq.Impl
{
    public struct OrderedEnumerator<T, TEnumerator> : IEnumerator<T>
        where TEnumerator : IEnumerator<T>
    {
        private IEnumerable<T, TEnumerator> source;
        private IPrimarySort<T> primarySort;
        private Func<int, T> itemAccessor;
        private int endIndex;
        private int index;

        public T Current
            => index.LtUn(endIndex) ? itemAccessor(index) : throw Error.EnumerableStateException(index.GtUn(endIndex));

        internal OrderedEnumerator(IEnumerable<T, TEnumerator> source, IPrimarySort<T> primarySort)
        {
            this.source = source;
            this.primarySort = primarySort;
            itemAccessor = null;
            endIndex = 0;
            index = 1;
        }

        public bool MoveNext()
        {
            switch (1 + endIndex - index)
            {
                default:
                    ++index;
                    return true;

                case 0:
                    return MoveNextSlow();

                case 1:
                    return false;

                case 2:
                    ++index;
                    return false;
            }
        }

        private bool MoveNextSlow()
        {
            itemAccessor = primarySort.Sort(source, out index, out endIndex);
            return endIndex > 0;
        }

        public void Reset()
        {
            itemAccessor = null;
            endIndex = 0;
            index = 1;
        }

        object IEnumerator.Current => Current;

        void IDisposable.Dispose() { }
    }
}
