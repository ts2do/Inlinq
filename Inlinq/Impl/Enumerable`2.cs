using System.Collections;
using System.Collections.Generic;

namespace Inlinq.Impl
{
    public abstract class Enumerable<T, TEnumerator> : IEnumerable<T, TEnumerator>
        where TEnumerator : IEnumerator<T>
    {
        public virtual bool GetCount(out int count)
        {
            count = 0;
            return false;
        }
        public abstract TEnumerator GetEnumerator();
        IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
