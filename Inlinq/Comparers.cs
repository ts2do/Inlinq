using Inlinq.Cmp;
using System.Collections.Generic;

namespace Inlinq
{
    public static class Comparers
    {
        public static NullableComparer<T, ComparableComparer<T>> Nullable<T>()
            where T : struct, System.IComparable<T>
            => new NullableComparer<T, ComparableComparer<T>>(new ComparableComparer<T>());

        public static NullableComparer<T, TComparer> Nullable<T, TComparer>(TComparer comparer)
            where T : struct
            where TComparer : struct, IComparer<T>
            => new NullableComparer<T, TComparer>(comparer);

        public static NullableComparer<T, IComparer<T>> Nullable<T>(IComparer<T> comparer)
            where T : struct
            => new NullableComparer<T, IComparer<T>>(comparer ?? Comparer<T>.Default);

        public static ComparableComparer<T> Comparable<T>()
            where T : System.IComparable<T>
            => new ComparableComparer<T>();

        public static StringComparer String()
            => new StringComparer();

        public static ReverseComparer<T, ComparableComparer<T>> Reverse<T>()
            where T : System.IComparable<T>
            => new ReverseComparer<T, ComparableComparer<T>>(new ComparableComparer<T>());

        public static ReverseComparer<T, TComparer> Reverse<T, TComparer>(TComparer comparer)
            where TComparer : struct, IComparer<T>
            => new ReverseComparer<T, TComparer>(comparer);

        public static ReverseComparer<T, IComparer<T>> Reverse<T>(IComparer<T> comparer)
            => new ReverseComparer<T, IComparer<T>>(comparer ?? Comparer<T>.Default);
    }
}
