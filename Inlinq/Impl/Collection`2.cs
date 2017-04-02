using System.Collections;
using System.Collections.Generic;

namespace Inlinq.Impl
{
    public abstract class Collection<T, TEnumerator> : IEnumerable<T, TEnumerator>, ICollection<T>, IReadOnlyCollection<T>
        where TEnumerator : IEnumerator<T>
    {
        public abstract int Count { get; }

        public abstract bool Contains(T item);

        public abstract void CopyTo(T[] array, int arrayIndex);

        bool IEnumerable<T, TEnumerator>.GetCount(out int count)
        {
            count = Count;
            return true;
        }

        public abstract TEnumerator GetEnumerator();

        bool ICollection<T>.IsReadOnly => true;
        void ICollection<T>.Add(T item) => throw Error.NotSupported();
        void ICollection<T>.Clear() => throw Error.NotSupported();
        bool ICollection<T>.Remove(T item) => throw Error.NotSupported();

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
