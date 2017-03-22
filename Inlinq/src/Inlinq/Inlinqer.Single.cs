using Inlinq.Impl;
using System;
using System.Collections.Generic;

namespace Inlinq
{
    public static partial class Inlinqer
    {
        #region Average
        public static float Average<TEnumerator>(this IEnumerable<float, TEnumerator> source)
            where TEnumerator : IEnumerator<float>
        {
            using (var e = (source ?? throw Error.ArgumentNull(nameof(source))).GetEnumerator())
                return Average_Single(e);
        }

        public static float Average<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, float> selector)
            where TEnumerator : IEnumerator<TSource>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (selector == null) throw Error.ArgumentNull(nameof(selector));
            using (var e = new SelectEnumerator<TSource, float, TEnumerator, FuncFunctor<TSource, float>>(source.GetEnumerator(), new FuncFunctor<TSource, float>(selector)))
                return Average_Single(e);
        }

        public static float? Average<TEnumerator>(this IEnumerable<float?, TEnumerator> source)
            where TEnumerator : IEnumerator<float?>
        {
            using (var e = (source ?? throw Error.ArgumentNull(nameof(source))).GetEnumerator())
                return Average_NullableOfSingle(e);
        }

        public static float? Average<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, float?> selector)
            where TEnumerator : IEnumerator<TSource>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (selector == null) throw Error.ArgumentNull(nameof(selector));
            using (var e = new SelectEnumerator<TSource, float?, TEnumerator, FuncFunctor<TSource, float?>>(source.GetEnumerator(), new FuncFunctor<TSource, float?>(selector)))
                return Average_NullableOfSingle(e);
        }

        private static float Average_Single<TEnumerator>(TEnumerator source)
            where TEnumerator : IEnumerator<float>
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
            return (float)(sum / count);
        }

        private static float? Average_NullableOfSingle<TEnumerator>(TEnumerator source)
            where TEnumerator : IEnumerator<float?>
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
                    return (float)(sum / count);
                }
            }
            return null;
        }
        #endregion

        #region Max
        public static float Max<TEnumerator>(this IEnumerable<float, TEnumerator> source)
            where TEnumerator : IEnumerator<float>
        {
            using (var e = (source ?? throw Error.ArgumentNull(nameof(source))).GetEnumerator())
                return Max_Single(e);
        }

        public static float Max<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, float> selector)
            where TEnumerator : IEnumerator<TSource>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (selector == null) throw Error.ArgumentNull(nameof(selector));
            using (var e = new SelectEnumerator<TSource, float, TEnumerator, FuncFunctor<TSource, float>>(source.GetEnumerator(), new FuncFunctor<TSource, float>(selector)))
                return Max_Single(e);
        }

        public static float? Max<TEnumerator>(this IEnumerable<float?, TEnumerator> source)
            where TEnumerator : IEnumerator<float?>
        {
            using (var e = (source ?? throw Error.ArgumentNull(nameof(source))).GetEnumerator())
                return Max_NullableOfSingle(e);
        }

        public static float? Max<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, float?> selector)
            where TEnumerator : IEnumerator<TSource>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (selector == null) throw Error.ArgumentNull(nameof(selector));
            using (var e = new SelectEnumerator<TSource, float?, TEnumerator, FuncFunctor<TSource, float?>>(source.GetEnumerator(), new FuncFunctor<TSource, float?>(selector)))
                return Max_NullableOfSingle(e);
        }

        private static float Max_Single<TEnumerator>(TEnumerator source)
            where TEnumerator : IEnumerator<float>
        {
            if (!source.MoveNext()) throw Error.NoElements();
            float max = source.Current;
            while (source.MoveNext())
            {
                var element = source.Current;
                if (!(max >= element))
                    max = element;
            }
            return max;
        }

        private static float? Max_NullableOfSingle<TEnumerator>(TEnumerator source)
            where TEnumerator : IEnumerator<float?>
        {
            while (source.MoveNext())
            {
                var x = source.Current;
                if (x == null)
                    continue;

                float max = x.GetValueOrDefault();
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
        public static float Min<TEnumerator>(this IEnumerable<float, TEnumerator> source)
            where TEnumerator : IEnumerator<float>
        {
            using (var e = (source ?? throw Error.ArgumentNull(nameof(source))).GetEnumerator())
                return Min_Single(e);
        }

        public static float Min<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, float> selector)
            where TEnumerator : IEnumerator<TSource>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (selector == null) throw Error.ArgumentNull(nameof(selector));
            using (var e = new SelectEnumerator<TSource, float, TEnumerator, FuncFunctor<TSource, float>>(source.GetEnumerator(), new FuncFunctor<TSource, float>(selector)))
                return Min_Single(e);
        }

        public static float? Min<TEnumerator>(this IEnumerable<float?, TEnumerator> source)
            where TEnumerator : IEnumerator<float?>
        {
            using (var e = (source ?? throw Error.ArgumentNull(nameof(source))).GetEnumerator())
                return Min_NullableOfSingle(e);
        }

        public static float? Min<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, float?> selector)
            where TEnumerator : IEnumerator<TSource>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (selector == null) throw Error.ArgumentNull(nameof(selector));
            using (var e = new SelectEnumerator<TSource, float?, TEnumerator, FuncFunctor<TSource, float?>>(source.GetEnumerator(), new FuncFunctor<TSource, float?>(selector)))
                return Min_NullableOfSingle(e);
        }

        private static float Min_Single<TEnumerator>(TEnumerator source)
            where TEnumerator : IEnumerator<float>
        {
            if (!source.MoveNext()) throw Error.NoElements();
            float min = source.Current;
            while (source.MoveNext())
            {
                var element = source.Current;
                if (!(min <= element))
                    min = element;
            }
            return min;
        }

        private static float? Min_NullableOfSingle<TEnumerator>(TEnumerator source)
            where TEnumerator : IEnumerator<float?>
        {
            while (source.MoveNext())
            {
                var x = source.Current;
                if (x == null)
                    continue;

                float min = x.GetValueOrDefault();
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
        public static float Sum<TEnumerator>(this IEnumerable<float, TEnumerator> source)
            where TEnumerator : IEnumerator<float>
        {
            using (var e = (source ?? throw Error.ArgumentNull(nameof(source))).GetEnumerator())
                return Sum_Single(e);
        }

        public static float Sum<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, float> selector)
            where TEnumerator : IEnumerator<TSource>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (selector == null) throw Error.ArgumentNull(nameof(selector));
            using (var e = new SelectEnumerator<TSource, float, TEnumerator, FuncFunctor<TSource, float>>(source.GetEnumerator(), new FuncFunctor<TSource, float>(selector)))
                return Sum_Single(e);
        }

        public static float? Sum<TEnumerator>(this IEnumerable<float?, TEnumerator> source)
            where TEnumerator : IEnumerator<float?>
        {
            using (var e = (source ?? throw Error.ArgumentNull(nameof(source))).GetEnumerator())
                return Sum_NullableOfSingle(e);
        }

        public static float? Sum<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, float?> selector)
            where TEnumerator : IEnumerator<TSource>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (selector == null) throw Error.ArgumentNull(nameof(selector));
            using (var e = new SelectEnumerator<TSource, float?, TEnumerator, FuncFunctor<TSource, float?>>(source.GetEnumerator(), new FuncFunctor<TSource, float?>(selector)))
                return Sum_NullableOfSingle(e);
        }

        private static float Sum_Single<TEnumerator>(TEnumerator source)
            where TEnumerator : IEnumerator<float>
        {
            if (!source.MoveNext()) throw Error.NoElements();
            double sum = source.Current;
            checked
            {
                while (source.MoveNext())
                    sum += source.Current;
            }
            return (float)sum;
        }

        private static float? Sum_NullableOfSingle<TEnumerator>(TEnumerator source)
            where TEnumerator : IEnumerator<float?>
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
                    return (float)sum;
                }
            }
            return null;
        }
        #endregion
    }
}
