using Inlinq.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Inlinq
{
    public sealed class Grouping<TKey, TElement> : Enumerable<TElement, ArrayEnumerator<TElement>>, IList<TElement>, IGrouping<TKey, TElement>
    {
        internal TKey key;
        internal int hashCode;
        internal TElement[] elements;
        private int count;
        private int capacity;
        internal Grouping<TKey, TElement> hashNext;
        internal Grouping<TKey, TElement> next;

        public int Count => count;
        public TKey Key => key;
        public TElement this[int index]
        {
            get
            {
                if (index.GeUn(count)) ThrowIndexOutOfRange();
                return elements[index];
            }
        }

        internal void Add(TElement element)
        {
            if (capacity == count) Array.Resize(ref elements, capacity = checked(count * 2));
            elements[count] = element;
            ++count;
        }

        public override bool GetCount(out int count)
        {
            count = this.count;
            return true;
        }

        public override ArrayEnumerator<TElement> GetEnumerator()
            => new ArrayEnumerator<TElement>(elements, 0, count);

        public bool Contains(TElement item)
            => count > 0 && Array.IndexOf(elements, item, 0, count) >= 0;

        public void CopyTo(TElement[] array, int arrayIndex)
        {
            if (count > 0)
                Array.Copy(elements, 0, array, arrayIndex, count);
        }

        public int IndexOf(TElement item)
            => count > 0 ? Array.IndexOf(elements, item, 0, count) : -1;

        TElement IList<TElement>.this[int index]
        {
            get => this[index];
            set => throw Error.NotSupported();
        }
        void IList<TElement>.Insert(int index, TElement item) => throw Error.NotSupported();
        void IList<TElement>.RemoveAt(int index) => throw Error.NotSupported();

        bool ICollection<TElement>.IsReadOnly => true;
        void ICollection<TElement>.Add(TElement item) => throw Error.NotSupported();
        void ICollection<TElement>.Clear() => throw Error.NotSupported();
        bool ICollection<TElement>.Remove(TElement item) => throw Error.NotSupported();

        internal InlinqArray<TElement> AsInlinqArray() => new InlinqArray<TElement>(elements, 0, count);

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void ThrowIndexOutOfRange()
        {
            throw Error.ArgumentOutOfRange("index");
        }
    }
}
