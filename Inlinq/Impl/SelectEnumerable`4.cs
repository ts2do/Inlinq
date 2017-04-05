using System;
using System.Collections.Generic;

namespace Inlinq.Impl
{
    public sealed class SelectEnumerable<TSource, TResult, TEnumerator>
        : Enumerable<TResult, SelectEnumerator<TSource, TResult, TEnumerator>>
        where TEnumerator : IEnumerator<TSource>
    {
        private IEnumerable<TSource, TEnumerator> source;
        private Func<TSource, TResult> selector;

        public SelectEnumerable(IEnumerable<TSource, TEnumerator> source, Func<TSource, TResult> selector)
        {
            this.source = source;
            this.selector = selector;
        }

        public override bool GetCount(out int count) => source.GetCount(out count);

        public override SelectEnumerator<TSource, TResult, TEnumerator> GetEnumerator()
            => new SelectEnumerator<TSource, TResult, TEnumerator>(source.GetEnumerator(), selector);
    }
}
