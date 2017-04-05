using System;
using System.Collections.Generic;

namespace Inlinq.Impl
{
    public sealed class SkipWhileEnumerableA<T, TEnumerator> : Enumerable<T, SkipWhileEnumeratorA<T, TEnumerator>>
        where TEnumerator : IEnumerator<T>
    {
        private IEnumerable<T, TEnumerator> source;
        private Func<T, bool> predicate;

        public SkipWhileEnumerableA(IEnumerable<T, TEnumerator> source, Func<T, bool> predicate)
        {
            this.source = source;
            this.predicate = predicate;
        }

        public override SkipWhileEnumeratorA<T, TEnumerator> GetEnumerator()
            => new SkipWhileEnumeratorA<T, TEnumerator>(source.GetEnumerator(), predicate);
    }
}
