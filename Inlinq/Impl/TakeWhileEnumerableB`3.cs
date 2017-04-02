using System.Collections.Generic;

namespace Inlinq.Impl
{
    public sealed class TakeWhileEnumerableB<T, TEnumerator, TPredicate> : Enumerable<T, TakeWhileEnumeratorB<T, TEnumerator, TPredicate>>
        where TEnumerator : IEnumerator<T>
        where TPredicate : IFunctor<T, int, bool>
    {
        private IEnumerable<T, TEnumerator> source;
        private TPredicate predicate;

        public TakeWhileEnumerableB(IEnumerable<T, TEnumerator> source, TPredicate predicate)
        {
            this.source = source;
            this.predicate = predicate;
        }

        public override TakeWhileEnumeratorB<T, TEnumerator, TPredicate> GetEnumerator()
            => new TakeWhileEnumeratorB<T, TEnumerator, TPredicate>(source.GetEnumerator(), predicate);
    }
}
