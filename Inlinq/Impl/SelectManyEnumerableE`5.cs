using System;
using System.Collections.Generic;

namespace Inlinq.Impl
{
    public sealed class SelectManyEnumerableE<TSource, TCollection, TResult, TEnumerator1, TEnumerator2>
        : Enumerable<TResult, SelectManyEnumeratorE<TSource, TCollection, TResult, TEnumerator1, TEnumerator2>>
        where TEnumerator1 : IEnumerator<TSource>
        where TEnumerator2 : IEnumerator<TCollection>
    {
        private IEnumerable<TSource, TEnumerator1> source;
        private Func<TSource, IEnumerable<TCollection, TEnumerator2>> collectionSelector;
        private Func<TSource, TCollection, TResult> resultSelector;

        public SelectManyEnumerableE(IEnumerable<TSource, TEnumerator1> source, Func<TSource, IEnumerable<TCollection, TEnumerator2>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
        {
            this.source = source;
            this.collectionSelector = collectionSelector;
            this.resultSelector = resultSelector;
        }

        public override SelectManyEnumeratorE<TSource, TCollection, TResult, TEnumerator1, TEnumerator2> GetEnumerator()
            => new SelectManyEnumeratorE<TSource, TCollection, TResult, TEnumerator1, TEnumerator2>(source.GetEnumerator(), collectionSelector, resultSelector);
    }
}