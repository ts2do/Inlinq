using System.Collections.Generic;

namespace Inlinq.Impl
{
    public sealed class InlinqSortedSet<T> : Collection<T, SortedSet<T>.Enumerator>
    {
        private SortedSet<T> source;
        public InlinqSortedSet(SortedSet<T> source) => this.source = source;
        public override int Count => source.Count;
        public override bool Contains(T item) => source.Contains(item);
        public override void CopyTo(T[] array, int arrayIndex) => source.CopyTo(array, arrayIndex);
        public override SortedSet<T>.Enumerator GetEnumerator() => source.GetEnumerator();
    }
}
