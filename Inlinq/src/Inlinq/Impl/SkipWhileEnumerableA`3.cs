using System.Collections.Generic;

namespace Inlinq.Impl
{
    public sealed class SkipWhileEnumerableA<T, TEnumerator, TPredicate> : Enumerable<T, SkipWhileEnumeratorA<T, TEnumerator, TPredicate>>
        where TEnumerator : IEnumerator<T>
        where TPredicate : IFunctor<T, bool>
    {
        private IEnumerable<T, TEnumerator> source;
        private TPredicate predicate;

        public SkipWhileEnumerableA(IEnumerable<T, TEnumerator> source, TPredicate predicate)
        {
            this.source = source;
            this.predicate = predicate;
        }

        public override SkipWhileEnumeratorA<T, TEnumerator, TPredicate> GetEnumerator()
            => new SkipWhileEnumeratorA<T, TEnumerator, TPredicate>(source.GetEnumerator(), predicate);
    }
}
