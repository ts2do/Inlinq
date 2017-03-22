using System;
using System.Collections;
using System.Collections.Generic;

namespace Inlinq.Impl
{
    public struct CastEnumerator<T> : IEnumerator<T>
    {
        private IEnumerator source;
        public T Current => (T)source.Current;
        public CastEnumerator(IEnumerator source) => this.source = source;
        public void Dispose() => (source as IDisposable)?.Dispose();
        public bool MoveNext() => source.MoveNext();
        public void Reset() => source.Reset();
        object IEnumerator.Current => Current;
    }
}
