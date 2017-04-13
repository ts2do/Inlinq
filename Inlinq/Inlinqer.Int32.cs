﻿using Inlinq.Impl;
using System;
using System.Collections.Generic;

namespace Inlinq
{
    public static partial class Inlinqer
    {
        #region Average
        public static double Average<TEnumerator>(this IEnumerable<int, TEnumerator> source)
            where TEnumerator : IEnumerator<int>
        {
            using (var e = (source ?? throw Error.ArgumentNull(nameof(source))).GetEnumerator())
                return Average_Int32(e);
        }

        public static double Average<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, int> selector)
            where TEnumerator : IEnumerator<TSource>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (selector == null) throw Error.ArgumentNull(nameof(selector));
            using (var e = new SelectEnumerator<TSource, int, TEnumerator>(source.GetEnumerator(), selector))
                return Average_Int32(e);
        }

        public static double? Average<TEnumerator>(this IEnumerable<int?, TEnumerator> source)
            where TEnumerator : IEnumerator<int?>
        {
            using (var e = (source ?? throw Error.ArgumentNull(nameof(source))).GetEnumerator())
                return Average_NullableOfInt32(e);
        }

        public static double? Average<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, int?> selector)
            where TEnumerator : IEnumerator<TSource>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (selector == null) throw Error.ArgumentNull(nameof(selector));
            using (var e = new SelectEnumerator<TSource, int?, TEnumerator>(source.GetEnumerator(), selector))
                return Average_NullableOfInt32(e);
        }

        private static double Average_Int32<TEnumerator>(TEnumerator source)
            where TEnumerator : IEnumerator<int>
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

        private static double? Average_NullableOfInt32<TEnumerator>(TEnumerator source)
            where TEnumerator : IEnumerator<int?>
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
        public static int Max<TEnumerator>(this IEnumerable<int, TEnumerator> source)
            where TEnumerator : IEnumerator<int>
        {
            using (var e = (source ?? throw Error.ArgumentNull(nameof(source))).GetEnumerator())
                return Max_Int32(e);
        }

        public static int Max<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, int> selector)
            where TEnumerator : IEnumerator<TSource>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (selector == null) throw Error.ArgumentNull(nameof(selector));
            using (var e = new SelectEnumerator<TSource, int, TEnumerator>(source.GetEnumerator(), selector))
                return Max_Int32(e);
        }

        public static int? Max<TEnumerator>(this IEnumerable<int?, TEnumerator> source)
            where TEnumerator : IEnumerator<int?>
        {
            using (var e = (source ?? throw Error.ArgumentNull(nameof(source))).GetEnumerator())
                return Max_NullableOfInt32(e);
        }

        public static int? Max<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, int?> selector)
            where TEnumerator : IEnumerator<TSource>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (selector == null) throw Error.ArgumentNull(nameof(selector));
            using (var e = new SelectEnumerator<TSource, int?, TEnumerator>(source.GetEnumerator(), selector))
                return Max_NullableOfInt32(e);
        }

        private static int Max_Int32<TEnumerator>(TEnumerator source)
            where TEnumerator : IEnumerator<int>
        {
            if (source.MoveNext())
            {
                int max = source.Current;
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

        private static int? Max_NullableOfInt32<TEnumerator>(TEnumerator source)
            where TEnumerator : IEnumerator<int?>
        {
            while (source.MoveNext())
            {
                var x = source.Current;
                if (x == null)
                    continue;

                int max = x.GetValueOrDefault();
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
        public static int Min<TEnumerator>(this IEnumerable<int, TEnumerator> source)
            where TEnumerator : IEnumerator<int>
        {
            using (var e = (source ?? throw Error.ArgumentNull(nameof(source))).GetEnumerator())
                return Min_Int32(e);
        }

        public static int Min<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, int> selector)
            where TEnumerator : IEnumerator<TSource>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (selector == null) throw Error.ArgumentNull(nameof(selector));
            using (var e = new SelectEnumerator<TSource, int, TEnumerator>(source.GetEnumerator(), selector))
                return Min_Int32(e);
        }

        public static int? Min<TEnumerator>(this IEnumerable<int?, TEnumerator> source)
            where TEnumerator : IEnumerator<int?>
        {
            using (var e = (source ?? throw Error.ArgumentNull(nameof(source))).GetEnumerator())
                return Min_NullableOfInt32(e);
        }

        public static int? Min<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, int?> selector)
            where TEnumerator : IEnumerator<TSource>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (selector == null) throw Error.ArgumentNull(nameof(selector));
            using (var e = new SelectEnumerator<TSource, int?, TEnumerator>(source.GetEnumerator(), selector))
                return Min_NullableOfInt32(e);
        }

        private static int Min_Int32<TEnumerator>(TEnumerator source)
            where TEnumerator : IEnumerator<int>
        {
            if (source.MoveNext())
            {
                int min = source.Current;
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

        private static int? Min_NullableOfInt32<TEnumerator>(TEnumerator source)
            where TEnumerator : IEnumerator<int?>
        {
            while (source.MoveNext())
            {
                var x = source.Current;
                if (x == null)
                    continue;

                int min = x.GetValueOrDefault();
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
        public static int Sum<TEnumerator>(this IEnumerable<int, TEnumerator> source)
            where TEnumerator : IEnumerator<int>
        {
            using (var e = (source ?? throw Error.ArgumentNull(nameof(source))).GetEnumerator())
                return Sum_Int32(e);
        }

        public static int Sum<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, int> selector)
            where TEnumerator : IEnumerator<TSource>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (selector == null) throw Error.ArgumentNull(nameof(selector));
            using (var e = new SelectEnumerator<TSource, int, TEnumerator>(source.GetEnumerator(), selector))
                return Sum_Int32(e);
        }

        public static int? Sum<TEnumerator>(this IEnumerable<int?, TEnumerator> source)
            where TEnumerator : IEnumerator<int?>
        {
            using (var e = (source ?? throw Error.ArgumentNull(nameof(source))).GetEnumerator())
                return Sum_NullableOfInt32(e);
        }

        public static int? Sum<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, int?> selector)
            where TEnumerator : IEnumerator<TSource>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (selector == null) throw Error.ArgumentNull(nameof(selector));
            using (var e = new SelectEnumerator<TSource, int?, TEnumerator>(source.GetEnumerator(), selector))
                return Sum_NullableOfInt32(e);
        }

        private static int Sum_Int32<TEnumerator>(TEnumerator source)
            where TEnumerator : IEnumerator<int>
        {
            if (source.MoveNext())
            {
                int sum = source.Current;
                checked
                {
                    while (source.MoveNext())
                        sum += source.Current;
                }
                return sum;
            }
            throw Error.NoElements();
        }

        private static int? Sum_NullableOfInt32<TEnumerator>(TEnumerator source)
            where TEnumerator : IEnumerator<int?>
        {
            while (source.MoveNext())
            {
                var x = source.Current;
                if (x == null)
                    continue;

                int sum = x.GetValueOrDefault();
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
