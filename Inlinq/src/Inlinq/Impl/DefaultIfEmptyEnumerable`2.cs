using System.Collections.Generic;

namespace Inlinq.Impl
{
    public sealed class DefaultIfEmptyEnumerable<T, TEnumerator>
        : Enumerable<T, DefaultIfEmptyEnumerator<T, TEnumerator>>
        where TEnumerator : IEnumerator<T>
    {
        private IEnumerable<T, TEnumerator> source;
        private T defaultValue;

        public DefaultIfEmptyEnumerable(IEnumerable<T, TEnumerator> source, T defaultValue)
        {
            this.source = source;
            this.defaultValue = defaultValue;
        }

        public override bool GetCount(out int count)
        {
            if (!source.GetCount(out count)) return false;
            if (count < 1) count = 1;
            return true;
        }

        public override DefaultIfEmptyEnumerator<T, TEnumerator> GetEnumerator()
            => new DefaultIfEmptyEnumerator<T, TEnumerator>(source.GetEnumerator(), defaultValue);
    }
}
