using System;
using System.Collections.Generic;

namespace Inlinq.Impl
{
    public sealed class InlinqEnumerable2<T, TEnumerator> : Enumerable<T, TEnumerator>
        where TEnumerator : IEnumerator<T>
    {
        private Func<TEnumerator> method;
        public InlinqEnumerable2(Func<TEnumerator> method) => this.method = method;
        public override TEnumerator GetEnumerator() => method();
    }
}
