using Inlinq.Cmp;
using System.Collections.Generic;

namespace Inlinq
{
    public static class EqualityComparers
    {
        public static NullableEqualityComparer<T, EquatableEqualityComparer<T>> Nullable<T>()
            where T : struct, System.IEquatable<T>
            => new NullableEqualityComparer<T, EquatableEqualityComparer<T>>(new EquatableEqualityComparer<T>());

        public static NullableEqualityComparer<T, TEqualityComparer> Nullable<T, TEqualityComparer>(TEqualityComparer comparer)
            where T : struct
            where TEqualityComparer : IEqualityComparer<T>
            => comparer != null ? new NullableEqualityComparer<T, TEqualityComparer>(comparer) : throw Error.ArgumentNull(nameof(comparer));

        public static NullableEqualityComparer<T, IEqualityComparer<T>> Nullable<T>(IEqualityComparer<T> comparer)
            where T : struct
            => new NullableEqualityComparer<T, IEqualityComparer<T>>(comparer ?? EqualityComparer<T>.Default);

        public static EquatableEqualityComparer<T> Equatable<T>()
            where T : System.IEquatable<T>
            => new EquatableEqualityComparer<T>();

        public static StringComparer String()
            => new StringComparer();
    }
}
