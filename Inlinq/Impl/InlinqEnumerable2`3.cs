using System.Collections.Generic;

namespace Inlinq.Impl
{
    public sealed class InlinqEnumerable2<T, TEnumerator, TFunc> : Enumerable<T, TEnumerator>
        where TEnumerator : IEnumerator<T>
        where TFunc : IFunctor<TEnumerator>
    {
        private TFunc method;
        public InlinqEnumerable2(TFunc method) => this.method = method;
        public override TEnumerator GetEnumerator() => method.Invoke();
    }
}
