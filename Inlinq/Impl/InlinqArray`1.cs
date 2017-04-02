using System;
using System.Collections;
using System.Collections.Generic;

namespace Inlinq.Impl
{
    public struct InlinqArray<T> : IEnumerable<T, ArrayEnumerator<T>>, IList<T>, IReadOnlyList<T>
    {
        private readonly T[] array;
        private int startIndex;
        private int length;

        public T this[int index]
            => index.LtUn(length) ? array[index + startIndex] : throw Error.ArgumentOutOfRange(nameof(index));

        public int Count => length;

        public InlinqArray(T[] array, int startIndex, int length)
        {
            this.array = array;
            this.startIndex = startIndex;
            this.length = length;
        }

        public InlinqArray(ArraySegment<T> arraySegment)
            : this(arraySegment.Array, arraySegment.Offset, arraySegment.Count)
        {
        }

        public bool Contains(T item) => IndexOf(item) >= 0;

        public void CopyTo(T[] array, int arrayIndex)
            => Array.Copy(this.array, startIndex, array, arrayIndex, length);

        public ArrayEnumerator<T> GetEnumerator()
            => new ArrayEnumerator<T>(array, startIndex, length);

        public int IndexOf(T item)
            => Array.IndexOf(array, item, startIndex, length);

        bool IEnumerable<T, ArrayEnumerator<T>>.GetCount(out int count)
        {
            count = length;
            return true;
        }

        T IList<T>.this[int index] { get => array[index]; set => throw Error.NotSupported(); }
        void IList<T>.Insert(int index, T item) => throw Error.NotSupported();
        void IList<T>.RemoveAt(int index) => throw Error.NotSupported();

        bool ICollection<T>.IsReadOnly => true;
        void ICollection<T>.Add(T item) => throw Error.NotSupported();
        void ICollection<T>.Clear() => throw Error.NotSupported();
        bool ICollection<T>.Remove(T item) => throw Error.NotSupported();

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
