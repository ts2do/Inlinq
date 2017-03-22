using System.Collections.Generic;

namespace Inlinq.Impl
{
    public sealed class InlinqQueue<T> : Collection<T, Queue<T>.Enumerator>
    {
        private Queue<T> source;
        public InlinqQueue(Queue<T> source) => this.source = source;
        public override int Count => source.Count;
        public override bool Contains(T item) => source.Contains(item);
        public override void CopyTo(T[] array, int arrayIndex) => source.CopyTo(array, arrayIndex);
        public override Queue<T>.Enumerator GetEnumerator() => source.GetEnumerator();
    }
}
