using System.Collections.Generic;

namespace Inlinq.Impl
{
    public sealed class SelectManyEnumerableC<TSource, TResult, TEnumerator, TSelector>
        : Enumerable<TResult, SelectManyEnumeratorC<TSource, TResult, TEnumerator, TSelector>>
        where TEnumerator : IEnumerator<TSource>
        where TSelector : IFunctor<TSource, IEnumerable<TResult>>
    {
        private IEnumerable<TSource, TEnumerator> source;
        private TSelector selector;
        
        public SelectManyEnumerableC(IEnumerable<TSource, TEnumerator> source, TSelector selector)
        {
            this.source = source;
            this.selector = selector;
        }

        public override SelectManyEnumeratorC<TSource, TResult, TEnumerator, TSelector> GetEnumerator()
            => new SelectManyEnumeratorC<TSource, TResult, TEnumerator, TSelector>(source.GetEnumerator(), selector);
    }
}