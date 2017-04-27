using System.Collections.Generic;

namespace Inlinq.Sort
{
    internal interface IPrimaryChainedSort<T, TEnumerator, TAux> : IPrimaryChainedSort<T, TEnumerator>, IPrimarySort<T, TEnumerator, TAux>
        where TEnumerator : IEnumerator<T>
    {
    }
}
