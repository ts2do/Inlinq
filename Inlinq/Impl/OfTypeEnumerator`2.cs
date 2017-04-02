using System;
using System.Collections;
using System.Collections.Generic;

namespace Inlinq.Impl
{
    public struct OfTypeEnumerator<T> : IEnumerator<T>
    {
        private IEnumerator source;
        private object currentObject;
        private T currentResult;
        private EnumeratorState state;

        public T Current
            => state.IsStarted() ? currentResult : throw Error.EnumerableStateException(state);

        public OfTypeEnumerator(IEnumerator source)
        {
            this.source = source;
            currentObject = null;
            currentResult = default(T);
            state = EnumeratorState.Initial;
        }

        public void Dispose()
            => (source as IDisposable)?.Dispose();

        public bool MoveNext()
        {
            switch (state)
            {
                default:
                    while (source.MoveNext())
                    {
                        var x = source.Current;
                        if (x is T result)
                        {
                            currentObject = x;
                            currentResult = result;
                            return true;
                        }
                    }
                    state = EnumeratorState.Ended;
                    return false;

                case EnumeratorState.Initial:
                    state = EnumeratorState.Started;
                    goto default;

                case EnumeratorState.Ended:
                    return false;
            }
        }

        public void Reset()
        {
            source.Reset();
            currentObject = null;
            currentResult = default(T);
            state = EnumeratorState.Initial;
        }

        object IEnumerator.Current => currentObject;
    }
}
