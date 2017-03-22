using System.Collections;

namespace Inlinq.Impl
{
    public sealed class OfTypeEnumerable<T> : Enumerable<T, OfTypeEnumerator<T>>
    {
        private IEnumerable source;
        public OfTypeEnumerable(IEnumerable source) => this.source = source;
        public override OfTypeEnumerator<T> GetEnumerator()
            => new OfTypeEnumerator<T>(source.GetEnumerator());
    }
}
