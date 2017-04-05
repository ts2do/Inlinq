using System;
using System.Collections.Generic;

namespace Inlinq.Impl
{
    public sealed class SelectManyEnumerableD<TSource, TResult, TEnumerator>
        : Enumerable<TResult, SelectManyEnumeratorD<TSource, TResult, TEnumerator>>
        where TEnumerator : IEnumerator<TSource>
    {
        private IEnumerable<TSource, TEnumerator> source;
        private Func<TSource, int, IEnumerable<TResult>> selector;

        public SelectManyEnumerableD(IEnumerable<TSource, TEnumerator> source, Func<TSource, int, IEnumerable<TResult>> selector)
        {
            this.source = source;
            this.selector = selector;
        }

        public override SelectManyEnumeratorD<TSource, TResult, TEnumerator> GetEnumerator()
            => new SelectManyEnumeratorD<TSource, TResult, TEnumerator>(source.GetEnumerator(), selector);
    }
}