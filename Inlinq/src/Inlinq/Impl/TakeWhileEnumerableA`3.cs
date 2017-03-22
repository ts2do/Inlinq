using System.Collections.Generic;

namespace Inlinq.Impl
{
    public sealed class TakeWhileEnumerableA<T, TEnumerator, TPredicate> : Enumerable<T, TakeWhileEnumeratorA<T, TEnumerator, TPredicate>>
        where TEnumerator : IEnumerator<T>
        where TPredicate : IFunctor<T, bool>
    {
        private IEnumerable<T, TEnumerator> source;
        private TPredicate predicate;

        public TakeWhileEnumerableA(IEnumerable<T, TEnumerator> source, TPredicate predicate)
        {
            this.source = source;
            this.predicate = predicate;
        }

        public override TakeWhileEnumeratorA<T, TEnumerator, TPredicate> GetEnumerator()
            => new TakeWhileEnumeratorA<T, TEnumerator, TPredicate>(source.GetEnumerator(), predicate);
    }
}
