using System;
using System.Collections.Generic;

namespace Inlinq
{
    internal struct Set<T, TEqualityComparer>
        where TEqualityComparer : IEqualityComparer<T>
    {
        private int size;
        private Slot[] slots;
        private int count;
        private int freeList;
        private TEqualityComparer comparer;

        public Set(TEqualityComparer comparer)
        {
            this.comparer = comparer;
            size = 7;
            slots = new Slot[size];
            count = 0;
            freeList = -1;
        }

        public void Clear()
        {
            Array.Clear(slots, 0, size);
            count = 0;
            freeList = -1;
        }

        // If value is not in set, add it and return true; otherwise return false
        public bool Add(T value)
        {
            int hashCode = GetHashCode(value);
            if (!Find(ref value, hashCode))
            {
                ref Slot slotRef = ref GetFreeSlot(out int index);
                ref int bucketRef = ref slots[hashCode % size].bucket;
                slotRef.hashCode = hashCode;
                slotRef.value = value;
                slotRef.next = bucketRef - 1;
                bucketRef = index + 1;
                return true;
            }
            return false;
        }

        // Check whether value is in set
        public bool Contains(T value)
            => Find(ref value, GetHashCode(value));

        // If value is in set, remove it and return true; otherwise return false
        public bool Remove(T value)
        {
            int hashCode = GetHashCode(value);
            int last = -1;
            ref int bucketRef = ref slots[hashCode % size].bucket;
            if (bucketRef > 0)
            {
                int i = bucketRef - 1;
                do
                {
                    ref Slot slotRef = ref slots[i];
                    if (slotRef.hashCode == hashCode && comparer.Equals(slotRef.value, value))
                    {
                        if (last < 0)
                            bucketRef = slotRef.next + 1;
                        else
                            slots[last].next = slotRef.next;
                        slotRef.hashCode = -1;
                        slotRef.value = default(T);
                        slotRef.next = freeList;
                        freeList = i;
                        return true;
                    }
                    last = i;
                    i = slotRef.next;
                } while (i >= 0);
            }
            return false;
        }

        private ref Slot GetFreeSlot(out int index)
        {
            if (freeList < 0)
            {
                if (count == size) Resize();
                return ref slots[index = count++];
            }

            index = freeList;
            ref Slot slotRef = ref slots[index];
            freeList = slotRef.next;
            return ref slotRef;
        }

        private bool Find(ref T value, int hashCode)
        {
            int bucketRef = slots[hashCode % size].bucket;
            if (bucketRef > 0)
            {
                int i = bucketRef - 1;
                do
                {
                    ref Slot slotRef = ref slots[i];
                    if (slotRef.hashCode == hashCode && comparer.Equals(slotRef.value, value))
                        return true;

                    i = slotRef.next;
                } while (i >= 0);
            }
            return false;
        }

        private void Resize()
        {
            int newSize = checked(count * 2 + 1);
            Slot[] newSlots = Util.ResizeArray(slots, count, newSize);
            for (int i = 0; i < count; i++)
            {
                ref Slot slot = ref slots[i];
                ref int bucketRef = ref newSlots[slot.hashCode % newSize].bucket;
                slot.next = bucketRef - 1;
                bucketRef = i + 1;
            }
            slots = newSlots;
            size = newSize;
        }

        private int GetHashCode(T value)
            => value != null ? comparer.GetHashCode(value) & 0x7FFFFFFF : 0;

        public struct Slot
        {
            public int bucket;
            public int hashCode;
            public T value;
            public int next;
        }
    }
}
