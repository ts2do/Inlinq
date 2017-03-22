namespace Inlinq.Impl
{
    public struct NotPredicate<T, TPredicate> : IPredicate<T>
        where TPredicate : IPredicate<T>
    {
        private TPredicate predicate;
        public NotPredicate(TPredicate predicate) => this.predicate = predicate;
        public bool Invoke(T arg)
            => !predicate.Invoke(arg);
    }
}
