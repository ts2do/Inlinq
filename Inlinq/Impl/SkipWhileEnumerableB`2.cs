using System;
using System.Collections.Generic;

namespace Inlinq.Impl
{
    public sealed class SkipWhileEnumerableB<T, TEnumerator> : Enumerable<T, SkipWhileEnumeratorB<T, TEnumerator>>
        where TEnumerator : IEnumerator<T>
    {
        private IEnumerable<T, TEnumerator> source;
        private Func<T, int, bool> predicate;

        public SkipWhileEnumerableB(IEnumerable<T, TEnumerator> source, Func<T, int, bool> predicate)
        {
            this.source = source;
            this.predicate = predicate;
        }

        public override SkipWhileEnumeratorB<T, TEnumerator> GetEnumerator()
            => new SkipWhileEnumeratorB<T, TEnumerator>(source.GetEnumerator(), predicate);
    }
}
