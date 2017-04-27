﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Inlinq.Sort
{
    internal struct SecondaryChainedSort<T, TKey, TComparer, TNextSort, TNextAux> : ISecondaryChainedSort<T, SecondaryChainedKey<TKey, TNextAux>>
        where TComparer : IComparer<TKey>
        where TNextSort : ISecondarySort<T, TNextAux>
    {
        private Func<T, TKey> selector;
        private TComparer comparer;
        private TNextSort nextSort;

        public SecondaryChainedSort(Func<T, TKey> selector, TComparer comparer, TNextSort nextSort)
        {
            this.selector = selector;
            this.comparer = comparer;
            this.nextSort = nextSort;
        }

        public void GetAux(ref T element, out SecondaryChainedKey<TKey, TNextAux> aux)
        {
            aux.key = default(TKey);
            aux.keySet = false;
            aux.next = default(TNextAux);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Compare(ref T elementX, ref SecondaryChainedKey<TKey, TNextAux> auxX, ref T elementY, ref SecondaryChainedKey<TKey, TNextAux> auxY)
        {
            if (!auxX.keySet)
            {
                auxX.key = selector(elementX);
                auxX.keySet = true;
            }
            if (!auxY.keySet)
            {
                auxY.key = selector(elementY);
                auxY.keySet = true;
            }
            int c = comparer.Compare(auxX.key, auxY.key);
            return c != 0 ? c : nextSort.Compare(ref elementX, ref auxX.next, ref elementY, ref auxY.next);
        }

        public ISecondaryChainedSort<T> Chain<TKey1, TComparer1>(Func<T, TKey1> selector, TComparer1 comparer)
            where TComparer1 : IComparer<TKey1>
            => nextSort.Chain(selector, comparer).InvertRebind(this);

        public ISecondaryChainedSort<T> InvertRebind(ISecondaryChainedSort<T> outer)
            => outer.Rebind(this, default(SecondaryChainedKey<TKey, TNextAux>));

        public IPrimaryChainedSort<T, TEnumerator> InvertRebind<TEnumerator>(IPrimaryChainedSort<T, TEnumerator> outer)
            where TEnumerator : IEnumerator<T>
            => outer.Rebind(this, default(SecondaryChainedKey<TKey, TNextAux>));

        public ISecondaryChainedSort<T> Rebind<TNextSort1, TNextAux1>(TNextSort1 nextSort, TNextAux1 nextAux)
            where TNextSort1 : ISecondarySort<T, TNextAux1>
            => new SecondaryChainedSort<T, TKey, TComparer, TNextSort1, TNextAux1>(selector, comparer, nextSort);
    }
}
