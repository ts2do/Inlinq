﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Inlinq.Sort
{
    internal struct SecondaryTerminalSort<T, TKey, TComparer> : ISecondarySort<T, SecondaryKey<TKey>>
        where TComparer : IComparer<TKey>
    {
        private Func<T, TKey> selector;
        private TComparer comparer;

        public SecondaryTerminalSort(Func<T, TKey> selector, TComparer comparer)
        {
            this.selector = selector;
            this.comparer = comparer;
        }

        public void GetAux(ref T element, out SecondaryKey<TKey> aux)
        {
            aux.key = default(TKey);
            aux.keySet = false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Compare(ref T elementX, ref SecondaryKey<TKey> auxX, ref T elementY, ref SecondaryKey<TKey> auxY)
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
            return comparer.Compare(auxX.key, auxY.key);
        }
        
        public ISecondaryChainedSort<T> Chain<TKey1, TComparer1>(Func<T, TKey1> selector, TComparer1 comparer)
            where TComparer1 : IComparer<TKey1>
            => new SecondaryChainedSort<T, TKey, TComparer, SecondaryTerminalSort<T, TKey1, TComparer1>, SecondaryKey<TKey1>>(this.selector, this.comparer, new SecondaryTerminalSort<T, TKey1, TComparer1>(selector, comparer));

        public ISecondaryChainedSort<T> InvertRebind(ISecondaryChainedSort<T> outer)
            => outer.Rebind(this, default(SecondaryKey<TKey>));

        public IPrimaryChainedSort<T, TEnumerator> InvertRebind<TEnumerator>(IPrimaryChainedSort<T, TEnumerator> outer)
            where TEnumerator : IEnumerator<T>
            => outer.Rebind(this, default(SecondaryKey<TKey>));
    }
}
