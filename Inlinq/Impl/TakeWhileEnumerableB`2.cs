using System;
using System.Collections.Generic;

namespace Inlinq.Impl
{
    public sealed class TakeWhileEnumerableB<T, TEnumerator> : Enumerable<T, TakeWhileEnumeratorB<T, TEnumerator>>
        where TEnumerator : IEnumerator<T>
    {
        private IEnumerable<T, TEnumerator> source;
        private Func<T, int, bool> predicate;

        public TakeWhileEnumerableB(IEnumerable<T, TEnumerator> source, Func<T, int, bool> predicate)
        {
            this.source = source;
            this.predicate = predicate;
        }

        public override TakeWhileEnumeratorB<T, TEnumerator> GetEnumerator()
            => new TakeWhileEnumeratorB<T, TEnumerator>(source.GetEnumerator(), predicate);
    }
}
