using System;
using System.Collections.Generic;

namespace Inlinq.Impl
{
    public sealed class SelectManyEnumerableH<TSource, TCollection, TResult, TEnumerator>
        : Enumerable<TResult, SelectManyEnumeratorH<TSource, TCollection, TResult, TEnumerator>>
        where TEnumerator : IEnumerator<TSource>
    {
        private IEnumerable<TSource, TEnumerator> source;
        private Func<TSource, int, IEnumerable<TCollection>> collectionSelector;
        private Func<TSource, TCollection, TResult> resultSelector;

        public SelectManyEnumerableH(IEnumerable<TSource, TEnumerator> source, Func<TSource, int, IEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
        {
            this.source = source;
            this.collectionSelector = collectionSelector;
            this.resultSelector = resultSelector;
        }

        public override SelectManyEnumeratorH<TSource, TCollection, TResult, TEnumerator> GetEnumerator()
            => new SelectManyEnumeratorH<TSource, TCollection, TResult, TEnumerator>(source.GetEnumerator(), collectionSelector, resultSelector);
    }
}