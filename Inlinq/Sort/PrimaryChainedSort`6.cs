using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Inlinq.Sort
{
    internal struct PrimaryChainedSort<T, TEnumerator, TKey, TComparer, TNextSort, TNextAux> : IPrimaryChainedSort<T, TEnumerator, PrimaryChainedKey<TKey, TNextAux>>
        where TEnumerator : IEnumerator<T>
        where TComparer : IComparer<TKey>
        where TNextSort : ISecondarySort<T, TNextAux>
    {
        private Func<T, TKey> selector;
        private TComparer comparer;
        private TNextSort nextSort;

        public PrimaryChainedSort(Func<T, TKey> selector, TComparer comparer, TNextSort nextSort)
        {
            this.selector = selector;
            this.comparer = comparer;
            this.nextSort = nextSort;
        }

        public SortedArray<T> Sort(IEnumerable<T, TEnumerator> source)
            => SortUtil<T>.Sort(source, this, default(PrimaryChainedKey<TKey, TNextAux>));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetAux(ref T element, out PrimaryChainedKey<TKey, TNextAux> aux)
        {
            aux.key = selector(element);
            aux.next = default(TNextAux);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Compare(SortElement<T, PrimaryChainedKey<TKey, TNextAux>> x, SortElement<T, PrimaryChainedKey<TKey, TNextAux>> y)
        {
            ref var auxX = ref x.aux;
            ref var auxY = ref y.aux;
            int c = comparer.Compare(auxX.key, auxY.key);
            return c != 0 ? c : nextSort.Compare(ref x.element, ref auxX.next, ref x.element, ref auxX.next);
        }

        public IPrimarySort<T, TEnumerator> Chain<TKey1, TComparer1>(Func<T, TKey1> selector, TComparer1 comparer)
            where TComparer1 : IComparer<TKey1>
            => nextSort.Chain(selector, comparer).InvertRebind(this);

        public IPrimaryChainedSort<T, TEnumerator> Rebind<TNextSort1, TNextAux1>(TNextSort1 nextSort, TNextAux1 nextAux)
            where TNextSort1 : ISecondarySort<T, TNextAux1>
            => new PrimaryChainedSort<T, TEnumerator, TKey, TComparer, TNextSort1, TNextAux1>(selector, comparer, nextSort);
    }
}
