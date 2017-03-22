using System.Collections.Generic;

namespace Inlinq.Impl
{
    public abstract class List<T, TEnumerator> : Collection<T, TEnumerator>, IList<T>, IReadOnlyList<T>
        where TEnumerator : IEnumerator<T>
    {
        public abstract T this[int index] { get; }

        public abstract int IndexOf(T item);

        T IList<T>.this[int index] { get => this[index]; set => throw Error.NotSupported(); }
        void IList<T>.Insert(int index, T item) => throw Error.NotSupported();
        void IList<T>.RemoveAt(int index) => throw Error.NotSupported();
    }
}
