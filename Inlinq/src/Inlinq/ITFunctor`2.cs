namespace Inlinq
{
    public interface ITFunctor<TFunctor, out TResult> : IFunctor<TResult>
        where TFunctor : IFunctor<TResult>
    {
        TFunctor Unwrap();
    }
}
