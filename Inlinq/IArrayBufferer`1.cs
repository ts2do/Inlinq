using System;
using System.Collections.Generic;

namespace Inlinq
{
    public interface IArrayBufferer<T> : IDisposable
    {
        int Buffer(T[] array);
        IArrayBufferer<T> Change(IEnumerable<T> enumerable);
    }
}
