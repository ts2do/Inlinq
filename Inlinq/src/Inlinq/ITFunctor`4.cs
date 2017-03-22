namespace Inlinq
{
    public interface ITFunctor<TFunctor, in T1, in T2, out TResult> : IFunctor<T1, T2, TResult>
        where TFunctor : IFunctor<T1, T2, TResult>
    {
        TFunctor Unwrap();
    }
}
