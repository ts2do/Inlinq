using System;
using System.Collections.Generic;

namespace Inlinq.Sort
{
    internal interface ISecondarySort<T>
    {
        ISecondaryChainedSort<T> Chain<TKey, TComparer>(Func<T, TKey> selector, TComparer comparer)
            where TComparer : IComparer<TKey>;
        ISecondaryChainedSort<T> InvertRebind(ISecondaryChainedSort<T> outer);
        IPrimaryChainedSort<T, TEnumerator> InvertRebind<TEnumerator>(IPrimaryChainedSort<T, TEnumerator> outer)
            where TEnumerator : IEnumerator<T>;
    }
}
