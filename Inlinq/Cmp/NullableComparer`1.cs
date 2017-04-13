using System;
using System.Collections.Generic;

namespace Inlinq.Cmp
{
    public struct NullableComparer<T> : IComparer<T?>
        where T : struct, IComparable<T>
    {
        public int Compare(T? x, T? y)
            => x != null ? y != null ? x.GetValueOrDefault().CompareTo(y.GetValueOrDefault()) : 1 : y != null ? -1 : 0;
        public override bool Equals(object obj)
            => obj is NullableComparer<T>;
        public override int GetHashCode()
            => typeof(NullableComparer<T>).Name.GetHashCode();
    }
}
