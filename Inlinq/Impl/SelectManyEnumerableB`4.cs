using System;
using System.Collections.Generic;

namespace Inlinq.Impl
{
    public sealed class SelectManyEnumerableB<TSource, TResult, TEnumerator1, TEnumerator2>
        : Enumerable<TResult, SelectManyEnumeratorB<TSource, TResult, TEnumerator1, TEnumerator2>>
        where TEnumerator1 : IEnumerator<TSource>
        where TEnumerator2 : IEnumerator<TResult>
    {
        private IEnumerable<TSource, TEnumerator1> source;
        private Func<TSource, int, IEnumerable<TResult, TEnumerator2>> selector;

        public SelectManyEnumerableB(IEnumerable<TSource, TEnumerator1> source, Func<TSource, int, IEnumerable<TResult, TEnumerator2>> selector)
        {
            this.source = source;
            this.selector = selector;
        }

        public override SelectManyEnumeratorB<TSource, TResult, TEnumerator1, TEnumerator2> GetEnumerator()
            => new SelectManyEnumeratorB<TSource, TResult, TEnumerator1, TEnumerator2>(source.GetEnumerator(), selector);
    }
}