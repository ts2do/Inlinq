using System.Collections;
using System.Collections.Generic;

namespace Inlinq.Impl
{
    public struct SelectEnumerator<TSource, TResult, TEnumerator, TSelector> : IEnumerator<TResult>
        where TEnumerator : IEnumerator<TSource>
        where TSelector : IFunctor<TSource, TResult>
    {
        private TEnumerator source;
        private TSelector selector;

        public TResult Current
            => selector.Invoke(source.Current);
        
        public SelectEnumerator(TEnumerator source, TSelector selector)
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
