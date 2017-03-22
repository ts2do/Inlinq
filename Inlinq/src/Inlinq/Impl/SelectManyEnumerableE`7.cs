using System.Collections.Generic;

namespace Inlinq.Impl
{
    public sealed class SelectManyEnumerableE<TSource, TCollection, TResult, TEnumerator1, TEnumerator2, TCollectionSelector, TResultSelector>
        : Enumerable<TResult, SelectManyEnumeratorE<TSource, TCollection, TResult, TEnumerator1, TEnumerator2, TCollectionSelector, TResultSelector>>
        where TEnumerator1 : IEnumerator<TSource>
        where TEnumerator2 : IEnumerator<TCollection>
        where TCollectionSelector : IFunctor<TSource, IEnumerable<TCollection, TEnumerator2>>
        where TResultSelector : IFunctor<TSource, TCollection, TResult>
    {
        private IEnumerable<TSource, TEnumerator1> source;
        private TCollectionSelector collectionSelector;
        private TResultSelector resultSelector;

        public SelectManyEnumerableE(IEnumerable<TSource, TEnumerator1> source, TCollectionSelector collectionSelector, TResultSelector resultSelector)
        {
            this.source = source;
            this.collectionSelector = collectionSelector;
            this.resultSelector = resultSelector;
        }

        public override SelectManyEnumeratorE<TSource, TCollection, TResult, TEnumerator1, TEnumerator2, TCollectionSelector, TResultSelector> GetEnumerator()
            => new SelectManyEnumeratorE<TSource, TCollection, TResult, TEnumerator1, TEnumerator2, TCollectionSelector, TResultSelector>(source.GetEnumerator(), collectionSelector, resultSelector);
    }
}