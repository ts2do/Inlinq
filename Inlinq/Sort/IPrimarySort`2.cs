using System;
using System.Collections.Generic;

namespace Inlinq.Sort
{
    internal interface IPrimarySort<T, TEnumerator>
        where TEnumerator : IEnumerator<T>
    {
        SortedArray<T> Sort(IEnumerable<T, TEnumerator> source);
        IPrimarySort<T, TEnumerator> Chain<TKey, TComparer>(Func<T, TKey> selector, TComparer comparer)
            where TComparer : IComparer<TKey>;
    }
}
