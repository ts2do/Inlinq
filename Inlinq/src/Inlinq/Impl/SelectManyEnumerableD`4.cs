using System.Collections.Generic;

namespace Inlinq.Impl
{
    public sealed class SelectManyEnumerableD<TSource, TResult, TEnumerator, TSelector>
        : Enumerable<TResult, SelectManyEnumeratorD<TSource, TResult, TEnumerator, TSelector>>
        where TEnumerator : IEnumerator<TSource>
        where TSelector : IFunctor<TSource, int, IEnumerable<TResult>>
    {
        private IEnumerable<TSource, TEnumerator> source;
        private TSelector selector;

        public SelectManyEnumerableD(IEnumerable<TSource, TEnumerator> source, TSelector selector)
        {
            this.source = source;
            this.selector = selector;
        }

        public override SelectManyEnumeratorD<TSource, TResult, TEnumerator, TSelector> GetEnumerator()
            => new SelectManyEnumeratorD<TSource, TResult, TEnumerator, TSelector>(source.GetEnumerator(), selector);
    }
}