using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Inlinq.Sort
{
    internal struct SecondaryChainedSort<T, TKey, TComparer, TNextSort, TNextData>
        : ISecondarySort<T, SecondaryChainedKey<TKey, TNextData>>,
          ISortRebind<T, ISortChain<T, ISecondarySort<T>>>,
          ISortChain<T, ISecondarySort<T>>
        where TComparer : IComparer<TKey>
        where TNextSort : ISecondarySort<T, TNextData>
    {
        private Func<T, TKey> selector;
        private TComparer comparer;
        private TNextSort nextSort;
        
        public ISecondarySort<T> UnwrapChain => this;

        public SecondaryChainedSort(Func<T, TKey> selector, TComparer comparer, TNextSort nextSort)
        {
            this.selector = selector;
            this.comparer = comparer;
            this.nextSort = nextSort;
        }

        public void GetData(ref T element, out SecondaryChainedKey<TKey, TNextData> data)
        {
            data.key = default(TKey);
            data.keySet = false;
            data.next = default(TNextData);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Compare(ref T elementX, ref SecondaryChainedKey<TKey, TNextData> dataX, ref T elementY, ref SecondaryChainedKey<TKey, TNextData> dataY)
        {
            if (!dataX.keySet)
            {
                dataX.key = selector(elementX);
                dataX.keySet = true;
            }
            if (!dataY.keySet)
            {
                dataY.key = selector(elementY);
                dataY.keySet = true;
            }
            int c = comparer.Compare(dataX.key, dataY.key);
            return c != 0 ? c : nextSort.Compare(ref elementX, ref dataX.next, ref elementY, ref dataY.next);
        }

        public ISortChain<T, ISecondarySort<T>> Chain<TKey1, TComparer1>(Func<T, TKey1> selector, TComparer1 comparer)
            where TComparer1 : IComparer<TKey1>
            => nextSort.Chain(selector, comparer).UnwrapChain.InvertRebind(this);

        public TSort InvertRebind<TSort>(ISortRebind<T, TSort> outer)
            => outer.Rebind(this, default(SecondaryChainedKey<TKey, TNextData>));

        public ISortChain<T, ISecondarySort<T>> Rebind<TNextSort1, TNextData1>(TNextSort1 nextSort, TNextData1 nextData)
            where TNextSort1 : ISecondarySort<T, TNextData1>
            => new SecondaryChainedSort<T, TKey, TComparer, TNextSort1, TNextData1>(selector, comparer, nextSort);
    }
}
