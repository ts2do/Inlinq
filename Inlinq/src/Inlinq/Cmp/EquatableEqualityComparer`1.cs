using System;
using System.Collections.Generic;

namespace Inlinq.Cmp
{
    public struct EquatableEqualityComparer<T> : IEqualityComparer<T>
        where T : IEquatable<T>
    {
        public bool Equals(T x, T y)
            => x?.Equals(y) ?? y == null;
        public int GetHashCode(T obj)
            => obj?.GetHashCode() ?? 0;
        public override bool Equals(object obj)
            => obj is EquatableEqualityComparer<T>;
        public override int GetHashCode()
            => typeof(EquatableEqualityComparer<T>).Name.GetHashCode();
    }
}
