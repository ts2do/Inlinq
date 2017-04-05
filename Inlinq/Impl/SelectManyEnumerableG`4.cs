using System;
using System.Collections.Generic;

namespace Inlinq.Impl
{
    public sealed class SelectManyEnumerableG<TSource, TCollection, TResult, TEnumerator>
        : Enumerable<TResult, SelectManyEnumeratorG<TSource, TCollection, TResult, TEnumerator>>
        where TEnumerator : IEnumerator<TSource>
    {
        private IEnumerable<TSource, TEnumerator> source;
        private Func<TSource, IEnumerable<TCollection>> collectionSelector;
        private Func<TSource, TCollection, TResult> resultSelector;

        public SelectManyEnumerableG(IEnumerable<TSource, TEnumerator> source, Func<TSource, IEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
        {
            this.source = source;
            this.collectionSelector = collectionSelector;
            this.resultSelector = resultSelector;
        }

        public override SelectManyEnumeratorG<TSource, TCollection, TResult, TEnumerator> GetEnumerator()
            => new SelectManyEnumeratorG<TSource, TCollection, TResult, TEnumerator>(source.GetEnumerator(), collectionSelector, resultSelector);
    }
}