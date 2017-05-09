using System;
using System.Collections.Generic;

namespace Inlinq.Sort
{
    internal interface IPrimarySort<T> : ISortChain<T, IPrimarySort<T>>
    {
        Func<int, T> Sort<TEnumerator>(IEnumerable<T, TEnumerator> source, out int startIndex, out int endIndex)
            where TEnumerator : IEnumerator<T>;
    }
}
