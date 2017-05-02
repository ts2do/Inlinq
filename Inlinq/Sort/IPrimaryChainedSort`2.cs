namespace Inlinq.Sort
{
    internal interface IPrimaryChainedSort<T> : IPrimarySort<T>
    {
        IPrimaryChainedSort<T> Rebind<TNextSort, TNextAux>(TNextSort nextSort, TNextAux nextAux)
            where TNextSort : ISecondarySort<T, TNextAux>;
    }
}
