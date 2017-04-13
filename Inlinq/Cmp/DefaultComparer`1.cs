using System.Collections.Generic;

namespace Inlinq.Cmp
{
    public sealed class DefaultComparer<T> : IComparer<T>
    {
        private Comparer<T> defaultComparer = Comparer<T>.Default;
        public int Compare(T x, T y) => defaultComparer.Compare(x, y);
        public override bool Equals(object obj)
            => obj is DefaultComparer<T>;
        public override int GetHashCode()
            => typeof(DefaultComparer<T>).Name.GetHashCode();
    }
}
