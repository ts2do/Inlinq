using System;
using System.Collections.Generic;

namespace Inlinq.Impl
{
    public sealed class SelectManyEnumerableA<TSource, TResult, TEnumerator1, TEnumerator2>
        : Enumerable<TResult, SelectManyEnumeratorA<TSource, TResult, TEnumerator1, TEnumerator2>>
        where TEnumerator1 : IEnumerator<TSource>
        where TEnumerator2 : IEnumerator<TResult>
    {
        private IEnumerable<TSource, TEnumerator1> source;
        private Func<TSource, IEnumerable<TResult, TEnumerator2>> selector;
        
        public SelectManyEnumerableA(IEnumerable<TSource, TEnumerator1> source, Func<TSource, IEnumerable<TResult, TEnumerator2>> selector)
        {
            this.source = source;
            this.selector = selector;
        }

        public override SelectManyEnumeratorA<TSource, TResult, TEnumerator1, TEnumerator2> GetEnumerator()
            => new SelectManyEnumeratorA<TSource, TResult, TEnumerator1, TEnumerator2>(source.GetEnumerator(), selector);
    }
}