﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Inlinq.Sort
{
    internal struct PrimaryTerminalSort<T, TKey, TComparer>
        : ISort<T, TKey>,
          IPrimarySort<T>,
          ISortChain<T, IPrimarySort<T>>
        where TComparer : IComparer<TKey>
    {
        private Func<T, TKey> selector;
        private TComparer comparer;

        public IPrimarySort<T> UnwrapChain => this;

        public PrimaryTerminalSort(Func<T, TKey> selector, TComparer comparer)
        {
            this.selector = selector;
            this.comparer = comparer;
        }

        public Func<int, T> Sort<TEnumerator>(IEnumerable<T, TEnumerator> source, out int startIndex, out int endIndex)
            where TEnumerator : IEnumerator<T>
            => SortUtil<T>.Sort(source, this, default(TKey), out startIndex, out endIndex);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetData(ref T element, out TKey data) => data = selector(element);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Compare(ref T elementX, ref TKey dataX, ref T elementY, ref TKey dataY) => comparer.Compare(dataX, dataY);

        public ISortChain<T, IPrimarySort<T>> Chain<TKey1, TComparer1>(Func<T, TKey1> selector, TComparer1 comparer)
            where TComparer1 : IComparer<TKey1>
            => new PrimaryChainedSort<T, TKey, TComparer, SecondaryTerminalSort<T, TKey1, TComparer1>, SecondaryKey<TKey1>>(this.selector, this.comparer, new SecondaryTerminalSort<T, TKey1, TComparer1>(selector, comparer));
    }
}
