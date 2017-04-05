using System;
using System.Collections.Generic;

namespace Inlinq.Impl
{
    public sealed class SelectManyEnumerableF<TSource, TCollection, TResult, TEnumerator1, TEnumerator2>
        : Enumerable<TResult, SelectManyEnumeratorF<TSource, TCollection, TResult, TEnumerator1, TEnumerator2>>
        where TEnumerator1 : IEnumerator<TSource>
        where TEnumerator2 : IEnumerator<TCollection>
    {
        private IEnumerable<TSource, TEnumerator1> source;
        private Func<TSource, int, IEnumerable<TCollection, TEnumerator2>> collectionSelector;
        private Func<TSource, TCollection, TResult> resultSelector;

        public SelectManyEnumerableF(IEnumerable<TSource, TEnumerator1> source, Func<TSource, int, IEnumerable<TCollection, TEnumerator2>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
        {
            this.source = source;
            this.collectionSelector = collectionSelector;
            this.resultSelector = resultSelector;
        }

        public override SelectManyEnumeratorF<TSource, TCollection, TResult, TEnumerator1, TEnumerator2> GetEnumerator()
            => new SelectManyEnumeratorF<TSource, TCollection, TResult, TEnumerator1, TEnumerator2>(source.GetEnumerator(), collectionSelector, resultSelector);
    }
}