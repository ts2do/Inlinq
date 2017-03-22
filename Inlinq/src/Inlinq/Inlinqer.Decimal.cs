using Inlinq.Impl;
using System;
using System.Collections.Generic;

namespace Inlinq
{
    public static partial class Inlinqer
    {
        #region Average
        public static decimal Average<TEnumerator>(this IEnumerable<decimal, TEnumerator> source)
            where TEnumerator : IEnumerator<decimal>
        {
            using (var e = (source ?? throw Error.ArgumentNull(nameof(source))).GetEnumerator())
                return Average_Decimal(e);
        }

        public static decimal AveragIEnumerable<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, decimal> selector)
            where TEnumerator : IEnumerator<TSource>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (selector == null) throw Error.ArgumentNull(nameof(selector));
            using (var e = new SelectEnumerator<TSource, decimal, TEnumerator, FuncFunc<TSource, decimal>>(source.GetEnumerator(), new FuncFunc<TSource, decimal>(selector)))
                return Average_Decimal(e);
        }

        public static decimal? Average<TEnumerator>(this IEnumerable<decimal?, TEnumerator> source)
            where TEnumerator : IEnumerator<decimal?>
        {
            using (var e = (source ?? throw Error.ArgumentNull(nameof(source))).GetEnumerator())
                return Average_NullableOfDecimal(e);
        }

        public static decimal? AveragIEnumerable<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, decimal?> selector)
            where TEnumerator : IEnumerator<TSource>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (selector == null) throw Error.ArgumentNull(nameof(selector));
            using (var e = new SelectEnumerator<TSource, decimal?, TEnumerator, FuncFunc<TSource, decimal?>>(source.GetEnumerator(), new FuncFunc<TSource, decimal?>(selector)))
                return Average_NullableOfDecimal(e);
        }

        private static decimal Average_Decimal<TEnumerator>(TEnumerator source)
            where TEnumerator : IEnumerator<decimal>
        {
            if (!source.MoveNext()) throw Error.NoElements();
            decimal sum = source.Current;
            long count = 1;
            checked
            {
                while (source.MoveNext())
                {
                    sum += source.Current;
                    ++count;
                }
            }
            return sum / count;
        }

        private static decimal? Average_NullableOfDecimal<TEnumerator>(TEnumerator source)
            where TEnumerator : IEnumerator<decimal?>
        {
            while (source.MoveNext())
            {
                var x = source.Current;
                if (x == null)
                    continue;

                decimal sum = x.GetValueOrDefault();
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
                    return sum / count;
                }
            }
            return null;
        }
        #endregion

        #region Max
        public static decimal Max<TEnumerator>(this IEnumerable<decimal, TEnumerator> source)
            where TEnumerator : IEnumerator<decimal>
        {
            using (var e = (source ?? throw Error.ArgumentNull(nameof(source))).GetEnumerator())
                return Max_Decimal(e);
        }

        public static decimal Max<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, decimal> selector)
            where TEnumerator : IEnumerator<TSource>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (selector == null) throw Error.ArgumentNull(nameof(selector));
            using (var e = new SelectEnumerator<TSource, decimal, TEnumerator, FuncFunc<TSource, decimal>>(source.GetEnumerator(), new FuncFunc<TSource, decimal>(selector)))
                return Max_Decimal(e);
        }

        public static decimal? Max<TEnumerator>(this IEnumerable<decimal?, TEnumerator> source)
            where TEnumerator : IEnumerator<decimal?>
        {
            using (var e = (source ?? throw Error.ArgumentNull(nameof(source))).GetEnumerator())
                return Max_NullableOfDecimal(e);
        }

        public static decimal? Max<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, decimal?> selector)
            where TEnumerator : IEnumerator<TSource>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (selector == null) throw Error.ArgumentNull(nameof(selector));
            using (var e = new SelectEnumerator<TSource, decimal?, TEnumerator, FuncFunc<TSource, decimal?>>(source.GetEnumerator(), new FuncFunc<TSource, decimal?>(selector)))
                return Max_NullableOfDecimal(e);
        }

        private static decimal Max_Decimal<TEnumerator>(TEnumerator source)
            where TEnumerator : IEnumerator<decimal>
        {
            if (!source.MoveNext()) throw Error.NoElements();
            decimal max = source.Current;
            while (source.MoveNext())
            {
                var element = source.Current;
                if (max < element)
                    max = element;
            }
            return max;
        }

        private static decimal? Max_NullableOfDecimal<TEnumerator>(TEnumerator source)
            where TEnumerator : IEnumerator<decimal?>
        {
            while (source.MoveNext())
            {
                var x = source.Current;
                if (x == null)
                    continue;

                decimal max = x.GetValueOrDefault();
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
        public static decimal Min<TEnumerator>(this IEnumerable<decimal, TEnumerator> source)
            where TEnumerator : IEnumerator<decimal>
        {
            using (var e = (source ?? throw Error.ArgumentNull(nameof(source))).GetEnumerator())
                return Min_Decimal(e);
        }

        public static decimal Min<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, decimal> selector)
            where TEnumerator : IEnumerator<TSource>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (selector == null) throw Error.ArgumentNull(nameof(selector));
            using (var e = new SelectEnumerator<TSource, decimal, TEnumerator, FuncFunc<TSource, decimal>>(source.GetEnumerator(), new FuncFunc<TSource, decimal>(selector)))
                return Min_Decimal(e);
        }

        public static decimal? Min<TEnumerator>(this IEnumerable<decimal?, TEnumerator> source)
            where TEnumerator : IEnumerator<decimal?>
        {
            using (var e = (source ?? throw Error.ArgumentNull(nameof(source))).GetEnumerator())
                return Min_NullableOfDecimal(e);
        }

        public static decimal? Min<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, decimal?> selector)
            where TEnumerator : IEnumerator<TSource>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (selector == null) throw Error.ArgumentNull(nameof(selector));
            using (var e = new SelectEnumerator<TSource, decimal?, TEnumerator, FuncFunc<TSource, decimal?>>(source.GetEnumerator(), new FuncFunc<TSource, decimal?>(selector)))
                return Min_NullableOfDecimal(e);
        }

        private static decimal Min_Decimal<TEnumerator>(TEnumerator source)
            where TEnumerator : IEnumerator<decimal>
        {
            if (!source.MoveNext()) throw Error.NoElements();
            decimal min = source.Current;
            while (source.MoveNext())
            {
                var element = source.Current;
                if (min > element)
                    min = element;
            }
            return min;
        }

        private static decimal? Min_NullableOfDecimal<TEnumerator>(TEnumerator source)
            where TEnumerator : IEnumerator<decimal?>
        {
            while (source.MoveNext())
            {
                var x = source.Current;
                if (x == null)
                    continue;

                decimal min = x.GetValueOrDefault();
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
        public static decimal Sum<TEnumerator>(this IEnumerable<decimal, TEnumerator> source)
            where TEnumerator : IEnumerator<decimal>
        {
            using (var e = (source ?? throw Error.ArgumentNull(nameof(source))).GetEnumerator())
                return Sum_Decimal(e);
        }

        public static decimal Sum<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, decimal> selector)
            where TEnumerator : IEnumerator<TSource>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (selector == null) throw Error.ArgumentNull(nameof(selector));
            using (var e = new SelectEnumerator<TSource, decimal, TEnumerator, FuncFunc<TSource, decimal>>(source.GetEnumerator(), new FuncFunc<TSource, decimal>(selector)))
                return Sum_Decimal(e);
        }

        public static decimal? Sum<TEnumerator>(this IEnumerable<decimal?, TEnumerator> source)
            where TEnumerator : IEnumerator<decimal?>
        {
            using (var e = (source ?? throw Error.ArgumentNull(nameof(source))).GetEnumerator())
                return Sum_NullableOfDecimal(e);
        }

        public static decimal? Sum<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, decimal?> selector)
            where TEnumerator : IEnumerator<TSource>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (selector == null) throw Error.ArgumentNull(nameof(selector));
            using (var e = new SelectEnumerator<TSource, decimal?, TEnumerator, FuncFunc<TSource, decimal?>>(source.GetEnumerator(), new FuncFunc<TSource, decimal?>(selector)))
                return Sum_NullableOfDecimal(e);
        }

        private static decimal Sum_Decimal<TEnumerator>(TEnumerator source)
            where TEnumerator : IEnumerator<decimal>
        {
            if (!source.MoveNext()) throw Error.NoElements();
            decimal sum = source.Current;
            checked
            {
                while (source.MoveNext())
                    sum += source.Current;
            }
            return sum;
        }

        private static decimal? Sum_NullableOfDecimal<TEnumerator>(TEnumerator source)
            where TEnumerator : IEnumerator<decimal?>
        {
            while (source.MoveNext())
            {
                var x = source.Current;
                if (x == null)
                    continue;

                decimal sum = x.GetValueOrDefault();
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
