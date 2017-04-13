using Inlinq.Impl;
using System;
using System.Collections.Generic;

namespace Inlinq
{
    public static partial class Inlinqer
    {
        #region Average
        public static double Average<TEnumerator>(this IEnumerable<long, TEnumerator> source)
            where TEnumerator : IEnumerator<long>
        {
            using (var e = (source ?? throw Error.ArgumentNull(nameof(source))).GetEnumerator())
                return Average_Int64(e);
        }

        public static double Average<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, long> selector)
            where TEnumerator : IEnumerator<TSource>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (selector == null) throw Error.ArgumentNull(nameof(selector));
            using (var e = new SelectEnumerator<TSource, long, TEnumerator>(source.GetEnumerator(), selector))
                return Average_Int64(e);
        }

        public static double? Average<TEnumerator>(this IEnumerable<long?, TEnumerator> source)
            where TEnumerator : IEnumerator<long?>
        {
            using (var e = (source ?? throw Error.ArgumentNull(nameof(source))).GetEnumerator())
                return Average_NullableOfInt64(e);
        }

        public static double? Average<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, long?> selector)
            where TEnumerator : IEnumerator<TSource>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (selector == null) throw Error.ArgumentNull(nameof(selector));
            using (var e = new SelectEnumerator<TSource, long?, TEnumerator>(source.GetEnumerator(), selector))
                return Average_NullableOfInt64(e);
        }

        private static double Average_Int64<TEnumerator>(TEnumerator source)
            where TEnumerator : IEnumerator<long>
        {
            if (source.MoveNext())
            {
                long sum = source.Current;
                long count = 1;
                checked
                {
                    while (source.MoveNext())
                    {
                        sum += source.Current;
                        ++count;
                    }
                }
                return (double)sum / count;
            }
            throw Error.NoElements();
        }

        private static double? Average_NullableOfInt64<TEnumerator>(TEnumerator source)
            where TEnumerator : IEnumerator<long?>
        {
            while (source.MoveNext())
            {
                var x = source.Current;
                if (x == null)
                    continue;

                long sum = x.GetValueOrDefault();
                long count = 1;
                checked
                {
                    while (source.MoveNext())
                    {
                        x = source.Current;
                        if (x != null)
                        {
                            sum += x.GetValueOrDefault();
                            ++count;
                        }
                    }
                    return (double)sum / count;
                }
            }
            return null;
        }
        #endregion

        #region Max
        public static long Max<TEnumerator>(this IEnumerable<long, TEnumerator> source)
            where TEnumerator : IEnumerator<long>
        {
            using (var e = (source ?? throw Error.ArgumentNull(nameof(source))).GetEnumerator())
                return Max_Int64(e);
        }

        public static long Max<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, long> selector)
            where TEnumerator : IEnumerator<TSource>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (selector == null) throw Error.ArgumentNull(nameof(selector));
            using (var e = new SelectEnumerator<TSource, long, TEnumerator>(source.GetEnumerator(), selector))
                return Max_Int64(e);
        }

        public static long? Max<TEnumerator>(this IEnumerable<long?, TEnumerator> source)
            where TEnumerator : IEnumerator<long?>
        {
            using (var e = (source ?? throw Error.ArgumentNull(nameof(source))).GetEnumerator())
                return Max_NullableOfInt64(e);
        }

        public static long? Max<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, long?> selector)
            where TEnumerator : IEnumerator<TSource>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (selector == null) throw Error.ArgumentNull(nameof(selector));
            using (var e = new SelectEnumerator<TSource, long?, TEnumerator>(source.GetEnumerator(), selector))
                return Max_NullableOfInt64(e);
        }

        private static long Max_Int64<TEnumerator>(TEnumerator source)
            where TEnumerator : IEnumerator<long>
        {
            if (source.MoveNext())
            {
                long max = source.Current;
                while (source.MoveNext())
                {
                    var element = source.Current;
                    if (max < element)
                        max = element;
                }
                return max;
            }
            throw Error.NoElements();
        }

        private static long? Max_NullableOfInt64<TEnumerator>(TEnumerator source)
            where TEnumerator : IEnumerator<long?>
        {
            while (source.MoveNext())
            {
                var x = source.Current;
                if (x == null)
                    continue;

                long max = x.GetValueOrDefault();
                while (source.MoveNext())
                {
                    x = source.Current;
                    if (x != null && max < x.GetValueOrDefault())
                        max = x.GetValueOrDefault();
                }
                return max;
            }
            return null;
        }
        #endregion

        #region Min
        public static long Min<TEnumerator>(this IEnumerable<long, TEnumerator> source)
            where TEnumerator : IEnumerator<long>
        {
            using (var e = (source ?? throw Error.ArgumentNull(nameof(source))).GetEnumerator())
                return Min_Int64(e);
        }

        public static long Min<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, long> selector)
            where TEnumerator : IEnumerator<TSource>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (selector == null) throw Error.ArgumentNull(nameof(selector));
            using (var e = new SelectEnumerator<TSource, long, TEnumerator>(source.GetEnumerator(), selector))
                return Min_Int64(e);
        }

        public static long? Min<TEnumerator>(this IEnumerable<long?, TEnumerator> source)
            where TEnumerator : IEnumerator<long?>
        {
            using (var e = (source ?? throw Error.ArgumentNull(nameof(source))).GetEnumerator())
                return Min_NullableOfInt64(e);
        }

        public static long? Min<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, long?> selector)
            where TEnumerator : IEnumerator<TSource>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (selector == null) throw Error.ArgumentNull(nameof(selector));
            using (var e = new SelectEnumerator<TSource, long?, TEnumerator>(source.GetEnumerator(), selector))
                return Min_NullableOfInt64(e);
        }

        private static long Min_Int64<TEnumerator>(TEnumerator source)
            where TEnumerator : IEnumerator<long>
        {
            if (source.MoveNext())
            {
                long min = source.Current;
                while (source.MoveNext())
                {
                    var element = source.Current;
                    if (min > element)
                        min = element;
                }
                return min;
            }
            throw Error.NoElements();
        }

        private static long? Min_NullableOfInt64<TEnumerator>(TEnumerator source)
            where TEnumerator : IEnumerator<long?>
        {
            while (source.MoveNext())
            {
                var x = source.Current;
                if (x == null)
                    continue;

                long min = x.GetValueOrDefault();
                while (source.MoveNext())
                {
                    x = source.Current;
                    if (x != null && min > x.GetValueOrDefault())
                        min = x.GetValueOrDefault();
                }
                return min;
            }
            return null;
        }
        #endregion

        #region Sum
        public static long Sum<TEnumerator>(this IEnumerable<long, TEnumerator> source)
            where TEnumerator : IEnumerator<long>
        {
            using (var e = (source ?? throw Error.ArgumentNull(nameof(source))).GetEnumerator())
                return Sum_Int64(e);
        }

        public static long Sum<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, long> selector)
            where TEnumerator : IEnumerator<TSource>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (selector == null) throw Error.ArgumentNull(nameof(selector));
            using (var e = new SelectEnumerator<TSource, long, TEnumerator>(source.GetEnumerator(), selector))
                return Sum_Int64(e);
        }

        public static long? Sum<TEnumerator>(this IEnumerable<long?, TEnumerator> source)
            where TEnumerator : IEnumerator<long?>
        {
            using (var e = (source ?? throw Error.ArgumentNull(nameof(source))).GetEnumerator())
                return Sum_NullableOfInt64(e);
        }

        public static long? Sum<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, long?> selector)
            where TEnumerator : IEnumerator<TSource>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (selector == null) throw Error.ArgumentNull(nameof(selector));
            using (var e = new SelectEnumerator<TSource, long?, TEnumerator>(source.GetEnumerator(), selector))
                return Sum_NullableOfInt64(e);
        }

        private static long Sum_Int64<TEnumerator>(TEnumerator source)
            where TEnumerator : IEnumerator<long>
        {
            if (source.MoveNext())
            {
                long sum = source.Current;
                checked
                {
                    while (source.MoveNext())
                        sum += source.Current;
                }
                return sum;
            }
            throw Error.NoElements();
        }

        private static long? Sum_NullableOfInt64<TEnumerator>(TEnumerator source)
            where TEnumerator : IEnumerator<long?>
        {
            while (source.MoveNext())
            {
                var x = source.Current;
                if (x == null)
                    continue;

                long sum = x.GetValueOrDefault();
                checked
                {
                    while (source.MoveNext())
                    {
                        x = source.Current;
                        if (x != null)
                            sum += x.GetValueOrDefault();
                    }
                    return sum;
                }
            }
            return null;
        }
        #endregion
    }
}
