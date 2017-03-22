using System.Collections.Generic;

namespace Inlinq.Impl
{
    public sealed class SelectEnumerable<TSource, TResult, TEnumerator, TSelector>
        : Enumerable<TResult, SelectEnumerator<TSource, TResult, TEnumerator, TSelector>>
        where TEnumerator : IEnumerator<TSource>
        where TSelector : IFunctor<TSource, TResult>
    {
        private IEnumerable<TSource, TEnumerator> source;
        private TSelector selector;

        public SelectEnumerable(IEnumerable<TSource, TEnumerator> source, TSelector selector)
        {
            this.source = source;
            this.selector = selector;
        }

        public override bool GetCount(out int count) => source.GetCount(out count);

        public override SelectEnumerator<TSource, TResult, TEnumerator, TSelector> GetEnumerator()
            => new SelectEnumerator<TSource, TResult, TEnumerator, TSelector>(source.GetEnumerator(), selector);
    }
}
