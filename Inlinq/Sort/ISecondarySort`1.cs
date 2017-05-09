namespace Inlinq.Sort
{
    internal interface ISecondarySort<T> : ISortChain<T, ISecondarySort<T>>
    {
        ISortRebind<T, TSort> InvertRebind<TSort>(ISortRebind<T, TSort> outer);
    }
}
