namespace Inlinq.Sort
{
    interface ISortRebind<T, TSort>
    {
        TSort Rebind<TNextSort, TNextData>(TNextSort nextSort, TNextData nextData)
            where TNextSort : ISecondarySort<T, TNextData>;
    }
}
