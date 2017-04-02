using System.Collections.Generic;

namespace Inlinq.Impl
{
    public sealed class InlinqList<T> : List<T, List<T>.Enumerator>
    {
        private List<T> source;
        public InlinqList(List<T> source) => this.source = source;
        public override T this[int index] => source[index];
        public override int Count => source.Count;
        public override bool Contains(T item) => source.Contains(item);
        public override void CopyTo(T[] array, int arrayIndex) => source.CopyTo(array, arrayIndex);
        public override List<T>.Enumerator GetEnumerator() => source.GetEnumerator();
        public override int IndexOf(T item) => source.IndexOf(item);
    }
}
