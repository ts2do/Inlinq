using System.Collections.Generic;

namespace Inlinq.Impl
{
    public sealed class InlinqEnumerable1<T, TEnumerable> : Enumerable<T, IEnumerator<T>>
        where TEnumerable : IEnumerable<T>
    {
        private TEnumerable source;
        public InlinqEnumerable1(TEnumerable source) => this.source = source;
        public override IEnumerator<T> GetEnumerator() => source.GetEnumerator();
    }
}
