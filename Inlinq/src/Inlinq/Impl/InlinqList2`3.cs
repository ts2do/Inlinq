using System.Collections.Generic;

namespace Inlinq.Impl
{
    public sealed class InlinqList2<T, TEnumerator, TFunc> : List<T, TEnumerator>
        where TEnumerator : IEnumerator<T>
        where TFunc : IFunctor<TEnumerator>
    {
        private IList<T> source;
        private TFunc method;
        public InlinqList2(IList<T> source, TFunc method)
        {
            this.source = source;
            this.method = method;
        }
        public override T this[int index] => source[index];
        public override int Count => source.Count;
        public override bool Contains(T item) => source.Contains(item);
        public override void CopyTo(T[] array, int arrayIndex) => source.CopyTo(array, arrayIndex);
        public override TEnumerator GetEnumerator() => method.Invoke();
        public override int IndexOf(T item) => source.IndexOf(item);
    }
}
