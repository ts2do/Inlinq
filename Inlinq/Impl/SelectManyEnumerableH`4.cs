using System.Collections.Generic;

namespace Inlinq.Impl
{
    public sealed class SelectManyEnumerableH<TSource, TCollection, TResult, TEnumerator, TCollectionSelector, TResultSelector>
        : Enumerable<TResult, SelectManyEnumeratorH<TSource, TCollection, TResult, TEnumerator, TCollectionSelector, TResultSelector>>
        where TEnumerator : IEnumerator<TSource>
        where TCollectionSelector : IFunctor<TSource, int, IEnumerable<TCollection>>
        where TResultSelector : IFunctor<TSource, TCollection, TResult>
    {
        private IEnumerable<TSource, TEnumerator> source;
        private TCollectionSelector collectionSelector;
        private TResultSelector resultSelector;

        public SelectManyEnumerableH(IEnumerable<TSource, TEnumerator> source, TCollectionSelector collectionSelector, TResultSelector resultSelector)
        {
            this.source = source;
            this.collectionSelector = collectionSelector;
            this.resultSelector = resultSelector;
        }

        public override SelectManyEnumeratorH<TSource, TCollection, TResult, TEnumerator, TCollectionSelector, TResultSelector> GetEnumerator()
            => new SelectManyEnumeratorH<TSource, TCollection, TResult, TEnumerator, TCollectionSelector, TResultSelector>(source.GetEnumerator(), collectionSelector, resultSelector);
    }
}