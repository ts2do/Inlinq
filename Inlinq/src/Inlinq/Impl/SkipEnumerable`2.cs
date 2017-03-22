using System.Collections.Generic;

namespace Inlinq.Impl
{
    public sealed class SkipEnumerable<T, TEnumerator> : Enumerable<T, SkipEnumerator<T, TEnumerator>>
        where TEnumerator : IEnumerator<T>
    {
        private IEnumerable<T, TEnumerator> source;
        private int count;

        public SkipEnumerable(IEnumerable<T, TEnumerator> source, int count)
        {
            this.source = source;
            this.count = count;
        }

        public override bool GetCount(out int count)
        {
            if (!source.GetCount(out count))
                return false;

            count = System.Math.Max(count - this.count, 0);
            return true;
        }

        public override SkipEnumerator<T, TEnumerator> GetEnumerator()
            => new SkipEnumerator<T, TEnumerator>(source.GetEnumerator(), count);
    }
}
