using System.Collections.Generic;

namespace Inlinq
{
    public interface IEnumerable<T, out TEnumerator, out TComparer, out TEqualityComparer> : IEnumerable<T, TEnumerator>
        where TEnumerator : IEnumerator<T>
        where TComparer : IComparer<T>
        where TEqualityComparer : IEqualityComparer<T>
    {
        TComparer DefaultComparer { get; }
        TEqualityComparer DefaultEqualityComparer { get; }
    }
}
