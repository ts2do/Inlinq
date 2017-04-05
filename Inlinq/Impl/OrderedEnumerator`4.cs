using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Inlinq.Impl
{
    public struct OrderedEnumerator<T, TEnumerator, TKey, TComparer> : IEnumerator<T>
        where TEnumerator : IEnumerator<T>
        where TComparer : IComparer<TKey>
    {
        private IEnumerable<T, TEnumerator> source;
        private TComparer comparer;
        private Func<T, TKey> keySelector;
        private Pair[] items;
        private int count;
        private int index;

        public T Current
            => index.LtUn(count) ? items[items[index].index].element : throw Error.EnumerableStateException(index.GtUn(count));

        public OrderedEnumerator(IEnumerable<T, TEnumerator> source, Func<T, TKey> keySelector, TComparer comparer)
        {
            this.source = source;
            this.keySelector = keySelector;
            this.comparer = comparer;
            items = null;
            count = 0;
            index = 1;
        }

        public bool MoveNext()
        {
            switch (1 + count - index)
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
            if (!source.GetCount(out int actualCount))
                actualCount = -1;

            if (actualCount != 0)
            {
                int capacity = 0;
                using (var e = source.GetEnumerator())
                {
                    if (e.MoveNext())
                    {
                        capacity = actualCount < 0 ? 10 : actualCount;
                        items = new Pair[capacity];
                        count = 0;
                        do
                        {
                            if (count == capacity)
                                Array.Resize(ref items, checked(capacity *= 2));

                            var p = new Pair { element = e.Current, index = count };
                            p.key = keySelector(p.element);
                            items[count++] = p;
                        } while (e.MoveNext());

                        QuickSort(ref comparer, items, 0, count - 1);
                    }
                    else
                    {
                        count = capacity = 0;
                    }
                }
            }
            else
                count = 0;

            index = 0;
            return count > 0;
        }

        private void QuickSort(ref TComparer comparer, Pair[] items, int left, int right)
        {
            do
            {
                Partition(ref comparer, items, left, right, out int i, out int j);
                if (j - left <= right - i)
                {
                    if (left < j)
                        QuickSort(ref comparer, items, left, j);
                    left = i;
                }
                else
                {
                    if (i < right)
                        QuickSort(ref comparer, items, i, right);
                    right = j;
                }
            } while (left < right);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Partition(ref TComparer comparer, Pair[] items, int left, int right, out int i, out int j)
        {
            i = left;
            j = right;
            TKey x = items[items[i + ((j - i) >> 1)].index].key;
            do
            {
                while (i <= right && comparer.Compare(x, items[items[i].index].key) > 0)
                    ++i;
                while (j >= left && comparer.Compare(x, items[items[j].index].key) < 0)
                    --j;
                switch (j - i)
                {
                    default:
                        if (i > j)
                            break;

                        Util.Swap(ref items[i].index, ref items[j].index);
                        break;

                    case 0:
                        ++i;
                        --j;
                        return;

                    case 1:
                        Util.Swap(ref items[i].index, ref items[j].index);
                        goto case 0;
                }
            } while (++i <= --j);
        }

        public void Reset()
        {
            items = null;
            count = 0;
            index = 1;
        }

        object IEnumerator.Current => Current;

        void IDisposable.Dispose() { }

        private struct Pair
        {
            public T element;
            public TKey key;
            public int index;
        }
    }
}
