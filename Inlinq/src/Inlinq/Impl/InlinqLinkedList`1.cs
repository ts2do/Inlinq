using System.Collections.Generic;

namespace Inlinq.Impl
{
    public sealed class InlinqLinkedList<T> : Collection<T, LinkedList<T>.Enumerator>
    {
        private LinkedList<T> source;
        public InlinqLinkedList(LinkedList<T> source) => this.source = source;
        public override int Count => source.Count;
        public override bool Contains(T item) => source.Contains(item);
        public override void CopyTo(T[] array, int arrayIndex) => source.CopyTo(array, arrayIndex);
        public override LinkedList<T>.Enumerator GetEnumerator() => source.GetEnumerator();
    }
}
