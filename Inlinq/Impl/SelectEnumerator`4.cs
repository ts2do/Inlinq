using System;
using System.Collections;
using System.Collections.Generic;

namespace Inlinq.Impl
{
    public struct SelectEnumerator<TSource, TResult, TEnumerator> : IEnumerator<TResult>
        where TEnumerator : IEnumerator<TSource>
    {
        private TEnumerator source;
        private Func<TSource, TResult> selector;

        public TResult Current
            => selector(source.Current);
        
        public SelectEnumerator(TEnumerator source, Func<TSource, TResult> selector)
        {
            this.source = source;
            this.selector = selector;
        }

        public void Dispose() => source.Dispose();
        public bool MoveNext() => source.MoveNext();
        public void Reset() => source.Reset();

        object IEnumerator.Current => Current;
    }
}
