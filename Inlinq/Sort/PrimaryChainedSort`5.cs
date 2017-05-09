using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Inlinq.Sort
{
    internal struct PrimaryChainedSort<T, TKey, TComparer, TNextSort, TNextAux>
        : ISort<T, PrimaryChainedKey<TKey, TNextAux>>,
          IPrimarySort<T>,
          ISortRebind<T, ISortChain<T, IPrimarySort<T>>>,
          ISortChain<T, IPrimarySort<T>>
        where TComparer : IComparer<TKey>
        where TNextSort : ISecondarySort<T, TNextAux>
    {
        private Func<T, TKey> selector;
        private TComparer comparer;
        private TNextSort nextSort;

        public IPrimarySort<T> UnwrapChain => this;
        public ISortChain<T, IPrimarySort<T>> UnwrapRebind => this;

        public PrimaryChainedSort(Func<T, TKey> selector, TComparer comparer, TNextSort nextSort)
        {
            this.selector = selector;
            this.comparer = comparer;
            this.nextSort = nextSort;
        }

        public Func<int, T> Sort<TEnumerator>(IEnumerable<T, TEnumerator> source, out int startIndex, out int endIndex)
            where TEnumerator : IEnumerator<T>
            => SortUtil<T>.Sort(source, this, default(PrimaryChainedKey<TKey, TNextAux>), out startIndex, out endIndex);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetAux(ref T element, out PrimaryChainedKey<TKey, TNextAux> aux)
        {
            aux.key = selector(element);
            aux.next = default(TNextAux);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Compare(ref T elementX, ref PrimaryChainedKey<TKey, TNextAux> auxX, ref T elementY, ref PrimaryChainedKey<TKey, TNextAux> auxY)
        {
            int c = comparer.Compare(auxX.key, auxY.key);
            return c != 0 ? c : nextSort.Compare(ref elementX, ref auxX.next, ref elementY, ref auxX.next);
        }

        public ISortChain<T, IPrimarySort<T>> Chain<TKey1, TComparer1>(Func<T, TKey1> selector, TComparer1 comparer)
            where TComparer1 : IComparer<TKey1>
            => nextSort.Chain(selector, comparer).UnwrapChain.InvertRebind(this).UnwrapRebind;

        public ISortRebind<T, ISortChain<T, IPrimarySort<T>>> Rebind<TNextSort1, TNextAux1>(TNextSort1 nextSort, TNextAux1 nextAux)
            where TNextSort1 : ISecondarySort<T, TNextAux1>
            => new PrimaryChainedSort<T, TKey, TComparer, TNextSort1, TNextAux1>(selector, comparer, nextSort);
    }
}
