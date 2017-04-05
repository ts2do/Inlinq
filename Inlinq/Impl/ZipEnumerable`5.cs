using System;
using System.Collections.Generic;

namespace Inlinq.Impl
{
    public sealed class ZipEnumerable<TFirst, TSecond, TResult, TEnumerator1, TEnumerator2> : Enumerable<TResult, ZipEnumerator<TFirst, TSecond, TResult, TEnumerator1, TEnumerator2>>
        where TEnumerator1 : IEnumerator<TFirst>
        where TEnumerator2 : IEnumerator<TSecond>
    {
        private IEnumerable<TFirst, TEnumerator1> first;
        private IEnumerable<TSecond, TEnumerator2> second;
        private Func<TFirst, TSecond, TResult> resultSelector;

        public ZipEnumerable(IEnumerable<TFirst, TEnumerator1> first, IEnumerable<TSecond, TEnumerator2> second, Func<TFirst, TSecond, TResult> resultSelector)
        {
            this.first = first;
            this.second = second;
            this.resultSelector = resultSelector;
        }

        public override bool GetCount(out int count)
        {
            if (!first.GetCount(out int firstCount) || !second.GetCount(out int secondCount))
            {
                count = 0;
                return false;
            }
            count = System.Math.Min(firstCount, secondCount);
            return true;
        }

        public override ZipEnumerator<TFirst, TSecond, TResult, TEnumerator1, TEnumerator2> GetEnumerator()
        {
            TEnumerator1 firstEnumerator = first.GetEnumerator();
            try
            {
                return new ZipEnumerator<TFirst, TSecond, TResult, TEnumerator1, TEnumerator2>(firstEnumerator, second.GetEnumerator(), resultSelector);
            }
            catch
            {
                firstEnumerator.Dispose();
                throw;
            }
        }
    }
}
