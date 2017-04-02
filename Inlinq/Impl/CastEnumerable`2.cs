using System.Collections;

namespace Inlinq.Impl
{
    public sealed class CastEnumerable<T> : Enumerable<T, CastEnumerator<T>>
    {
        private IEnumerable source;
        public CastEnumerable(IEnumerable source) => this.source = source;
        public override CastEnumerator<T> GetEnumerator()
            => new CastEnumerator<T>(source.GetEnumerator());
    }
}
