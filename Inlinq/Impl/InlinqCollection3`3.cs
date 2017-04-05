using System;
using System.Collections.Generic;

namespace Inlinq.Impl
{
    public sealed class InlinqCollection3<T, TEnumerator, TCollection> : Collection<T, TEnumerator>
        where TEnumerator : IEnumerator<T>
        where TCollection : ICollection<T>
    {
        private TCollection source;
        private Func<TCollection, TEnumerator> method;
        public InlinqCollection3(TCollection source, Func<TCollection, TEnumerator> method)
        {
            this.source = source;
            this.method = method;
        }
        public override int Count => source.Count;
        public override bool Contains(T item) => source.Contains(item);
        public override void CopyTo(T[] array, int arrayIndex) => source.CopyTo(array, arrayIndex);
        public override TEnumerator GetEnumerator() => method(source);
    }
}
