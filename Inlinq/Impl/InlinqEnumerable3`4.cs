using System.Collections.Generic;

namespace Inlinq.Impl
{
    public sealed class InlinqEnumerable3<T, TEnumerator, TEnumerable, TFunc> : Enumerable<T, TEnumerator>
        where TEnumerator : IEnumerator<T>
        where TFunc : IFunctor<TEnumerable, TEnumerator>
    {
        private TEnumerable source;
        private TFunc func;
        public InlinqEnumerable3(TEnumerable source, TFunc func)
        {
            this.source = source;
            this.func = func;
        }
        public override TEnumerator GetEnumerator() => func.Invoke(source);
    }
}
