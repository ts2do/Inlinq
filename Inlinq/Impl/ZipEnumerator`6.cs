using System.Collections;
using System.Collections.Generic;

namespace Inlinq.Impl
{
    public struct ZipEnumerator<TFirst, TSecond, TResult, TEnumerator1, TEnumerator2, TResultSelector> : IEnumerator<TResult>
        where TEnumerator1 : IEnumerator<TFirst>
        where TEnumerator2 : IEnumerator<TSecond>
        where TResultSelector : IFunctor<TFirst, TSecond, TResult>
    {
        private TEnumerator1 first;
        private TEnumerator2 second;
        private TResultSelector resultSelector;

        public ZipEnumerator(TEnumerator1 first, TEnumerator2 second, TResultSelector resultSelector)
        {
            this.first = first;
            this.second = second;
            this.resultSelector = resultSelector;
        }

        public TResult Current
            => resultSelector.Invoke(first.Current, second.Current);

        public void Dispose()
        {
            first.Dispose();
            second.Dispose();
        }

        public bool MoveNext()
            => first.MoveNext() && second.MoveNext();

        public void Reset()
        {
            first.Reset();
            second.Reset();
        }

        object IEnumerator.Current => Current;
    }
}
