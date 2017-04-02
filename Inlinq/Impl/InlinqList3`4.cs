using System.Collections.Generic;

namespace Inlinq.Impl
{
    public sealed class InlinqList3<T, TEnumerator, TList, TFunc> : List<T, TEnumerator>
        where TEnumerator : IEnumerator<T>
        where TList : IList<T>
        where TFunc : IFunctor<TList, TEnumerator>
    {
        private TList source;
        private TFunc method;
        public InlinqList3(TList source, TFunc method)
        {
            this.source = source;
            this.method = method;
        }
        public override T this[int index] => source[index];
        public override int Count => source.Count;
        public override bool Contains(T item) => source.Contains(item);
        public override void CopyTo(T[] array, int arrayIndex) => source.CopyTo(array, arrayIndex);
        public override TEnumerator GetEnumerator() => method.Invoke(source);
        public override int IndexOf(T item) => source.IndexOf(item);
    }
}
