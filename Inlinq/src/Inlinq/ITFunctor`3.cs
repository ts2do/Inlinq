namespace Inlinq
{
    public interface ITFunctor<TFunctor, in T, out TResult> : IFunctor<T, TResult>
        where TFunctor : IFunctor<T, TResult>
    {
        TFunctor Unwrap();
    }
}
