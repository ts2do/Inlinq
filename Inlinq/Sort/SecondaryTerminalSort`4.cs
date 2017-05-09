using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Inlinq.Sort
{
    internal struct SecondaryTerminalSort<T, TKey, TComparer>
        : ISecondarySort<T, SecondaryKey<TKey>>,
          ISortChain<T, ISecondarySort<T>>
        where TComparer : IComparer<TKey>
    {
        private Func<T, TKey> selector;
        private TComparer comparer;

        public ISecondarySort<T> UnwrapChain => this;

        public SecondaryTerminalSort(Func<T, TKey> selector, TComparer comparer)
        {
            this.selector = selector;
            this.comparer = comparer;
        }

        public void GetData(ref T element, out SecondaryKey<TKey> data)
        {
            data.key = default(TKey);
            data.keySet = false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Compare(ref T elementX, ref SecondaryKey<TKey> dataX, ref T elementY, ref SecondaryKey<TKey> dataY)
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
            return comparer.Compare(dataX.key, dataY.key);
        }
        
        public ISortChain<T, ISecondarySort<T>> Chain<TKey1, TComparer1>(Func<T, TKey1> selector, TComparer1 comparer)
            where TComparer1 : IComparer<TKey1>
            => new SecondaryChainedSort<T, TKey, TComparer, SecondaryTerminalSort<T, TKey1, TComparer1>, SecondaryKey<TKey1>>(this.selector, this.comparer, new SecondaryTerminalSort<T, TKey1, TComparer1>(selector, comparer));

        public TSort InvertRebind<TSort>(ISortRebind<T, TSort> outer)
            => outer.Rebind(this, default(SecondaryKey<TKey>));
    }
}
