﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Inlinq.Sort
{
    internal struct PrimaryTerminalSort<T, TEnumerator, TKey, TComparer> : IPrimarySort<T, TEnumerator, TKey>
        where TEnumerator : IEnumerator<T>
        where TComparer : IComparer<TKey>
    {
        private Func<T, TKey> selector;
        private TComparer comparer;

        public PrimaryTerminalSort(Func<T, TKey> selector, TComparer comparer)
        {
            this.selector = selector;
            this.comparer = comparer;
        }

        public SortedArray<T> Sort(IEnumerable<T, TEnumerator> source)
            => SortUtil<T>.Sort(source, this, default(TKey));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetAux(ref T element, out TKey aux) => aux = selector(element);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Compare(SortElement<T, TKey> x, SortElement<T, TKey> y) => comparer.Compare(x.aux, y.aux);

        public IPrimarySort<T, TEnumerator> Chain<TKey1, TComparer1>(Func<T, TKey1> selector, TComparer1 comparer)
            where TComparer1 : IComparer<TKey1>
            => new PrimaryChainedSort<T, TEnumerator, TKey, TComparer, SecondaryTerminalSort<T, TKey1, TComparer1>, SecondaryKey<TKey1>>(this.selector, this.comparer, new SecondaryTerminalSort<T, TKey1, TComparer1>(selector, comparer));
    }
}