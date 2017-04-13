using System;
using System.Collections.Generic;

namespace Inlinq.Cmp
{
    public struct NullableEqualityComparer<T> : IEqualityComparer<T?>
        where T : struct, IEquatable<T>
    {
        public bool Equals(T? x, T? y)
            => x != null ? y != null && x.GetValueOrDefault().Equals(y.GetValueOrDefault()) : y == null;
        public int GetHashCode(T? obj) => obj.GetHashCode();
        public override bool Equals(object obj)
            => obj is NullableEqualityComparer<T>;
        public override int GetHashCode()
            => typeof(NullableEqualityComparer<T>).Name.GetHashCode();
    }
}
