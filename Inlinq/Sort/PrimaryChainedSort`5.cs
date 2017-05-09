using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Inlinq.Sort
{
    internal struct PrimaryChainedSort<T, TKey, TComparer, TNextSort, TNextData>
        : ISort<T, PrimaryChainedKey<TKey, TNextData>>,
          IPrimarySort<T>,
          ISortRebind<T, ISortChain<T, IPrimarySort<T>>>,
          ISortChain<T, IPrimarySort<T>>
        where TComparer : IComparer<TKey>
        where TNextSort : ISecondarySort<T, TNextData>
    {
        private Func<T, TKey> selector;
        private TComparer comparer;
        private TNextSort nextSort;

        public IPrimarySort<T> UnwrapChain => this;

        public PrimaryChainedSort(Func<T, TKey> selector, TComparer comparer, TNextSort nextSort)
        {
            this.selector = selector;
            this.comparer = comparer;
            this.nextSort = nextSort;
        }

        public Func<int, T> Sort<TEnumerator>(IEnumerable<T, TEnumerator> source, out int startIndex, out int endIndex)
            where TEnumerator : IEnumerator<T>
            => SortUtil<T>.Sort(source, this, default(PrimaryChainedKey<TKey, TNextData>), out startIndex, out endIndex);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetData(ref T element, out PrimaryChainedKey<TKey, TNextData> data)
        {
            data.key = selector(element);
            data.next = default(TNextData);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Compare(ref T elementX, ref PrimaryChainedKey<TKey, TNextData> dataX, ref T elementY, ref PrimaryChainedKey<TKey, TNextData> dataY)
        {
            int c = comparer.Compare(dataX.key, dataY.key);
            return c != 0 ? c : nextSort.Compare(ref elementX, ref dataX.next, ref elementY, ref dataX.next);
        }

        public ISortChain<T, IPrimarySort<T>> Chain<TKey1, TComparer1>(Func<T, TKey1> selector, TComparer1 comparer)
            where TComparer1 : IComparer<TKey1>
            => nextSort.Chain(selector, comparer).UnwrapChain.InvertRebind(this);

        public ISortChain<T, IPrimarySort<T>> Rebind<TNextSort1, TNextData1>(TNextSort1 nextSort, TNextData1 nextData)
            where TNextSort1 : ISecondarySort<T, TNextData1>
            => new PrimaryChainedSort<T, TKey, TComparer, TNextSort1, TNextData1>(selector, comparer, nextSort);
    }
}
