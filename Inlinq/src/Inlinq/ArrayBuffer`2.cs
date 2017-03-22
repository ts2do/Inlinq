using System;
using System.Collections.Generic;

namespace Inlinq
{
    internal struct ArrayBuffer<T, TEnumerator>
        where TEnumerator : IEnumerator<T>
    {
        internal int count;
        private int capacity;
        internal T[] items;

        private const int DefaultInitCapacity = 4;

        public ArrayBuffer(IEnumerable<T, TEnumerator> source)
            : this(source, DefaultInitCapacity)
        { }

        public ArrayBuffer(IEnumerable<T, TEnumerator> source, int initCapacity)
            => Fill(source, initCapacity, 0, 0, null, out count, out capacity, out items);

        public void Fill(IEnumerable<T, TEnumerator> source)
            => Fill(source, DefaultInitCapacity);

        public void Fill(IEnumerable<T, TEnumerator> source, int initCapacity)
            => Fill(source, initCapacity, count, capacity, items, out count, out capacity, out items);

        private static void Fill(IEnumerable<T, TEnumerator> source, int initCapacity, int oldCount, int oldCapacity, T[] oldItems, out int countOut, out int capacityOut, out T[] itemsOut)
        {
            int count, capacity;
            T[] items;
            if (!(source is ICollection<T> collection))
            {
                if (!source.GetCount(out int actualCount))
                    actualCount = -1;
                if (actualCount != 0)
                {
                    using (var e = source.GetEnumerator())
                    {
                        if (e.MoveNext())
                        {
                            if (oldItems == null)
                            {
                                capacity = actualCount < 0 ? Math.Max(initCapacity, 1) : actualCount;
                                items = new T[capacity];
                            }
                            else if (actualCount > oldCapacity)
                            {
                                capacity = actualCount;
                                items = new T[actualCount];
                            }
                            else
                            {
                                capacity = oldCapacity;
                                items = oldItems;
                            }
                            count = 0;
                            do
                            {
                                if (count == capacity)
                                    Array.Resize(ref items, checked(capacity *= 2));
                                items[count++] = e.Current;
                            } while (e.MoveNext());
                        }
                        else
                        {
                            count = capacity = 0;
                            items = oldItems ?? new T[0];
                        }
                    }
                }
                else
                {
                    count = capacity = 0;
                    items = oldItems ?? new T[0];
                }
            }
            else
            {
                count = capacity = collection.Count;
                if (count > 0)
                {
                    items = count > oldCapacity ? new T[count] : oldItems;
                    collection.CopyTo(items, 0);
                }
                else
                {
                    items = oldItems ?? new T[0];
                }
            }

            if (items == oldItems && count < oldCount)
                Array.Clear(items, count, oldCount - count);

            countOut = count;
            capacityOut = capacity;
            itemsOut = items;
        }

        public void Clear()
        {
            if (count > 0)
                Array.Clear(items, 0, count);
        }

        public T[] ToArray()
            => count < capacity ? Util.ResizeArray(items, count, count) : items;

        public ArraySegment<T> ToArraySegment()
            => count < capacity ? new ArraySegment<T>(items, 0, count) : new ArraySegment<T>(items);
    }
}
