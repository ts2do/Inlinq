using System;
using System.Collections;
using System.Collections.Generic;

namespace Inlinq.Impl
{
    public struct SelectManyEnumeratorC<TSource, TResult, TEnumerator> : IEnumerator<TResult>
        where TEnumerator : IEnumerator<TSource>
    {
        private TEnumerator source;
        private Func<TSource, IEnumerable<TResult>> selector;
        private IEnumerator<TResult> resultEnumerator;
        private EnumeratorState state;

        public TResult Current
            => state.IsStarted() ? resultEnumerator.Current : throw Error.EnumerableStateException(state);

        public SelectManyEnumeratorC(TEnumerator source, Func<TSource, IEnumerable<TResult>> selector)
        {
            this.source = source;
            this.selector = selector;
            resultEnumerator = null;
            state = EnumeratorState.Initial;
        }

        public void Dispose()
        {
            source.Dispose();
            resultEnumerator?.Dispose();
        }

        public bool MoveNext()
        {
            switch (state)
            {
                default:
                    if (resultEnumerator.MoveNext())
                        return true;

                    resultEnumerator.Dispose();
                    break;

                case EnumeratorState.Initial:
                    state = EnumeratorState.Started;
                    break;

                case EnumeratorState.Ended:
                    return false;
            }

            while (source.MoveNext())
            {
                // keep querying source until a non-empty IEnumerator<> is acquired
                var re1 = selector(source.Current);
                if (re1 != null)
                {
                    var re2 = re1.GetEnumerator();
                    if (re2.MoveNext())
                    {
                        resultEnumerator = re2;
                        return true;
                    }

                    re2.Dispose();
                }
            }

            resultEnumerator = null;
            state = EnumeratorState.Ended;
            return false;
        }

        public void Reset()
        {
            source.Reset();
            if (resultEnumerator != null)
            {
                resultEnumerator.Dispose();
                resultEnumerator = null;
            }
            state = EnumeratorState.Initial;
        }

        object IEnumerator.Current => Current;
    }
}
