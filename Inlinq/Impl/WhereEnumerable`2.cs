using System;
using System.Collections.Generic;

namespace Inlinq.Impl
{
    public sealed class WhereEnumerable<T, TEnumerator> : Enumerable<T, WhereEnumerator<T, TEnumerator>>
        where TEnumerator : IEnumerator<T>
    {
        private IEnumerable<T, TEnumerator> source;
        private Func<T, bool> predicate;

        public WhereEnumerable(IEnumerable<T, TEnumerator> source, Func<T, bool> predicate)
        {
            this.source = source;
            this.predicate = predicate;
        }

        public override bool GetCount(out int count) => source.GetCount(out count);

        public override WhereEnumerator<T, TEnumerator> GetEnumerator()
            => new WhereEnumerator<T, TEnumerator>(source.GetEnumerator(), predicate);
    }
}
