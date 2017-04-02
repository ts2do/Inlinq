using System.Collections.Generic;

namespace Inlinq.Cmp
{
    public struct NullableComparer<T, TComparer> : IComparer<T?>
        where T : struct
        where TComparer : IComparer<T>
    {
        private TComparer comparer;
        public NullableComparer(TComparer comparer)
            => this.comparer = comparer;
        public int Compare(T? x, T? y)
            => x != null ? y != null ? comparer.Compare(x.GetValueOrDefault(), y.GetValueOrDefault()) : 1 : y != null ? -1 : 0;
        public override bool Equals(object obj)
            => obj is NullableComparer<T, TComparer> x && comparer.Equals(x.comparer);
        public override int GetHashCode()
            => typeof(NullableComparer<T, TComparer>).Name.GetHashCode() ^ comparer.GetHashCode();
    }
}
