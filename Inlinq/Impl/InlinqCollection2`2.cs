using System;
using System.Collections.Generic;

namespace Inlinq.Impl
{
    public sealed class InlinqCollection2<T, TEnumerator> : Collection<T, TEnumerator>
        where TEnumerator : IEnumerator<T>
    {
        private ICollection<T> source;
        private Func<TEnumerator> method;
        public InlinqCollection2(ICollection<T> source, Func<TEnumerator> method)
        {
            this.source = source;
            this.method = method;
        }
        public override int Count => source.Count;
        public override bool Contains(T item) => source.Contains(item);
        public override void CopyTo(T[] array, int arrayIndex) => source.CopyTo(array, arrayIndex);
        public override TEnumerator GetEnumerator() => method();
    }
}
