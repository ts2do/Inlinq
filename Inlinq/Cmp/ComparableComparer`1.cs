using System;
using System.Collections.Generic;

namespace Inlinq.Cmp
{
    public struct ComparableComparer<T> : IComparer<T>
        where T : IComparable<T>
    {
        public int Compare(T x, T y)
            => x?.CompareTo(y) ?? (y != null ? -1 : 0);
        public override bool Equals(object obj)
            => obj is ComparableComparer<T>;
        public override int GetHashCode()
            => typeof(ComparableComparer<T>).Name.GetHashCode();
    }
}
