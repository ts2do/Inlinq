namespace Inlinq.Sort
{
    internal interface ISecondaryChainedSort<T> : ISecondarySort<T>
    {
        ISecondaryChainedSort<T> Rebind<TNextSort, TNextAux>(TNextSort nextSort, TNextAux nextAux)
            where TNextSort : ISecondarySort<T, TNextAux>;
    }
}
