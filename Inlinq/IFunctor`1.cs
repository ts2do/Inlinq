namespace Inlinq
{
    public interface IFunctor<out TResult>
    {
        TResult Invoke();
    }
}
