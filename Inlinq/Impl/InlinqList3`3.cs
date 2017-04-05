using System;
using System.Collections.Generic;

namespace Inlinq.Impl
{
    public sealed class InlinqList3<T, TEnumerator, TList> : List<T, TEnumerator>
        where TEnumerator : IEnumerator<T>
        where TList : IList<T>
    {
        private TList source;
        private Func<TList, TEnumerator> method;
        public InlinqList3(TList source, Func<TList, TEnumerator> method)
        {
            this.source = source;
            this.method = method;
        }
        public override T this[int index] => source[index];
        public override int Count => source.Count;
        public override bool Contains(T item) => source.Contains(item);
        public override void CopyTo(T[] array, int arrayIndex) => source.CopyTo(array, arrayIndex);
        public override TEnumerator GetEnumerator() => method(source);
        public override int IndexOf(T item) => source.IndexOf(item);
    }
}
