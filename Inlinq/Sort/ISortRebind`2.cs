namespace Inlinq.Sort
{
    interface ISortRebind<T, TSort>
    {
        TSort UnwrapRebind { get; }
        ISortRebind<T, TSort> Rebind<TNextSort, TNextAux>(TNextSort nextSort, TNextAux nextAux)
            where TNextSort : ISecondarySort<T, TNextAux>;
    }
}
