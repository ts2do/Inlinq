using System.Collections.Generic;

namespace Inlinq.Impl
{
    public sealed class InlinqCollection3<T, TEnumerator, TCollection, TFunc> : Collection<T, TEnumerator>
        where TEnumerator : IEnumerator<T>
        where TCollection : ICollection<T>
        where TFunc : IFunctor<TCollection, TEnumerator>
    {
        private TCollection source;
        private TFunc method;
        public InlinqCollection3(TCollection source, TFunc method)
        {
            this.source = source;
            this.method = method;
        }
        public override int Count => source.Count;
        public override bool Contains(T item) => source.Contains(item);
        public override void CopyTo(T[] array, int arrayIndex) => source.CopyTo(array, arrayIndex);
        public override TEnumerator GetEnumerator() => method.Invoke(source);
    }
}
