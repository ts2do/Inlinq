using System.Collections.Generic;

namespace Inlinq.Sort
{
    internal interface IPrimaryChainedSort<T, TEnumerator> : IPrimarySort<T, TEnumerator>
        where TEnumerator : IEnumerator<T>
    {
        IPrimaryChainedSort<T, TEnumerator> Rebind<TNextSort, TNextAux>(TNextSort nextSort, TNextAux nextAux)
            where TNextSort : ISecondarySort<T, TNextAux>;
    }
}
