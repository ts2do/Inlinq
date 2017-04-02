namespace Inlinq
{
    public interface IFunctor<in T1, in T2, out TResult>
    {
        TResult Invoke(T1 arg1, T2 arg2);
    }
}
