using System;
using System.Collections.Generic;

namespace Inlinq.Impl
{
    public sealed class SelectManyEnumerableC<TSource, TResult, TEnumerator>
        : Enumerable<TResult, SelectManyEnumeratorC<TSource, TResult, TEnumerator>>
        where TEnumerator : IEnumerator<TSource>
    {
        private IEnumerable<TSource, TEnumerator> source;
        private Func<TSource, IEnumerable<TResult>> selector;
        
        public SelectManyEnumerableC(IEnumerable<TSource, TEnumerator> source, Func<TSource, IEnumerable<TResult>> selector)
        {
            this.source = source;
            this.selector = selector;
        }

        public override SelectManyEnumeratorC<TSource, TResult, TEnumerator> GetEnumerator()
            => new SelectManyEnumeratorC<TSource, TResult, TEnumerator>(source.GetEnumerator(), selector);
    }
}