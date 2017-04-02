using System.Collections.Generic;

namespace Inlinq.Impl
{
    public sealed class ReverseEnumerable<T, TEnumerator> : Enumerable<T, ReverseEnumerator<T, TEnumerator>>
        where TEnumerator : IEnumerator<T>
    {
        private IEnumerable<T, TEnumerator> source;
        public ReverseEnumerable(IEnumerable<T, TEnumerator> source) => this.source = source;
        public override bool GetCount(out int count) => source.GetCount(out count);
        public override ReverseEnumerator<T, TEnumerator> GetEnumerator()
            => new ReverseEnumerator<T, TEnumerator>(source);
    }
}
