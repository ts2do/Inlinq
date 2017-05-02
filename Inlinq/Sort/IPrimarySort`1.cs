using System;
using System.Collections.Generic;

namespace Inlinq.Sort
{
    internal interface IPrimarySort<T>
    {
        SortedArray<T> Sort<TEnumerator>(IEnumerable<T, TEnumerator> source)
            where TEnumerator : IEnumerator<T>;
        IPrimarySort<T> Chain<TKey, TComparer>(Func<T, TKey> selector, TComparer comparer)
            where TComparer : IComparer<TKey>;
    }
}
