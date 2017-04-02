using System.Collections.Generic;

namespace Inlinq.Impl
{
    public sealed class InlinqCollection2<T, TEnumerator, TFunc> : Collection<T, TEnumerator>
        where TEnumerator : IEnumerator<T>
        where TFunc : IFunctor<TEnumerator>
    {
        private ICollection<T> source;
        private TFunc method;
        public InlinqCollection2(ICollection<T> source, TFunc method)
        {
            this.source = source;
            this.method = method;
        }
        public override int Count => source.Count;
        public override bool Contains(T item) => source.Contains(item);
        public override void CopyTo(T[] array, int arrayIndex) => source.CopyTo(array, arrayIndex);
        public override TEnumerator GetEnumerator() => method.Invoke();
    }
}
