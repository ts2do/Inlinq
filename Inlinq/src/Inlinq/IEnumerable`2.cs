using System.Collections.Generic;

namespace Inlinq
{
    public interface IEnumerable<T, out TEnumerator> : IEnumerable<T>
        where TEnumerator : IEnumerator<T>
    {
        bool GetCount(out int count);
        new TEnumerator GetEnumerator();
    }
}
