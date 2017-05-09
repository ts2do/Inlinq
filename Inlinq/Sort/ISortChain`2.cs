using System;
using System.Collections.Generic;

namespace Inlinq.Sort
{
    internal interface ISortChain<T, TSort>
    {
        TSort UnwrapChain { get; }
        ISortChain<T, TSort> Chain<TKey, TComparer>(Func<T, TKey> selector, TComparer comparer)
            where TComparer : IComparer<TKey>;
    }
}
