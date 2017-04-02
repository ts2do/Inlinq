using System;
using System.Collections;
using System.Collections.Generic;

namespace Inlinq.Impl
{
    public struct OrderedEnumerator<T, TEnumerator, TKey, TKeySelector, TComparer> : IEnumerator<T>
        where TEnumerator : IEnumerator<T>
        where TKeySelector : IFunctor<T, TKey>
        where TComparer : IComparer<TKey>
    {
        private IEnumerable<T, TEnumerator> source;
        private TComparer comparer;
        private TKeySelector keySelector;
        private Pair[] items;
        private int count;
        private int index;

        public T Current
            => index.LtUn(count) ? items[index].element : throw Error.EnumerableStateException(index.GtUn(count));

        public OrderedEnumerator(IEnumerable<T, TEnumerator> source, TKeySelector keySelector, TComparer comparer)
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
                    break;

                case 1:
                    return false;

                case 2:
                    ++index;
                    return false;
            }

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

                            var p = new Pair { element = e.Current };
                            p.key = keySelector.Invoke(p.element);
                            items[count++] = p;
                        } while (e.MoveNext());

                        QuickSort(ref comparer, items, count, 0, count - 1);
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

        private void QuickSort(ref TComparer comparer, Pair[] items, int count, int left, int right)
        {
            do
            {
                int i = left, j = right;
                TKey x = items[i + ((j - i) >> 1)].key;
                do
                {
                    while (i < count && comparer.Compare(x, items[i].key) > 0)
                        ++i;
                    while (j >= 0 && comparer.Compare(x, items[j].key) < 0)
                        --j;
                    if (i > j) break;
                    if (i < j)
                    {
                        ref Pair a = ref items[i], b = ref items[j];
                        Pair temp = a;
                        a = b;
                        b = temp;
                    }
                } while (++i <= --j);
                if (j - left <= right - i)
                {
                    if (left < j)
                        QuickSort(ref comparer, items, count, left, j);
                    left = i;
                }
                else
                {
                    if (i < right)
                        QuickSort(ref comparer, items, count, i, right);
                    right = j;
                }
            } while (left < right);
        }

        public void Reset()
        {
            items = null;
            count = 0;
            index = 1;
        }

        object IEnumerator.Current => Current;

        void IDisposable.Dispose() { }

        private sealed class Pair
        {
            public T element;
            public TKey key;
        }
    }
}
