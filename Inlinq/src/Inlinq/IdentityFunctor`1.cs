namespace Inlinq
{
    public struct IdentityFunctor<T> : ITFunctor<IdentityFunctor<T>, T, T>
    {
        public T Invoke(T arg) => arg;
        public IdentityFunctor<T> Unwrap() => this;
    }
}
