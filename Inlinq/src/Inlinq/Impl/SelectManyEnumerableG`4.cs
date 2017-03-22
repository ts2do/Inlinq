using System.Collections.Generic;

namespace Inlinq.Impl
{
    public sealed class SelectManyEnumerableG<TSource, TCollection, TResult, TEnumerator, TCollectionSelector, TResultSelector>
        : Enumerable<TResult, SelectManyEnumeratorG<TSource, TCollection, TResult, TEnumerator, TCollectionSelector, TResultSelector>>
        where TEnumerator : IEnumerator<TSource>
        where TCollectionSelector : IFunctor<TSource, IEnumerable<TCollection>>
        where TResultSelector : IFunctor<TSource, TCollection, TResult>
    {
        private IEnumerable<TSource, TEnumerator> source;
        private TCollectionSelector collectionSelector;
        private TResultSelector resultSelector;

        public SelectManyEnumerableG(IEnumerable<TSource, TEnumerator> source, TCollectionSelector collectionSelector, TResultSelector resultSelector)
        {
            this.source = source;
            this.collectionSelector = collectionSelector;
            this.resultSelector = resultSelector;
        }

        public override SelectManyEnumeratorG<TSource, TCollection, TResult, TEnumerator, TCollectionSelector, TResultSelector> GetEnumerator()
            => new SelectManyEnumeratorG<TSource, TCollection, TResult, TEnumerator, TCollectionSelector, TResultSelector>(source.GetEnumerator(), collectionSelector, resultSelector);
    }
}