using System.Collections.Generic;

namespace Inlinq.Impl
{
    public sealed class TakeEnumerable<T, TEnumerator> : Enumerable<T, TakeEnumerator<T, TEnumerator>>
        where TEnumerator : IEnumerator<T>
    {
        private IEnumerable<T, TEnumerator> source;
        private int count;

        public TakeEnumerable(IEnumerable<T, TEnumerator> source, int count)
        {
            this.source = source;
            this.count = count;
        }

        public override bool GetCount(out int count)
        {
            if (!source.GetCount(out count))
                return false;

            if (count > this.count)
                count = this.count;
            return true;
        }

        public override TakeEnumerator<T, TEnumerator> GetEnumerator()
            => new TakeEnumerator<T, TEnumerator>(source.GetEnumerator(), count);
    }
}
