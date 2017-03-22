namespace Inlinq
{
    public interface IFunctor<in T, out TResult>
    {
        TResult Invoke(T arg);
    }
}
