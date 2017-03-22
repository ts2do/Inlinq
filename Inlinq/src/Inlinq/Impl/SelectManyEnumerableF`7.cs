using System.Collections.Generic;

namespace Inlinq.Impl
{
    public sealed class SelectManyEnumerableF<TSource, TCollection, TResult, TEnumerator1, TEnumerator2, TCollectionSelector, TResultSelector>
        : Enumerable<TResult, SelectManyEnumeratorF<TSource, TCollection, TResult, TEnumerator1, TEnumerator2, TCollectionSelector, TResultSelector>>
        where TEnumerator1 : IEnumerator<TSource>
        where TEnumerator2 : IEnumerator<TCollection>
        where TCollectionSelector : IFunctor<TSource, int, IEnumerable<TCollection, TEnumerator2>>
        where TResultSelector : IFunctor<TSource, TCollection, TResult>
    {
        private IEnumerable<TSource, TEnumerator1> source;
        private TCollectionSelector collectionSelector;
        private TResultSelector resultSelector;

        public SelectManyEnumerableF(IEnumerable<TSource, TEnumerator1> source, TCollectionSelector collectionSelector, TResultSelector resultSelector)
        {
            this.source = source;
            this.collectionSelector = collectionSelector;
            this.resultSelector = resultSelector;
        }

        public override SelectManyEnumeratorF<TSource, TCollection, TResult, TEnumerator1, TEnumerator2, TCollectionSelector, TResultSelector> GetEnumerator()
            => new SelectManyEnumeratorF<TSource, TCollection, TResult, TEnumerator1, TEnumerator2, TCollectionSelector, TResultSelector>(source.GetEnumerator(), collectionSelector, resultSelector);
    }
}