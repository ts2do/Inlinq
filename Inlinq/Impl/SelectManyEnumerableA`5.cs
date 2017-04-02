using System.Collections.Generic;

namespace Inlinq.Impl
{
    public sealed class SelectManyEnumerableA<TSource, TResult, TEnumerator1, TEnumerator2, TSelector>
        : Enumerable<TResult, SelectManyEnumeratorA<TSource, TResult, TEnumerator1, TEnumerator2, TSelector>>
        where TEnumerator1 : IEnumerator<TSource>
        where TEnumerator2 : IEnumerator<TResult>
        where TSelector : IFunctor<TSource, IEnumerable<TResult, TEnumerator2>>
    {
        private IEnumerable<TSource, TEnumerator1> source;
        private TSelector selector;
        
        public SelectManyEnumerableA(IEnumerable<TSource, TEnumerator1> source, TSelector selector)
        {
            this.source = source;
            this.selector = selector;
        }

        public override SelectManyEnumeratorA<TSource, TResult, TEnumerator1, TEnumerator2, TSelector> GetEnumerator()
            => new SelectManyEnumeratorA<TSource, TResult, TEnumerator1, TEnumerator2, TSelector>(source.GetEnumerator(), selector);
    }
}