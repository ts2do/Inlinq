using System.Collections.Generic;

namespace Inlinq.Sort
{
    internal interface IPrimarySort<T, TAux> : IPrimarySort<T>, ISort<T, TAux>, IComparer<SortElement<T, TAux>>
    {
    }
}
