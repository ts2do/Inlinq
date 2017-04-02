using System.Collections.Generic;

namespace Inlinq.Impl
{
    public sealed class InlinqList1<T> : List<T, IEnumerator<T>>
    {
        private IList<T> source;
        public InlinqList1(IList<T> source) => this.source = source;
        public override T this[int index] => source[index];
        public override int Count => source.Count;
        public override bool Contains(T item) => source.Contains(item);
        public override void CopyTo(T[] array, int arrayIndex) => source.CopyTo(array, arrayIndex);
        public override IEnumerator<T> GetEnumerator() => source.GetEnumerator();
        public override int IndexOf(T item) => source.IndexOf(item);
    }
}
