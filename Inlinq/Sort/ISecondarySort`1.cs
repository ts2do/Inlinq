namespace Inlinq.Sort
{
    internal interface ISecondarySort<T> : ISortChain<T, ISecondarySort<T>>
    {
        TSort InvertRebind<TSort>(ISortRebind<T, TSort> outer);
    }
}
