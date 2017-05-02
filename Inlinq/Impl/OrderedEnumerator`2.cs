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
        private SortedArray<T> items;
        private int index;

        public T Current
            => index.LtUn(items.count) ? items.items[index].element : throw Error.EnumerableStateException(index.GtUn(items.count));

        internal OrderedEnumerator(IEnumerable<T, TEnumerator> source, IPrimarySort<T> primarySort)
        {
            this.source = source;
            this.primarySort = primarySort;
            items = new SortedArray<T>();
            index = 1;
        }

        public bool MoveNext()
        {
            switch (1 + items.count - index)
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
            items = primarySort.Sort(source);
            index = 0;
            return items.count > 0;
        }

        public void Reset()
        {
            items = new SortedArray<T>();
            index = 1;
        }

        object IEnumerator.Current => Current;

        void IDisposable.Dispose() { }
    }
}
