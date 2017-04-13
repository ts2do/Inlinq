using System.Collections.Generic;

namespace Inlinq.Cmp
{
    public sealed class DefaultEqualityComparer<T> : IEqualityComparer<T>
    {
        private EqualityComparer<T> defaultComparer = EqualityComparer<T>.Default;
        public bool Equals(T x, T y) => defaultComparer.Equals(x, y);
        public int GetHashCode(T obj) => defaultComparer.GetHashCode(obj);
        public override bool Equals(object obj)
            => obj is DefaultEqualityComparer<T>;
        public override int GetHashCode()
            => typeof(DefaultEqualityComparer<T>).Name.GetHashCode();
    }
}
