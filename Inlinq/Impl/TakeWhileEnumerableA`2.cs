using System;
using System.Collections.Generic;

namespace Inlinq.Impl
{
    public sealed class TakeWhileEnumerableA<T, TEnumerator> : Enumerable<T, TakeWhileEnumeratorA<T, TEnumerator>>
        where TEnumerator : IEnumerator<T>
    {
        private IEnumerable<T, TEnumerator> source;
        private Func<T, bool> predicate;

        public TakeWhileEnumerableA(IEnumerable<T, TEnumerator> source, Func<T, bool> predicate)
        {
            this.source = source;
            this.predicate = predicate;
        }

        public override TakeWhileEnumeratorA<T, TEnumerator> GetEnumerator()
            => new TakeWhileEnumeratorA<T, TEnumerator>(source.GetEnumerator(), predicate);
    }
}
