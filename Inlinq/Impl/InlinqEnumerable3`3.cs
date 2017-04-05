using System;
using System.Collections.Generic;

namespace Inlinq.Impl
{
    public sealed class InlinqEnumerable3<T, TEnumerator, TEnumerable> : Enumerable<T, TEnumerator>
        where TEnumerator : IEnumerator<T>
    {
        private TEnumerable source;
        private Func<TEnumerable, TEnumerator> func;
        public InlinqEnumerable3(TEnumerable source, Func<TEnumerable, TEnumerator> func)
        {
            this.source = source;
            this.func = func;
        }
        public override TEnumerator GetEnumerator() => func(source);
    }
}
