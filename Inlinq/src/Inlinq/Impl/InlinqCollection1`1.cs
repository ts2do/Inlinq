using System.Collections.Generic;

namespace Inlinq.Impl
{
    public sealed class InlinqCollection1<T> : Collection<T, IEnumerator<T>>
    {
        private ICollection<T> source;
        public InlinqCollection1(ICollection<T> source) => this.source = source;
        public override int Count => source.Count;
        public override bool Contains(T item) => source.Contains(item);
        public override void CopyTo(T[] array, int arrayIndex) => source.CopyTo(array, arrayIndex);
        public override IEnumerator<T> GetEnumerator() => source.GetEnumerator();
    }
}
