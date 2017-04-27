using System.Collections.Generic;

namespace Inlinq.Sort
{
    internal interface IPrimarySort<T, TEnumerator, TAux> : IPrimarySort<T, TEnumerator>, ISort<T, TAux>, IComparer<SortElement<T, TAux>>
        where TEnumerator : IEnumerator<T>
    {
    }
}
