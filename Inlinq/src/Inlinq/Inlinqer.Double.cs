using Inlinq.Impl;
using System;
using System.Collections.Generic;

namespace Inlinq
{
    public static partial class Inlinqer
    {
        #region Average
        public static double Average<TEnumerator>(this IEnumerable<double, TEnumerator> source)
            where TEnumerator : IEnumerator<double>
        {
            using (var e = (source ?? throw Error.ArgumentNull(nameof(source))).GetEnumerator())
                return Average_Double(e);
        }

        public static double Average<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, double> selector)
            where TEnumerator : IEnumerator<TSource>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (selector == null) throw Error.ArgumentNull(nameof(selector));
            using (var e = new SelectEnumerator<TSource, double, TEnumerator, FuncFunc<TSource, double>>(source.GetEnumerator(), new FuncFunc<TSource, double>(selector)))
                return Average_Double(e);
        }

        public static double? Average<TEnumerator>(this IEnumerable<double?, TEnumerator> source)
            where TEnumerator : IEnumerator<double?>
        {
            using (var e = (source ?? throw Error.ArgumentNull(nameof(source))).GetEnumerator())
                return Average_NullableOfDouble(e);
        }

        public static double? Average<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, double?> selector)
            where TEnumerator : IEnumerator<TSource>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (selector == null) throw Error.ArgumentNull(nameof(selector));
            using (var e = new SelectEnumerator<TSource, double?, TEnumerator, FuncFunc<TSource, double?>>(source.GetEnumerator(), new FuncFunc<TSource, double?>(selector)))
                return Average_NullableOfDouble(e);
        }

        private static double Average_Double<TEnumerator>(TEnumerator source)
            where TEnumerator : IEnumerator<double>
        {
            if (!source.MoveNext()) throw Error.NoElements();
            double sum = source.Current;
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

        private static double? Average_NullableOfDouble<TEnumerator>(TEnumerator source)
            where TEnumerator : IEnumerator<double?>
        {
            while (source.MoveNext())
            {
                var x = source.Current;
                if (x == null)
                    continue;

                double sum = x.GetValueOrDefault();
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
        public static double Max<TEnumerator>(this IEnumerable<double, TEnumerator> source)
            where TEnumerator : IEnumerator<double>
        {
            using (var e = (source ?? throw Error.ArgumentNull(nameof(source))).GetEnumerator())
                return Max_Double(e);
        }

        public static double Max<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, double> selector)
            where TEnumerator : IEnumerator<TSource>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (selector == null) throw Error.ArgumentNull(nameof(selector));
            using (var e = new SelectEnumerator<TSource, double, TEnumerator, FuncFunc<TSource, double>>(source.GetEnumerator(), new FuncFunc<TSource, double>(selector)))
                return Max_Double(e);
        }

        public static double? Max<TEnumerator>(this IEnumerable<double?, TEnumerator> source)
            where TEnumerator : IEnumerator<double?>
        {
            using (var e = (source ?? throw Error.ArgumentNull(nameof(source))).GetEnumerator())
                return Max_NullableOfDouble(e);
        }

        public static double? Max<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, double?> selector)
            where TEnumerator : IEnumerator<TSource>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (selector == null) throw Error.ArgumentNull(nameof(selector));
            using (var e = new SelectEnumerator<TSource, double?, TEnumerator, FuncFunc<TSource, double?>>(source.GetEnumerator(), new FuncFunc<TSource, double?>(selector)))
                return Max_NullableOfDouble(e);
        }

        private static double Max_Double<TEnumerator>(TEnumerator source)
            where TEnumerator : IEnumerator<double>
        {
            if (!source.MoveNext()) throw Error.NoElements();
            double max = source.Current;
            while (source.MoveNext())
            {
                var element = source.Current;
                if (!(max >= element))
                    max = element;
            }
            return max;
        }

        private static double? Max_NullableOfDouble<TEnumerator>(TEnumerator source)
            where TEnumerator : IEnumerator<double?>
        {
            while (source.MoveNext())
            {
                var x = source.Current;
                if (x == null)
                    continue;

                double max = x.GetValueOrDefault();
                while (source.MoveNext())
                {
                    x = source.Current;
                    if (x != null && !(max >= x.GetValueOrDefault()))
                        max = x.GetValueOrDefault();
                }
                return max;
            }
            return null;
        }
        #endregion

        #region Min
        public static double Min<TEnumerator>(this IEnumerable<double, TEnumerator> source)
            where TEnumerator : IEnumerator<double>
        {
            using (var e = (source ?? throw Error.ArgumentNull(nameof(source))).GetEnumerator())
                return Min_Double(e);
        }

        public static double Min<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, double> selector)
            where TEnumerator : IEnumerator<TSource>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (selector == null) throw Error.ArgumentNull(nameof(selector));
            using (var e = new SelectEnumerator<TSource, double, TEnumerator, FuncFunc<TSource, double>>(source.GetEnumerator(), new FuncFunc<TSource, double>(selector)))
                return Min_Double(e);
        }

        public static double? Min<TEnumerator>(this IEnumerable<double?, TEnumerator> source)
            where TEnumerator : IEnumerator<double?>
        {
            using (var e = (source ?? throw Error.ArgumentNull(nameof(source))).GetEnumerator())
                return Min_NullableOfDouble(e);
        }

        public static double? Min<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, double?> selector)
            where TEnumerator : IEnumerator<TSource>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (selector == null) throw Error.ArgumentNull(nameof(selector));
            using (var e = new SelectEnumerator<TSource, double?, TEnumerator, FuncFunc<TSource, double?>>(source.GetEnumerator(), new FuncFunc<TSource, double?>(selector)))
                return Min_NullableOfDouble(e);
        }

        private static double Min_Double<TEnumerator>(TEnumerator source)
            where TEnumerator : IEnumerator<double>
        {
            if (!source.MoveNext()) throw Error.NoElements();
            double min = source.Current;
            while (source.MoveNext())
            {
                var element = source.Current;
                if (!(min <= element))
                    min = element;
            }
            return min;
        }

        private static double? Min_NullableOfDouble<TEnumerator>(TEnumerator source)
            where TEnumerator : IEnumerator<double?>
        {
            while (source.MoveNext())
            {
                var x = source.Current;
                if (x == null)
                    continue;

                double min = x.GetValueOrDefault();
                while (source.MoveNext())
                {
                    x = source.Current;
                    if (x != null && !(min <= x.GetValueOrDefault()))
                        min = x.GetValueOrDefault();
                }
                return min;
            }
            return null;
        }
        #endregion

        #region Sum
        public static double Sum<TEnumerator>(this IEnumerable<double, TEnumerator> source)
            where TEnumerator : IEnumerator<double>
        {
            using (var e = (source ?? throw Error.ArgumentNull(nameof(source))).GetEnumerator())
                return Sum_Double(e);
        }

        public static double Sum<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, double> selector)
            where TEnumerator : IEnumerator<TSource>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (selector == null) throw Error.ArgumentNull(nameof(selector));
            using (var e = new SelectEnumerator<TSource, double, TEnumerator, FuncFunc<TSource, double>>(source.GetEnumerator(), new FuncFunc<TSource, double>(selector)))
                return Sum_Double(e);
        }

        public static double? Sum<TEnumerator>(this IEnumerable<double?, TEnumerator> source)
            where TEnumerator : IEnumerator<double?>
        {
            using (var e = (source ?? throw Error.ArgumentNull(nameof(source))).GetEnumerator())
                return Sum_NullableOfDouble(e);
        }

        public static double? Sum<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, double?> selector)
            where TEnumerator : IEnumerator<TSource>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (selector == null) throw Error.ArgumentNull(nameof(selector));
            using (var e = new SelectEnumerator<TSource, double?, TEnumerator, FuncFunc<TSource, double?>>(source.GetEnumerator(), new FuncFunc<TSource, double?>(selector)))
                return Sum_NullableOfDouble(e);
        }

        private static double Sum_Double<TEnumerator>(TEnumerator source)
            where TEnumerator : IEnumerator<double>
        {
            if (!source.MoveNext()) throw Error.NoElements();
            double sum = source.Current;
            checked
            {
                while (source.MoveNext())
                    sum += source.Current;
            }
            return sum;
        }

        private static double? Sum_NullableOfDouble<TEnumerator>(TEnumerator source)
            where TEnumerator : IEnumerator<double?>
        {
            while (source.MoveNext())
            {
                var x = source.Current;
                if (x == null)
                    continue;

                double sum = x.GetValueOrDefault();
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
