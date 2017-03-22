namespace Inlinq
{
    public struct Identity<T> : ITFunctor<Identity<T>, T, T>
    {
        public T Invoke(T arg) => arg;
        public Identity<T> Unwrap() => this;
    }
}
