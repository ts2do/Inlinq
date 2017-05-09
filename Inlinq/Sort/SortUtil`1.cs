using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Inlinq.Sort
{
    internal static class SortUtil<T>
    {
        public static Func<int, T> Sort<TEnumerator, TSort, TData>(IEnumerable<T, TEnumerator> source, TSort sort, TData data, out int startIndex, out int endIndex)
            where TEnumerator : IEnumerator<T>
            where TSort : ISort<T, TData>
        {
            Sorter<TEnumerator, TSort, TData> sorter = new Sorter<TEnumerator, TSort, TData> { sort = sort };
            sorter.Sort(source);
            startIndex = 0;
            endIndex = sorter.count;
            SortElement<T, TData>[] items = sorter.items;
            return i => items[i].element;
        }

        private struct Sorter<TEnumerator, TSort, TData>
            where TEnumerator : IEnumerator<T>
            where TSort : ISort<T, TData>
        {
            public TSort sort;
            public int count;

            public SortElement<T, TData>[] items;

            public void Sort(IEnumerable<T, TEnumerator> source)
            {
                if (!source.GetCount(out int capacity))
                    capacity = 10;

                count = 0;
                if (capacity > 0)
                {
                    using (TEnumerator enumerator = source.GetEnumerator())
                    {
                        if (enumerator.MoveNext())
                        {
                            items = new SortElement<T, TData>[capacity];
                            do
                            {
                                if (count == capacity)
                                    Array.Resize(ref items, checked(capacity *= 2));

                                ref SortElement<T, TData> p = ref items[count];
                                p.element = enumerator.Current;
                                sort.GetData(ref p.element, out p.data);
                                ++count;
                            } while (enumerator.MoveNext());
                        }
                    }

                    if (count > 1)
                    {
                        // Add a try block here to detect IComparers (or their
                        // underlying IComparables, etc) that are bogus.
                        try
                        {
                            IntroSort(0, count - 1, 2 * FloorLog2(count));
                        }
                        catch (IndexOutOfRangeException)
                        {
                            // This is hit when an invarant of QuickSort is violated due to a bad IComparer implementation (for
                            // example, imagine an IComparer that returns 0 when items are equal but -1 all other times).
                            //
                            // We could have thrown this exception on v4, but due to changes in v4.5 around how we partition arrays
                            // there are different sets of input where we would throw this exception.  In order to reduce overall risk from
                            // an app compat persective, we're changing to never throw on v4.  Instead, we'll return with a partially
                            // sorted array.
                            throw new ArgumentException();//Environment.GetResourceString("Arg_BogusIComparer", comparer));
                        }
                        catch (Exception e)
                        {
                            throw new InvalidOperationException();//Environment.GetResourceString("InvalidOperation_IComparerFailed"), e);
                        }
                    }
                }
            }

            public static int FloorLog2(int n)
            {
                int result = 0;
                while (n >= 1)
                {
                    ++result;
                    n >>= 1;
                }
                return result;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private void SwapIfGreater(int a, int b)
            {
                ref SortElement<T, TData> first = ref items[a], second = ref items[b];
                if (Compare(ref first, ref second) > 0)
                    Util.Swap(ref first, ref second);
            }

            private void Swap(int i, int j)
            {
                Util.Swap(ref items[i], ref items[j]);
            }

            private void IntroSort(int lo, int hi, int depthLimit)
            {
                while (true)
                {
                    int hilo = hi - lo;
                    switch (hilo)
                    {
                        default:
                            if (hi < lo)
                                return;

                            if (depthLimit > 0)
                            {
                                int p = PickPivotAndPartition(lo, hi);
                                // Note we've already partitioned around the pivot and do not have to move the pivot again.
                                IntroSort(p + 1, hi, --depthLimit);
                                hi = p - 1;
                                break;
                            }

                            Heapsort(lo, hi);
                            return;

                        case 0:
                            return;

                        case 1:
                            SwapIfGreater(lo, hi);
                            return;

                        case 2:
                            SwapIfGreater(lo, hi - 1);
                            SwapIfGreater(lo, hi);
                            SwapIfGreater(hi - 1, hi);
                            return;

                        case 3:case 4:case 5:case 6:case 7:
                        case 8:case 9:case 10:case 11:case 12:
                        case 13:case 14:case 15:
                            InsertionSort(lo, hi);
                            return;
                    }
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private int Compare(ref SortElement<T, TData> left, ref SortElement<T, TData> right)
                => sort.Compare(ref left.element, ref left.data, ref right.element, ref right.data);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private int PickPivotAndPartition(int lo, int hi)
            {
                // Compute median-of-three.  But also partition them, since we've done the comparison.
                int middle = lo + ((hi - lo) / 2);

                // Sort lo, mid and hi appropriately, then pick mid as the pivot.
                SwapIfGreater(lo, middle); // swap the low with the mid point
                SwapIfGreater(lo, hi);     // swap the low with the high
                SwapIfGreater(middle, hi); // swap the middle with the high

                ref SortElement<T, TData> pivot = ref items[middle];
                Swap(middle, hi - 1);
                int left = lo, right = hi - 1;  // We already partitioned lo and hi and put the pivot in hi - 1.  And we pre-increment & decrement below.

                while (left < right)
                {
                    while (Compare(ref items[++left], ref pivot) < 0) ;
                    while (Compare(ref pivot, ref items[--right]) < 0) ;

                    if (left >= right)
                        break;

                    Swap(left, right);
                }

                // Put pivot in the right location.
                Swap(left, hi - 1);
                return left;
            }

            [MethodImpl(MethodImplOptions.NoInlining)]
            private void Heapsort(int lo, int hi)
            {
                int n = hi - lo + 1;
                for (int i = n / 2; i >= 1; i = i - 1)
                {
                    DownHeap(i, n, lo);
                }
                for (int i = n; i > 1; i = i - 1)
                {
                    Swap(lo, lo + i - 1);
                    DownHeap(1, i - 1, lo);
                }
            }

            private void DownHeap(int i, int n, int lo)
            {
                ref SortElement<T, TData> d = ref items[lo + i - 1];
                int child;
                while (i <= n / 2)
                {
                    child = 2 * i;
                    if (child < n && Compare(ref items[lo + child - 1], ref items[lo + child]) < 0)
                    {
                        child++;
                    }
                    if (Compare(ref d, ref items[lo + child - 1]) >= 0)
                        break;
                    items[lo + i - 1] = items[lo + child - 1];
                    i = child;
                }
                items[lo + i - 1] = d;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private void InsertionSort(int lo, int hi)
            {
                for (int i = lo; i < hi; ++i)
                {
                    int j = i;
                    ref SortElement<T, TData> t = ref items[i + 1];
                    while (j >= lo && Compare(ref t, ref items[j]) < 0)
                    {
                        items[j + 1] = items[j];
                        --j;
                    }
                    items[j + 1] = t;
                }
            }
        }
    }
}
