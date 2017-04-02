using System.Collections.Generic;

namespace Inlinq.Impl
{
    public sealed class WhereEnumerable<T, TEnumerator, TPredicate> : Enumerable<T, WhereEnumerator<T, TEnumerator, TPredicate>>
        where TEnumerator : IEnumerator<T>
        where TPredicate : IFunctor<T, bool>
    {
        private IEnumerable<T, TEnumerator> source;
        private TPredicate predicate;

        public WhereEnumerable(IEnumerable<T, TEnumerator> source, TPredicate predicate)
        {
            this.source = source;
            this.predicate = predicate;
        }

        public override bool GetCount(out int count) => source.GetCount(out count);

        public override WhereEnumerator<T, TEnumerator, TPredicate> GetEnumerator()
            => new WhereEnumerator<T, TEnumerator, TPredicate>(source.GetEnumerator(), predicate);
    }
}
