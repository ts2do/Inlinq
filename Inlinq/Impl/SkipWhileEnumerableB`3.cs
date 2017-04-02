using System.Collections.Generic;

namespace Inlinq.Impl
{
    public sealed class SkipWhileEnumerableB<T, TEnumerator, TPredicate> : Enumerable<T, SkipWhileEnumeratorB<T, TEnumerator, TPredicate>>
        where TEnumerator : IEnumerator<T>
        where TPredicate : IFunctor<T, int, bool>
    {
        private IEnumerable<T, TEnumerator> source;
        private TPredicate predicate;

        public SkipWhileEnumerableB(IEnumerable<T, TEnumerator> source, TPredicate predicate)
        {
            this.source = source;
            this.predicate = predicate;
        }

        public override SkipWhileEnumeratorB<T, TEnumerator, TPredicate> GetEnumerator()
            => new SkipWhileEnumeratorB<T, TEnumerator, TPredicate>(source.GetEnumerator(), predicate);
    }
}
