using System.Collections.Generic;

namespace Inlinq.Cmp
{
    public struct NullableEqualityComparer<T, TEqualityComparer> : IEqualityComparer<T?>
        where T : struct
        where TEqualityComparer : IEqualityComparer<T>
    {
        private TEqualityComparer comparer;
        public NullableEqualityComparer(TEqualityComparer comparer) => this.comparer = comparer;
        public bool Equals(T? x, T? y)
            => x != null ? y != null && comparer.Equals(x.GetValueOrDefault(), y.GetValueOrDefault()) : y == null;
        public int GetHashCode(T? obj)
            => obj != null ? comparer.GetHashCode(obj.GetValueOrDefault()) : 0;
        public override bool Equals(object obj)
            => obj is NullableEqualityComparer<T, TEqualityComparer> x && comparer.Equals(x.comparer);
        public override int GetHashCode()
            => typeof(NullableEqualityComparer<T, TEqualityComparer>).Name.GetHashCode() ^ comparer.GetHashCode();
    }
}
