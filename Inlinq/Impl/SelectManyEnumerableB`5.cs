using System.Collections.Generic;

namespace Inlinq.Impl
{
    public sealed class SelectManyEnumerableB<TSource, TResult, TEnumerator1, TEnumerator2, TSelector>
        : Enumerable<TResult, SelectManyEnumeratorB<TSource, TResult, TEnumerator1, TEnumerator2, TSelector>>
        where TEnumerator1 : IEnumerator<TSource>
        where TEnumerator2 : IEnumerator<TResult>
        where TSelector : IFunctor<TSource, int, IEnumerable<TResult, TEnumerator2>>
    {
        private IEnumerable<TSource, TEnumerator1> source;
        private TSelector selector;

        public SelectManyEnumerableB(IEnumerable<TSource, TEnumerator1> source, TSelector selector)
        {
            this.source = source;
            this.selector = selector;
        }

        public override SelectManyEnumeratorB<TSource, TResult, TEnumerator1, TEnumerator2, TSelector> GetEnumerator()
            => new SelectManyEnumeratorB<TSource, TResult, TEnumerator1, TEnumerator2, TSelector>(source.GetEnumerator(), selector);
    }
}