using Inlinq.Cmp;
using System.Collections.Generic;

namespace Inlinq
{
    public static class Comparers
    {
        public static NullableComparer<T, ComparableComparer<T>> Nullable<T>()
            where T : struct, System.IComparable<T>
            => new NullableComparer<T, ComparableComparer<T>>(new ComparableComparer<T>());

        public static NullableComparer<T, TEqualityComparer> Nullable<T, TEqualityComparer>(TEqualityComparer comparer)
            where T : struct
            where TEqualityComparer : IComparer<T>
            => comparer != null ? new NullableComparer<T, TEqualityComparer>(comparer) : throw Error.ArgumentNull(nameof(comparer));

        public static NullableComparer<T, IComparer<T>> Nullable<T>(IComparer<T> comparer)
            where T : struct
            => new NullableComparer<T, IComparer<T>>(comparer ?? Comparer<T>.Default);

        public static ComparableComparer<T> Comparable<T>()
            where T : System.IComparable<T>
            => new ComparableComparer<T>();

        public static StringComparer String()
            => new StringComparer();
    }
}
