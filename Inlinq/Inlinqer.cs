using System;
using System.Collections;
using System.Collections.Generic;
using Inlinq.Impl;
using Inlinq.Cmp;
using Inlinq.Sort;

namespace Inlinq
{
    public static partial class Inlinqer
    {
        #region Aggregate
        public static TSource Aggregate<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TSource, TSource> func)
            where TEnumerator : IEnumerator<TSource>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (func == null) throw Error.ArgumentNull(nameof(func));
            using (var e = source.GetEnumerator())
            {
                if (e.MoveNext())
                {
                    var result = e.Current;
                    while (e.MoveNext())
                        result = func(result, e.Current);
                    return result;
                }
                throw Error.NoElements();
            }
        }

        public static TAccumulate Aggregate<TSource, TAccumulate, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func)
            where TEnumerator : IEnumerator<TSource>
            => AggregateImpl(source ?? throw Error.ArgumentNull(nameof(source)), seed, func ?? throw Error.ArgumentNull(nameof(func)));

        public static TResult Aggregate<TSource, TEnumerator, TAccumulate, TResult>(this IEnumerable<TSource, TEnumerator> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func, Func<TAccumulate, TResult> resultSelector)
            where TEnumerator : IEnumerator<TSource>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (func == null) throw Error.ArgumentNull(nameof(func));
            if (resultSelector == null) throw Error.ArgumentNull(nameof(resultSelector));
            return resultSelector(AggregateImpl(source, seed, func));
        }

        private static TAccumulate AggregateImpl<TSource, TAccumulate, TEnumerator>(IEnumerable<TSource, TEnumerator> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func)
            where TEnumerator : IEnumerator<TSource>
        {
            using (var e = source.GetEnumerator())
                while (e.MoveNext())
                    seed = func(seed, e.Current);
            return seed;
        }
        #endregion

        #region All
        public static bool All<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, bool> predicate)
            where TEnumerator : IEnumerator<TSource>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (predicate == null) throw Error.ArgumentNull(nameof(predicate));
            using (var e = source.GetEnumerator())
                while (e.MoveNext())
                    if (!predicate(e.Current))
                        return false;
            return true;
        }
        #endregion

        #region Any
        public static bool Any<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source)
            where TEnumerator : IEnumerator<TSource>
        {
            using (var e = (source ?? throw Error.ArgumentNull(nameof(source))).GetEnumerator())
                return e.MoveNext();
        }

        public static bool Any<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, bool> predicate)
            where TEnumerator : IEnumerator<TSource>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (predicate == null) throw Error.ArgumentNull(nameof(predicate));
            using (var e = source.GetEnumerator())
                while (e.MoveNext())
                    if (predicate(e.Current))
                        return true;
            return false;
        }
        #endregion

        #region Cast
        public static CastEnumerable<TResult> Cast<TResult>(this IEnumerable source)
            => new CastEnumerable<TResult>(source ?? throw Error.ArgumentNull(nameof(source)));
        #endregion

        #region Concat
        public static ConcatEnumerable<TSource, TEnumerator1, TEnumerator2> Concat<TSource, TEnumerator1, TEnumerator2>(this IEnumerable<TSource, TEnumerator1> first, IEnumerable<TSource, TEnumerator2> second)
            where TEnumerator1 : IEnumerator<TSource>
            where TEnumerator2 : IEnumerator<TSource>
            => ConcatImpl(first ?? throw Error.ArgumentNull(nameof(first)), second ?? throw Error.ArgumentNull(nameof(second)));

        private static ConcatEnumerable<TSource, TEnumerator1, TEnumerator2> ConcatImpl<TSource, TEnumerator1, TEnumerator2>(IEnumerable<TSource, TEnumerator1> first, IEnumerable<TSource, TEnumerator2> second)
            where TEnumerator1 : IEnumerator<TSource>
            where TEnumerator2 : IEnumerator<TSource>
            => new ConcatEnumerable<TSource, TEnumerator1, TEnumerator2>(first, second);
        #endregion

        #region Count
        public static int Count<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source)
            where TEnumerator : IEnumerator<TSource>
        {
            if ((source ?? throw Error.ArgumentNull(nameof(source))).GetCount(out int count))
                return count;

            using (var e = source.GetEnumerator())
            {
                count = 0;
                checked
                {
                    while (e.MoveNext())
                        ++count;
                }
                return count;
            }
        }
        
        public static int Count<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, bool> predicate)
            where TEnumerator : IEnumerator<TSource>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (predicate == null) throw Error.ArgumentNull(nameof(predicate));
            int count = 0;
            using (var e = source.GetEnumerator())
                while (e.MoveNext())
                    if (predicate(e.Current))
                        count = checked(count + 1);
            return count;
        }
        #endregion

        #region DefaultIfEmpty
        public static DefaultIfEmptyEnumerable<TSource, TEnumerator> DefaultIfEmpty<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source)
            where TEnumerator : IEnumerator<TSource>
            => DefaultIfEmpty(source, default(TSource));

        public static DefaultIfEmptyEnumerable<TSource, TEnumerator> DefaultIfEmpty<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, TSource defaultValue)
            where TEnumerator : IEnumerator<TSource>
            => new DefaultIfEmptyEnumerable<TSource, TEnumerator>(source ?? throw Error.ArgumentNull(nameof(source)), defaultValue);
        #endregion

        #region ElementAt
        public static TSource ElementAt<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, int index)
            where TEnumerator : IEnumerator<TSource>
            => ElementAtImpl(source ?? throw Error.ArgumentNull(nameof(source)), index >= 0 ? index : throw Error.ArgumentOutOfRange(nameof(index)), out var element) ? element : throw Error.ArgumentOutOfRange(nameof(index));
        #endregion

        #region ElementAtOrDefault
        public static TSource ElementAtOrDefault<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, int index)
            where TEnumerator : IEnumerator<TSource>
            => ElementAtImpl(source ?? throw Error.ArgumentNull(nameof(source)), index >= 0 ? index : throw Error.ArgumentOutOfRange(nameof(index)), out var element) ? element : default(TSource);

        private static bool ElementAtImpl<TSource, TEnumerator>(IEnumerable<TSource, TEnumerator> source, int index, out TSource element)
            where TEnumerator : IEnumerator<TSource>
        {
            if (!source.GetCount(out int count) || count > index)
            {
                if (source is IList<TSource> list)
                {
                    element = list[index];
                    return true;
                }

                using (var e = source.GetEnumerator())
                {
                    for (; index >= 0; --index)
                        if (!e.MoveNext())
                            break;
                    element = e.Current;
                    return true;
                }
            }
            
            element = default(TSource);
            return false;
        }
        #endregion

        #region Empty
        public static EmptyEnumerable<TSource> Empty<TSource>()
            => EmptyEnumerable<TSource>.Instance;
        #endregion

        #region First
        public static TSource First<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source)
            where TEnumerator : IEnumerator<TSource>
            => FirstImpl(source ?? throw Error.ArgumentNull(nameof(source)), out var first) ? first : throw Error.NoElements();

        public static TSource First<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, bool> predicate)
            where TEnumerator : IEnumerator<TSource>
            => FirstImpl(source ?? throw Error.ArgumentNull(nameof(source)), predicate ?? throw Error.ArgumentNull(nameof(predicate)), out var first) ? first : throw Error.NoMatches();
        #endregion

        #region FirstOrDefault
        public static TSource FirstOrDefault<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source)
            where TEnumerator : IEnumerator<TSource>
            => FirstImpl(source ?? throw Error.ArgumentNull(nameof(source)), out var first) ? first : default(TSource);

        public static TSource FirstOrDefault<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, bool> predicate)
            where TEnumerator : IEnumerator<TSource>
            => FirstImpl(source ?? throw Error.ArgumentNull(nameof(source)), predicate ?? throw Error.ArgumentNull(nameof(predicate)), out var first) ? first : default(TSource);

        private static bool FirstImpl<TSource, TEnumerator>(IEnumerable<TSource, TEnumerator> source, out TSource first)
            where TEnumerator : IEnumerator<TSource>
        {
            using (var e = source.GetEnumerator())
            {
                if (e.MoveNext())
                {
                    first = e.Current;
                    return true;
                }
                else
                {
                    first = default(TSource);
                    return false;
                }
            }
        }

        private static bool FirstImpl<TSource, TEnumerator>(IEnumerable<TSource, TEnumerator> source, Func<TSource, bool> predicate, out TSource first)
            where TEnumerator : IEnumerator<TSource>
        {
            foreach (var element in source)
                if (predicate(element))
                {
                    first = element;
                    return true;
                }
            first = default(TSource);
            return false;
        }
        #endregion

        #region Join
        public static JoinEnumerable<TOuter, TOuterEnumerator, TInner, TInnerEnumerator, TKey, TResult, EquatableEqualityComparer<TKey>> Join<TOuter, TOuterEnumerator, TInner, TInnerEnumerator, TKey, TResult>(this IEnumerable<TOuter, TOuterEnumerator> outer, IEnumerable<TInner, TInnerEnumerator> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector)
            where TOuterEnumerator : IEnumerator<TOuter>
            where TInnerEnumerator : IEnumerator<TInner>
            where TKey : IEquatable<TKey>
            => JoinImpl(outer ?? throw Error.ArgumentNull(nameof(outer)), inner ?? throw Error.ArgumentNull(nameof(inner)), outerKeySelector ?? throw Error.ArgumentNull(nameof(outerKeySelector)), innerKeySelector ?? throw Error.ArgumentNull(nameof(innerKeySelector)), resultSelector ?? throw Error.ArgumentNull(nameof(resultSelector)), new EquatableEqualityComparer<TKey>());

        public static JoinEnumerable<TOuter, TOuterEnumerator, TInner, TInnerEnumerator, TKey, TResult, IEqualityComparer<TKey>> Join<TOuter, TOuterEnumerator, TInner, TInnerEnumerator, TKey, TResult>(this IEnumerable<TOuter, TOuterEnumerator> outer, IEnumerable<TInner, TInnerEnumerator> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector, IEqualityComparer<TKey> comparer)
            where TOuterEnumerator : IEnumerator<TOuter>
            where TInnerEnumerator : IEnumerator<TInner>
            => JoinImpl(outer ?? throw Error.ArgumentNull(nameof(outer)), inner ?? throw Error.ArgumentNull(nameof(inner)), outerKeySelector ?? throw Error.ArgumentNull(nameof(outerKeySelector)), innerKeySelector ?? throw Error.ArgumentNull(nameof(innerKeySelector)), resultSelector ?? throw Error.ArgumentNull(nameof(resultSelector)), comparer ?? EqualityComparer<TKey>.Default);

        public static JoinEnumerable<TOuter, TOuterEnumerator, TInner, TInnerEnumerator, TKey, TResult, TEqualityComparer> Join<TOuter, TOuterEnumerator, TInner, TInnerEnumerator, TKey, TResult, TEqualityComparer>(this IEnumerable<TOuter, TOuterEnumerator> outer, IEnumerable<TInner, TInnerEnumerator> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector, TEqualityComparer comparer)
            where TOuterEnumerator : IEnumerator<TOuter>
            where TInnerEnumerator : IEnumerator<TInner>
            where TEqualityComparer : struct, IEqualityComparer<TKey>
            => JoinImpl(outer ?? throw Error.ArgumentNull(nameof(outer)), inner ?? throw Error.ArgumentNull(nameof(inner)), outerKeySelector ?? throw Error.ArgumentNull(nameof(outerKeySelector)), innerKeySelector ?? throw Error.ArgumentNull(nameof(innerKeySelector)), resultSelector ?? throw Error.ArgumentNull(nameof(resultSelector)), comparer);

        #region String key optimization
        public static JoinEnumerable<TOuter, TOuterEnumerator, TInner, TInnerEnumerator, string, TResult, Cmp.StringComparer> Join<TOuter, TOuterEnumerator, TInner, TInnerEnumerator, TResult>(this IEnumerable<TOuter, TOuterEnumerator> outer, IEnumerable<TInner, TInnerEnumerator> inner, Func<TOuter, string> outerKeySelector, Func<TInner, string> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector)
            where TOuterEnumerator : IEnumerator<TOuter>
            where TInnerEnumerator : IEnumerator<TInner>
            => JoinImpl(outer ?? throw Error.ArgumentNull(nameof(outer)), inner ?? throw Error.ArgumentNull(nameof(inner)), outerKeySelector ?? throw Error.ArgumentNull(nameof(outerKeySelector)), innerKeySelector ?? throw Error.ArgumentNull(nameof(innerKeySelector)), resultSelector ?? throw Error.ArgumentNull(nameof(resultSelector)), new Cmp.StringComparer());
        #endregion

        #region Nullable key optimization
        public static JoinEnumerable<TOuter, TOuterEnumerator, TInner, TInnerEnumerator, TKey?, TResult, NullableEqualityComparer<TKey>> Join<TOuter, TOuterEnumerator, TInner, TInnerEnumerator, TKey, TResult>(this IEnumerable<TOuter, TOuterEnumerator> outer, IEnumerable<TInner, TInnerEnumerator> inner, Func<TOuter, TKey?> outerKeySelector, Func<TInner, TKey?> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector)
            where TOuterEnumerator : IEnumerator<TOuter>
            where TInnerEnumerator : IEnumerator<TInner>
            where TKey : struct, IEquatable<TKey>
            => JoinImpl(outer ?? throw Error.ArgumentNull(nameof(outer)), inner ?? throw Error.ArgumentNull(nameof(inner)), outerKeySelector ?? throw Error.ArgumentNull(nameof(outerKeySelector)), innerKeySelector ?? throw Error.ArgumentNull(nameof(innerKeySelector)), resultSelector ?? throw Error.ArgumentNull(nameof(resultSelector)), new NullableEqualityComparer<TKey>());
        #endregion

        private static JoinEnumerable<TOuter, TOuterEnumerator, TInner, TInnerEnumerator, TKey, TResult, TEqualityComparer> JoinImpl<TOuter, TOuterEnumerator, TInner, TInnerEnumerator, TKey, TResult, TEqualityComparer>(IEnumerable<TOuter, TOuterEnumerator> outer, IEnumerable<TInner, TInnerEnumerator> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector, TEqualityComparer comparer)
            where TOuterEnumerator : IEnumerator<TOuter>
            where TInnerEnumerator : IEnumerator<TInner>
            where TEqualityComparer : IEqualityComparer<TKey>
            => new JoinEnumerable<TOuter, TOuterEnumerator, TInner, TInnerEnumerator, TKey, TResult, TEqualityComparer>(outer, inner, outerKeySelector, innerKeySelector, resultSelector, comparer);
        #endregion

        #region Last
        public static TSource Last<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source)
            where TEnumerator : IEnumerator<TSource>
            => LastImpl(source ?? throw Error.ArgumentNull(nameof(source)), out var last) ? last : throw Error.NoElements();

        public static TSource Last<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, bool> predicate)
            where TEnumerator : IEnumerator<TSource>
            => LastImpl(source ?? throw Error.ArgumentNull(nameof(source)), predicate ?? throw Error.ArgumentNull(nameof(predicate)), out var last) ? last : throw Error.NoElements();
        #endregion

        #region LastOrDefault
        public static TSource LastOrDefault<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source)
            where TEnumerator : IEnumerator<TSource>
            => LastImpl(source ?? throw Error.ArgumentNull(nameof(source)), out var last) ? last : default(TSource);

        public static TSource LastOrDefault<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, bool> predicate)
            where TEnumerator : IEnumerator<TSource>
            => LastImpl(source ?? throw Error.ArgumentNull(nameof(source)), predicate ?? throw Error.ArgumentNull(nameof(predicate)), out var last) ? last : default(TSource);

        private static bool LastImpl<TSource, TEnumerator>(IEnumerable<TSource, TEnumerator> source, out TSource last)
            where TEnumerator : IEnumerator<TSource>
        {
            using (var e = (source ?? throw Error.ArgumentNull(nameof(source))).GetEnumerator())
            {
                if (e.MoveNext())
                {
                    last = e.Current;
                    while (e.MoveNext())
                        last = e.Current;
                    return true;
                }
            }
            last = default(TSource);
            return false;
        }

        private static bool LastImpl<TSource, TEnumerator>(IEnumerable<TSource, TEnumerator> source, Func<TSource, bool> predicate, out TSource last)
            where TEnumerator : IEnumerator<TSource>
        {
            using (var e = source.GetEnumerator())
            {
                while (e.MoveNext())
                {
                    last = e.Current;
                    if (predicate(last))
                    {
                        while (e.MoveNext())
                        {
                            var element = e.Current;
                            if (predicate(last))
                                last = element;
                        }
                        return true;
                    }
                }
            }
            last = default(TSource);
            return false;
        }
        #endregion

        #region LongCount
        public static long LongCount<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source)
            where TEnumerator : IEnumerator<TSource>
        {
            if (source.GetCount(out int c))
                return c;

            using (var e = (source ?? throw Error.ArgumentNull(nameof(source))).GetEnumerator())
            {
                long count = 0;
                checked
                {
                    while (e.MoveNext())
                        ++count;
                }
                return count;
            }
        }

        public static long LongCount<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, bool> predicate)
            where TEnumerator : IEnumerator<TSource>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (predicate == null) throw Error.ArgumentNull(nameof(predicate));
            long count = 0;
            using (var e = source.GetEnumerator())
                while (e.MoveNext())
                    if (predicate(e.Current))
                        count = checked(count + 1);
            return count;
        }
        #endregion

        #region Max
        public static TSource Max<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source)
            where TSource : IComparable<TSource>
            where TEnumerator : IEnumerator<TSource>
            => MaxImpl(source ?? throw Error.ArgumentNull(nameof(source)), new ComparableComparer<TSource>());

        public static TSource Max<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, IComparer<TSource> comparer)
            where TEnumerator : IEnumerator<TSource>
            => MaxImpl(source ?? throw Error.ArgumentNull(nameof(source)), comparer ?? Comparer<TSource>.Default);

        public static TSource Max<TSource, TEnumerator, TComparer>(this IEnumerable<TSource, TEnumerator> source, TComparer comparer)
            where TEnumerator : IEnumerator<TSource>
            where TComparer : struct, IComparer<TSource>
            => MaxImpl(source ?? throw Error.ArgumentNull(nameof(source)), comparer);

        public static TResult Max<TSource, TResult, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TResult> selector)
            where TResult : IComparable<TResult>
            where TEnumerator : IEnumerator<TSource>
            => MaxImpl(source ?? throw Error.ArgumentNull(nameof(source)), selector ?? throw Error.ArgumentNull(nameof(selector)), new ComparableComparer<TResult>());

        public static TResult Max<TSource, TResult, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TResult> selector, IComparer<TResult> comparer)
            where TEnumerator : IEnumerator<TSource>
            => MaxImpl(source ?? throw Error.ArgumentNull(nameof(source)), selector ?? throw Error.ArgumentNull(nameof(selector)), comparer ?? Comparer<TResult>.Default);

        public static TResult Max<TSource, TResult, TEnumerator, TComparer>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TResult> selector, TComparer comparer)
            where TEnumerator : IEnumerator<TSource>
            where TComparer : struct, IComparer<TResult>
            => MaxImpl(source ?? throw Error.ArgumentNull(nameof(source)), selector ?? throw Error.ArgumentNull(nameof(selector)), comparer);

        #region Nullable optimization
        public static TSource? Max<TSource, TEnumerator>(this IEnumerable<TSource?, TEnumerator> source)
            where TSource : struct, IComparable<TSource>
            where TEnumerator : IEnumerator<TSource?>
            => MaxImpl(source ?? throw Error.ArgumentNull(nameof(source)), new NullableComparer<TSource>());

        public static TResult? Max<TSource, TResult, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TResult?> selector)
            where TResult : struct, IComparable<TResult>
            where TEnumerator : IEnumerator<TSource>
            => MaxImpl(source ?? throw Error.ArgumentNull(nameof(source)), selector ?? throw Error.ArgumentNull(nameof(selector)), new NullableComparer<TResult>());
        #endregion

        private static TSource MaxImpl<TSource, TEnumerator, TComparer>(IEnumerable<TSource, TEnumerator> source, TComparer comparer)
            where TEnumerator : IEnumerator<TSource>
            where TComparer : IComparer<TSource>
        {
            using (var e = source.GetEnumerator())
                return MaxImpl<TSource, TEnumerator, TComparer>(e, comparer);
        }

        private static TResult MaxImpl<TSource, TResult, TEnumerator, TComparer>(IEnumerable<TSource, TEnumerator> source, Func<TSource, TResult> selector, TComparer comparer)
            where TEnumerator : IEnumerator<TSource>
            where TComparer : IComparer<TResult>
        {
            using (var e = new SelectEnumerator<TSource, TResult, TEnumerator>(source.GetEnumerator(), selector))
                return MaxImpl<TResult, SelectEnumerator<TSource, TResult, TEnumerator>, TComparer>(e, comparer);
        }

        private static TSource MaxImpl<TSource, TEnumerator, TComparer>(TEnumerator source, TComparer comparer)
            where TEnumerator : IEnumerator<TSource>
            where TComparer : IComparer<TSource>
        {
            var defaultValue = default(TSource);
            if (source.MoveNext())
            {
                var max = source.Current;
                if (defaultValue == null)
                {
                    while (max == null)
                    {
                        if (!source.MoveNext())
                            return defaultValue;
                        max = source.Current;
                    }
                    while (source.MoveNext())
                    {
                        var x = source.Current;
                        if (x != null && comparer.Compare(x, max) > 0)
                            max = x;
                    }
                }
                else
                {
                    while (source.MoveNext())
                    {
                        var x = source.Current;
                        if (comparer.Compare(x, max) > 0)
                            max = x;
                    }
                }
                return max;
            }
            return defaultValue == null ? defaultValue : throw Error.NoElements();
        }
        #endregion

        #region Min
        public static TSource Min<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source)
            where TSource : IComparable<TSource>
            where TEnumerator : IEnumerator<TSource>
            => MinImpl(source ?? throw Error.ArgumentNull(nameof(source)), new ComparableComparer<TSource>());

        public static TSource Min<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, IComparer<TSource> comparer)
            where TEnumerator : IEnumerator<TSource>
            => MinImpl(source ?? throw Error.ArgumentNull(nameof(source)), comparer ?? Comparer<TSource>.Default);

        public static TSource Min<TSource, TEnumerator, TComparer>(this IEnumerable<TSource, TEnumerator> source, TComparer comparer)
            where TEnumerator : IEnumerator<TSource>
            where TComparer : struct, IComparer<TSource>
            => MinImpl(source ?? throw Error.ArgumentNull(nameof(source)), comparer);

        public static TResult Min<TSource, TResult, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TResult> selector)
            where TResult : IComparable<TResult>
            where TEnumerator : IEnumerator<TSource>
            => MinImpl(source ?? throw Error.ArgumentNull(nameof(source)), selector ?? throw Error.ArgumentNull(nameof(selector)), new ComparableComparer<TResult>());

        public static TResult Min<TSource, TResult, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TResult> selector, IComparer<TResult> comparer)
            where TEnumerator : IEnumerator<TSource>
            => MinImpl(source ?? throw Error.ArgumentNull(nameof(source)), selector ?? throw Error.ArgumentNull(nameof(selector)), comparer ?? Comparer<TResult>.Default);

        public static TResult Min<TSource, TResult, TEnumerator, TComparer>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TResult> selector, TComparer comparer)
            where TEnumerator : IEnumerator<TSource>
            where TComparer : struct, IComparer<TResult>
            => MinImpl(source ?? throw Error.ArgumentNull(nameof(source)), selector ?? throw Error.ArgumentNull(nameof(selector)), comparer);

        #region Nullable optimization
        public static TSource? Min<TSource, TEnumerator>(this IEnumerable<TSource?, TEnumerator> source)
            where TSource : struct, IComparable<TSource>
            where TEnumerator : IEnumerator<TSource?>
            => MinImpl(source ?? throw Error.ArgumentNull(nameof(source)), new NullableComparer<TSource>());

        public static TResult? Min<TSource, TResult, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TResult?> selector)
            where TResult : struct, IComparable<TResult>
            where TEnumerator : IEnumerator<TSource>
            => MinImpl(source ?? throw Error.ArgumentNull(nameof(source)), selector ?? throw Error.ArgumentNull(nameof(selector)), new NullableComparer<TResult>());
        #endregion

        private static TSource MinImpl<TSource, TEnumerator, TComparer>(IEnumerable<TSource, TEnumerator> source, TComparer comparer)
            where TEnumerator : IEnumerator<TSource>
            where TComparer : IComparer<TSource>
        {
            using (var e = source.GetEnumerator())
                return MinImpl<TSource, TEnumerator, TComparer>(e, comparer);
        }

        private static TResult MinImpl<TSource, TResult, TEnumerator, TComparer>(IEnumerable<TSource, TEnumerator> source, Func<TSource, TResult> selector, TComparer comparer)
            where TEnumerator : IEnumerator<TSource>
            where TComparer : IComparer<TResult>
        {
            using (var e = new SelectEnumerator<TSource, TResult, TEnumerator>(source.GetEnumerator(), selector))
                return MinImpl<TResult, SelectEnumerator<TSource, TResult, TEnumerator>, TComparer>(e, comparer);
        }

        private static TSource MinImpl<TSource, TEnumerator, TComparer>(TEnumerator source, TComparer comparer)
            where TEnumerator : IEnumerator<TSource>
            where TComparer : IComparer<TSource>
        {
            var defaultValue = default(TSource);
            if (source.MoveNext())
            {
                var min = source.Current;
                if (defaultValue == null)
                {
                    while (min == null)
                    {
                        if (!source.MoveNext())
                            return defaultValue;
                        min = source.Current;
                    }
                    while (source.MoveNext())
                    {
                        var x = source.Current;
                        if (x != null && comparer.Compare(x, min) < 0)
                            min = x;
                    }
                }
                else
                {
                    while (source.MoveNext())
                    {
                        var x = source.Current;
                        if (comparer.Compare(x, min) < 0)
                            min = x;
                    }
                }
                return min;
            }
                return defaultValue == null ? defaultValue : throw Error.NoElements();
        }
        #endregion

        #region OfType
        public static OfTypeEnumerable<TResult> OfType<TResult>(this IEnumerable source)
            => new OfTypeEnumerable<TResult>(source ?? throw Error.ArgumentNull(nameof(source)));
        #endregion

        #region OrderBy
        public static OrderedEnumerable<TSource, TEnumerator> OrderBy<TSource, TEnumerator, TKey>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey> keySelector)
            where TEnumerator : IEnumerator<TSource>
            where TKey : IComparable<TKey>
            => OrderBy(source, keySelector, new ComparableComparer<TKey>());

        public static OrderedEnumerable<TSource, TEnumerator> OrderBy<TSource, TEnumerator, TKey, TComparer>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey> keySelector, TComparer comparer)
            where TEnumerator : IEnumerator<TSource>
            where TComparer : struct, IComparer<TKey>
            => OrderByImpl(source ?? throw Error.ArgumentNull(nameof(source)), keySelector ?? throw Error.ArgumentNull(nameof(keySelector)), comparer);

        public static OrderedEnumerable<TSource, TEnumerator> OrderBy<TSource, TEnumerator, TKey>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
            where TEnumerator : IEnumerator<TSource>
            => OrderByImpl(source ?? throw Error.ArgumentNull(nameof(source)), keySelector ?? throw Error.ArgumentNull(nameof(keySelector)), comparer ?? Comparer<TKey>.Default);

        #region String key optimization
        public static OrderedEnumerable<TSource, TEnumerator> OrderBy<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, string> keySelector)
            where TEnumerator : IEnumerator<TSource>
            => OrderBy(source, keySelector, new Cmp.StringComparer());
        #endregion

        #region Nullable key optimization
        public static OrderedEnumerable<TSource, TEnumerator> OrderBy<TSource, TEnumerator, TKey>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey?> keySelector)
            where TEnumerator : IEnumerator<TSource>
            where TKey : struct, IComparable<TKey>
            => OrderBy(source, keySelector, new NullableComparer<TKey>());
        #endregion

        private static OrderedEnumerable<TSource, TEnumerator> OrderByImpl<TSource, TEnumerator, TKey, TComparer>(IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey> keySelector, TComparer comparer)
            where TEnumerator : IEnumerator<TSource>
            where TComparer : IComparer<TKey>
            => new OrderedEnumerable<TSource, TEnumerator>(source, new PrimaryTerminalSort<TSource, TEnumerator, TKey, TComparer>(keySelector, comparer));
        #endregion

        #region OrderByDescending
        public static OrderedEnumerable<TSource, TEnumerator> OrderByDescending<TSource, TEnumerator, TKey>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey> keySelector)
            where TEnumerator : IEnumerator<TSource>
            where TKey : IComparable<TKey>
            => OrderByDescending(source, keySelector, new ComparableComparer<TKey>());

        public static OrderedEnumerable<TSource, TEnumerator> OrderByDescending<TSource, TEnumerator, TKey, TComparer>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey> keySelector, TComparer comparer)
            where TEnumerator : IEnumerator<TSource>
            where TComparer : struct, IComparer<TKey>
            => OrderByDescendingImpl(source ?? throw Error.ArgumentNull(nameof(source)), keySelector ?? throw Error.ArgumentNull(nameof(keySelector)), comparer);

        public static OrderedEnumerable<TSource, TEnumerator> OrderByDescending<TSource, TEnumerator, TKey>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
            where TEnumerator : IEnumerator<TSource>
            => OrderByDescendingImpl(source ?? throw Error.ArgumentNull(nameof(source)), keySelector ?? throw Error.ArgumentNull(nameof(keySelector)), comparer ?? Comparer<TKey>.Default);

        #region String key optimization
        public static OrderedEnumerable<TSource, TEnumerator> OrderByDescending<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, string> keySelector)
            where TEnumerator : IEnumerator<TSource>
            => OrderByDescending(source, keySelector, new Cmp.StringComparer());
        #endregion

        #region Nullable key optimization
        public static OrderedEnumerable<TSource, TEnumerator> OrderByDescending<TSource, TEnumerator, TKey>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey?> keySelector)
            where TEnumerator : IEnumerator<TSource>
            where TKey : struct, IComparable<TKey>
            => OrderByDescending(source, keySelector, new NullableComparer<TKey>());
        #endregion

        private static OrderedEnumerable<TSource, TEnumerator> OrderByDescendingImpl<TSource, TEnumerator, TKey, TComparer>(IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey> keySelector, TComparer comparer)
            where TEnumerator : IEnumerator<TSource>
            where TComparer : IComparer<TKey>
            => OrderByImpl(source, keySelector, new ReverseComparer<TKey, TComparer>(comparer));
        #endregion

        #region Range
        public static Range32Enumerable Range(int start, int count)
            => new Range32Enumerable(start, start + count >= start ? count : throw Error.ArgumentOutOfRange(nameof(count)));

        public static Range64Enumerable Range(long start, long count)
            => new Range64Enumerable(start, start + count >= start ? count : throw Error.ArgumentOutOfRange(nameof(count)));
        #endregion

        #region Repeat
        public static Repeat32Enumerable<TResult> Repeat<TResult>(TResult element, int count)
            => new Repeat32Enumerable<TResult>(element, count >= 0 ? count : throw Error.ArgumentOutOfRange(nameof(count)));

        public static Repeat64Enumerable<TResult> Repeat<TResult>(TResult element, long count)
            => new Repeat64Enumerable<TResult>(element, count >= 0 ? count : throw Error.ArgumentOutOfRange(nameof(count)));
        #endregion

        #region Select
        public static SelectEnumerable<TSource, TResult, TEnumerator> Select<TSource, TResult, TEnumerator>(
            this IEnumerable<TSource, TEnumerator> source, Func<TSource, TResult> selector)
            where TEnumerator : IEnumerator<TSource>
            => SelectImpl(source ?? throw Error.ArgumentNull(nameof(source)), selector ?? throw Error.ArgumentNull(nameof(selector)));

        private static SelectEnumerable<TSource, TResult, TEnumerator> SelectImpl<TSource, TResult, TEnumerator>(IEnumerable<TSource, TEnumerator> source, Func<TSource, TResult> selector)
            where TEnumerator : IEnumerator<TSource>
            => new SelectEnumerable<TSource, TResult, TEnumerator>(source, selector);
        #endregion

        #region SelectMany
        public static SelectManyEnumerableA<TSource, TResult, TEnumerator1, TEnumerator2> SelectMany<TSource, TResult, TEnumerator1, TEnumerator2>(this IEnumerable<TSource, TEnumerator1> source, Func<TSource, IEnumerable<TResult, TEnumerator2>> selector)
            where TEnumerator1 : IEnumerator<TSource>
            where TEnumerator2 : IEnumerator<TResult>
            => new SelectManyEnumerableA<TSource, TResult, TEnumerator1, TEnumerator2>(source ?? throw Error.ArgumentNull(nameof(source)), selector ?? throw Error.ArgumentNull(nameof(selector)));

        public static SelectManyEnumerableB<TSource, TResult, TEnumerator1, TEnumerator2> SelectMany<TSource, TResult, TEnumerator1, TEnumerator2>(this IEnumerable<TSource, TEnumerator1> source, Func<TSource, int, IEnumerable<TResult, TEnumerator2>> selector)
            where TEnumerator1 : IEnumerator<TSource>
            where TEnumerator2 : IEnumerator<TResult>
            => new SelectManyEnumerableB<TSource, TResult, TEnumerator1, TEnumerator2>(source ?? throw Error.ArgumentNull(nameof(source)), selector ?? throw Error.ArgumentNull(nameof(selector)));

        public static SelectManyEnumerableC<TSource, TResult, TEnumerator> SelectMany<TSource, TResult, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, IEnumerable<TResult>> selector)
            where TEnumerator : IEnumerator<TSource>
            => new SelectManyEnumerableC<TSource, TResult, TEnumerator>(source ?? throw Error.ArgumentNull(nameof(source)), selector ?? throw Error.ArgumentNull(nameof(selector)));

        public static SelectManyEnumerableD<TSource, TResult, TEnumerator> SelectMany<TSource, TResult, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, int, IEnumerable<TResult>> selector)
            where TEnumerator : IEnumerator<TSource>
            => new SelectManyEnumerableD<TSource, TResult, TEnumerator>(source ?? throw Error.ArgumentNull(nameof(source)), selector ?? throw Error.ArgumentNull(nameof(selector)));

        public static SelectManyEnumerableE<TSource, TCollection, TResult, TEnumerator1, TEnumerator2> SelectMany<TSource, TCollection, TResult, TEnumerator1, TEnumerator2>(this IEnumerable<TSource, TEnumerator1> source, Func<TSource, IEnumerable<TCollection, TEnumerator2>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
            where TEnumerator1 : IEnumerator<TSource>
            where TEnumerator2 : IEnumerator<TCollection>
            => new SelectManyEnumerableE<TSource, TCollection, TResult, TEnumerator1, TEnumerator2>(source ?? throw Error.ArgumentNull(nameof(source)), collectionSelector ?? throw Error.ArgumentNull(nameof(collectionSelector)), resultSelector ?? throw Error.ArgumentNull(nameof(resultSelector)));

        public static SelectManyEnumerableF<TSource, TCollection, TResult, TEnumerator1, TEnumerator2> SelectMany<TSource, TCollection, TResult, TEnumerator1, TEnumerator2>(this IEnumerable<TSource, TEnumerator1> source, Func<TSource, int, IEnumerable<TCollection, TEnumerator2>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
            where TEnumerator1 : IEnumerator<TSource>
            where TEnumerator2 : IEnumerator<TCollection>
            => new SelectManyEnumerableF<TSource, TCollection, TResult, TEnumerator1, TEnumerator2>(source ?? throw Error.ArgumentNull(nameof(source)), collectionSelector ?? throw Error.ArgumentNull(nameof(collectionSelector)), resultSelector ?? throw Error.ArgumentNull(nameof(resultSelector)));

        public static SelectManyEnumerableG<TSource, TCollection, TResult, TEnumerator> SelectMany<TSource, TCollection, TResult, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, IEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
            where TEnumerator : IEnumerator<TSource>
            => new SelectManyEnumerableG<TSource, TCollection, TResult, TEnumerator>(source ?? throw Error.ArgumentNull(nameof(source)), collectionSelector ?? throw Error.ArgumentNull(nameof(collectionSelector)), resultSelector ?? throw Error.ArgumentNull(nameof(resultSelector)));

        public static SelectManyEnumerableH<TSource, TCollection, TResult, TEnumerator> SelectMany<TSource, TCollection, TResult, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, int, IEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
            where TEnumerator : IEnumerator<TSource>
            => new SelectManyEnumerableH<TSource, TCollection, TResult, TEnumerator>(source ?? throw Error.ArgumentNull(nameof(source)), collectionSelector ?? throw Error.ArgumentNull(nameof(collectionSelector)), resultSelector ?? throw Error.ArgumentNull(nameof(resultSelector)));
        #endregion

        #region SequenceEqual
        public static bool SequenceEqual<TSource, TEnumerator1, TEnumerator2>(this IEnumerable<TSource, TEnumerator1> first, IEnumerable<TSource, TEnumerator2> second)
            where TSource : IEquatable<TSource>
            where TEnumerator1 : IEnumerator<TSource>
            where TEnumerator2 : IEnumerator<TSource>
            => SequenceEqual(first, second, new EquatableEqualityComparer<TSource>());

        public static bool SequenceEqual<TSource, TEnumerator1, TEnumerator2>(this IEnumerable<TSource, TEnumerator1> first, IEnumerable<TSource, TEnumerator2> second, IEqualityComparer<TSource> comparer)
            where TEnumerator1 : IEnumerator<TSource>
            where TEnumerator2 : IEnumerator<TSource>
            => SequenceEqualImpl(first ?? throw Error.ArgumentNull(nameof(first)), second ?? throw Error.ArgumentNull(nameof(second)), comparer ?? EqualityComparer<TSource>.Default);

        public static bool SequenceEqual<TSource, TEnumerator1, TEnumerator2, TEqualityComparer>(this IEnumerable<TSource, TEnumerator1> first, IEnumerable<TSource, TEnumerator2> second, TEqualityComparer comparer)
            where TEnumerator1 : IEnumerator<TSource>
            where TEnumerator2 : IEnumerator<TSource>
            where TEqualityComparer : struct, IEqualityComparer<TSource>
            => SequenceEqualImpl(first ?? throw Error.ArgumentNull(nameof(first)), second ?? throw Error.ArgumentNull(nameof(second)), comparer);

        public static bool SequenceEqual<TEnumerator1, TEnumerator2>(this IEnumerable<string, TEnumerator1> first, IEnumerable<string, TEnumerator2> second)
            where TEnumerator1 : IEnumerator<string>
            where TEnumerator2 : IEnumerator<string>
            => SequenceEqual(first, second, new Cmp.StringComparer());

        public static bool SequenceEqual<TSource, TEnumerator1, TEnumerator2>(this IEnumerable<TSource?, TEnumerator1> first, IEnumerable<TSource?, TEnumerator2> second)
            where TSource : struct, IEquatable<TSource>
            where TEnumerator1 : IEnumerator<TSource?>
            where TEnumerator2 : IEnumerator<TSource?>
            => SequenceEqual(first, second, new NullableEqualityComparer<TSource>());

        private static bool SequenceEqualImpl<TSource, TEnumerator1, TEnumerator2, TEqualityComparer>(IEnumerable<TSource, TEnumerator1> first, IEnumerable<TSource, TEnumerator2> second, TEqualityComparer comparer)
            where TEnumerator1 : IEnumerator<TSource>
            where TEnumerator2 : IEnumerator<TSource>
            where TEqualityComparer : IEqualityComparer<TSource>
        {
            if (first.GetCount(out int firstCount) && second.GetCount(out int secondCount) && firstCount != secondCount)
                return false;

            using (var e1 = first.GetEnumerator())
            using (var e2 = second.GetEnumerator())
            {
                while (e1.MoveNext())
                    if (!e2.MoveNext() || !comparer.Equals(e1.Current, e2.Current))
                        return false;
                if (e2.MoveNext())
                    return false;
            }
            return true;
        }
        #endregion

        #region Single
        public static TSource Single<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source)
            where TEnumerator : IEnumerator<TSource>
            => SingleImpl(source ?? throw Error.ArgumentNull(nameof(source)), out var single, out var status) ? single : throw (status == SingleStatus.None ? Error.NoElements() : Error.MoreThanOneElement());

        public static TSource Single<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, bool> predicate)
            where TEnumerator : IEnumerator<TSource>
            => SingleImpl(source ?? throw Error.ArgumentNull(nameof(source)), predicate ?? throw Error.ArgumentNull(nameof(predicate)), out var single, out var status) ? single : throw (status == SingleStatus.None ? Error.NoMatches() : Error.MoreThanOneMatch());
        #endregion

        #region SingleOrDefault
        public static TSource SingleOrDefault<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source)
            where TEnumerator : IEnumerator<TSource>
            => SingleImpl(source ?? throw Error.ArgumentNull(nameof(source)), out var single, out var status) ? single : status == SingleStatus.None ? default(TSource) : throw Error.MoreThanOneMatch();

        public static TSource SingleOrDefault<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, bool> predicate)
            where TEnumerator : IEnumerator<TSource>
            => SingleImpl(source ?? throw Error.ArgumentNull(nameof(source)), predicate ?? throw Error.ArgumentNull(nameof(predicate)), out var single, out var status) ? single : status == SingleStatus.None ? default(TSource) : throw Error.MoreThanOneMatch();

        private enum SingleStatus { None, Single, Multiple }

        private static bool SingleImpl<TSource, TEnumerator>(IEnumerable<TSource, TEnumerator> source, out TSource single, out SingleStatus status)
            where TEnumerator : IEnumerator<TSource>
        {
            using (var e = source.GetEnumerator())
            {
                if (e.MoveNext())
                {
                    single = e.Current;
                    if (!e.MoveNext())
                    {
                        status = SingleStatus.Single;
                        return true;
                    }
                    status = SingleStatus.Multiple;
                    return false;
                }
            }
            single = default(TSource);
            status = SingleStatus.None;
            return false;
        }

        private static bool SingleImpl<TSource, TEnumerator>(IEnumerable<TSource, TEnumerator> source, Func<TSource, bool> predicate, out TSource single, out SingleStatus status)
            where TEnumerator : IEnumerator<TSource>
        {
            using (var e = source.GetEnumerator())
            {
                while (e.MoveNext())
                {
                    single = e.Current;
                    if (predicate(single))
                    {
                        while (e.MoveNext())
                            if (predicate(e.Current))
                            {
                                single = default(TSource);
                                status = SingleStatus.Multiple;
                                return false;
                            }
                        status = SingleStatus.Single;
                        return true;
                    }
                }
            }
            single = default(TSource);
            status = SingleStatus.None;
            return false;
        }
        #endregion

        #region Skip
        public static SkipEnumerable<TSource, TEnumerator> Skip<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, int count)
            where TEnumerator : IEnumerator<TSource>
            => new SkipEnumerable<TSource, TEnumerator>(source ?? throw Error.ArgumentNull(nameof(source)), count >= 0 ? count : throw Error.ArgumentOutOfRange(nameof(count)));
        #endregion

        #region SkipWhile
        public static SkipWhileEnumerableA<TSource, TEnumerator> SkipWhile<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, bool> predicate)
            where TEnumerator : IEnumerator<TSource>
            => new SkipWhileEnumerableA<TSource, TEnumerator>(source ?? throw Error.ArgumentNull(nameof(source)), predicate ?? throw Error.ArgumentNull(nameof(predicate)));

        public static SkipWhileEnumerableB<TSource, TEnumerator> SkipWhile<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, int, bool> predicate)
            where TEnumerator : IEnumerator<TSource>
            => new SkipWhileEnumerableB<TSource, TEnumerator>(source ?? throw Error.ArgumentNull(nameof(source)), predicate ?? throw Error.ArgumentNull(nameof(predicate)));
        #endregion

        #region Take
        public static TakeEnumerable<TSource, TEnumerator> Take<TSource, TEnumerable, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, int count)
            where TEnumerator : IEnumerator<TSource>
            => new TakeEnumerable<TSource, TEnumerator>(source ?? throw Error.ArgumentNull(nameof(source)), count >= 0 ? count : throw Error.ArgumentOutOfRange(nameof(count)));
        #endregion

        #region TakeWhile
        public static TakeWhileEnumerableA<TSource, TEnumerator> TakeWhile<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, bool> predicate)
            where TEnumerator : IEnumerator<TSource>
            => new TakeWhileEnumerableA<TSource, TEnumerator>(source ?? throw Error.ArgumentNull(nameof(source)), predicate ?? throw Error.ArgumentNull(nameof(predicate)));
        
        public static TakeWhileEnumerableB<TSource, TEnumerator> TakeWhile<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, int, bool> predicate)
            where TEnumerator : IEnumerator<TSource>
            => new TakeWhileEnumerableB<TSource, TEnumerator>(source ?? throw Error.ArgumentNull(nameof(source)), predicate ?? throw Error.ArgumentNull(nameof(predicate)));
        #endregion

        #region ThenBy
        public static OrderedEnumerable<TSource, TEnumerator> ThenBy<TSource, TEnumerator, TKey>(this OrderedEnumerable<TSource, TEnumerator> source, Func<TSource, TKey> keySelector)
            where TEnumerator : IEnumerator<TSource>
            where TKey : IComparable<TKey>
            => (source ?? throw Error.ArgumentNull(nameof(source))).ThenByImpl(keySelector ?? throw Error.ArgumentNull(nameof(keySelector)), new ComparableComparer<TKey>());

        public static OrderedEnumerable<TSource, TEnumerator> ThenBy<TSource, TEnumerator, TKey>(this OrderedEnumerable<TSource, TEnumerator> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
            where TEnumerator : IEnumerator<TSource>
            => (source ?? throw Error.ArgumentNull(nameof(source))).ThenByImpl(keySelector ?? throw Error.ArgumentNull(nameof(keySelector)), comparer ?? Comparer<TKey>.Default);

        public static OrderedEnumerable<TSource, TEnumerator> ThenBy<TSource, TEnumerator, TKey, TComparer>(this OrderedEnumerable<TSource, TEnumerator> source, Func<TSource, TKey> keySelector, TComparer comparer)
            where TEnumerator : IEnumerator<TSource>
            where TComparer : struct, IComparer<TKey>
            => (source ?? throw Error.ArgumentNull(nameof(source))).ThenByImpl(keySelector ?? throw Error.ArgumentNull(nameof(keySelector)), comparer);

        #region String key optimization
        public static OrderedEnumerable<TSource, TEnumerator> ThenBy<TSource, TEnumerator>(this OrderedEnumerable<TSource, TEnumerator> source, Func<TSource, string> keySelector)
            where TEnumerator : IEnumerator<TSource>
            => ThenBy(source, keySelector, new Cmp.StringComparer());
        #endregion

        #region Nullable key optimization
        public static OrderedEnumerable<TSource, TEnumerator> ThenBy<TSource, TEnumerator, TKey>(this OrderedEnumerable<TSource, TEnumerator> source, Func<TSource, TKey?> keySelector)
            where TEnumerator : IEnumerator<TSource>
            where TKey : struct, IComparable<TKey>
            => ThenBy(source, keySelector, new NullableComparer<TKey>());
        #endregion
        #endregion

        #region ThenByDescending
        public static OrderedEnumerable<TSource, TEnumerator> ThenByDescending<TSource, TEnumerator, TKey>(this OrderedEnumerable<TSource, TEnumerator> source, Func<TSource, TKey> keySelector)
            where TEnumerator : IEnumerator<TSource>
            where TKey : IComparable<TKey>
            => ThenByDescendingImpl(source ?? throw Error.ArgumentNull(nameof(source)), keySelector ?? throw Error.ArgumentNull(nameof(keySelector)), new ComparableComparer<TKey>());

        public static OrderedEnumerable<TSource, TEnumerator> ThenByDescending<TSource, TEnumerator, TKey>(this OrderedEnumerable<TSource, TEnumerator> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
            where TEnumerator : IEnumerator<TSource>
            => ThenByDescendingImpl(source ?? throw Error.ArgumentNull(nameof(source)), keySelector ?? throw Error.ArgumentNull(nameof(keySelector)), comparer ?? Comparer<TKey>.Default);

        public static OrderedEnumerable<TSource, TEnumerator> ThenByDescending<TSource, TEnumerator, TKey1, TComparer1, TKey2, TComparer2>(this OrderedEnumerable<TSource, TEnumerator> source, Func<TSource, TKey2> keySelector, TComparer2 comparer)
            where TEnumerator : IEnumerator<TSource>
            where TComparer1 : IComparer<TKey1>
            where TComparer2 : struct, IComparer<TKey2>
            => ThenByDescendingImpl(source ?? throw Error.ArgumentNull(nameof(source)), keySelector ?? throw Error.ArgumentNull(nameof(keySelector)), comparer);

        #region String key optimization
        public static OrderedEnumerable<TSource, TEnumerator> ThenByDescending<TSource, TEnumerator>(this OrderedEnumerable<TSource, TEnumerator> source, Func<TSource, string> keySelector)
            where TEnumerator : IEnumerator<TSource>
            => ThenByDescending(source, keySelector, new Cmp.StringComparer());
        #endregion

        #region Nullable key optimization
        public static OrderedEnumerable<TSource, TEnumerator> ThenByDescending<TSource, TEnumerator, TKey>(this OrderedEnumerable<TSource, TEnumerator> source, Func<TSource, TKey?> keySelector)
            where TEnumerator : IEnumerator<TSource>
            where TKey : struct, IComparable<TKey>
            => ThenByDescending(source, keySelector, new NullableComparer<TKey>());
        #endregion

        private static OrderedEnumerable<TSource, TEnumerator> ThenByDescendingImpl<TSource, TEnumerator, TKey, TComparer>(OrderedEnumerable<TSource, TEnumerator> source, Func<TSource, TKey> keySelector, TComparer comparer)
            where TEnumerator : IEnumerator<TSource>
            where TComparer : IComparer<TKey>
            => source.ThenByImpl(keySelector, new ReverseComparer<TKey, TComparer>(comparer));
        #endregion

        #region ToArray
        public static TSource[] ToArray<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source)
            where TEnumerator : IEnumerator<TSource>
            => new ArrayBuffer<TSource, TEnumerator>(source ?? throw Error.ArgumentNull(nameof(source))).ToArray();

        public static TSource[] ToArray<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, int initCapacity)
            where TEnumerator : IEnumerator<TSource>
            => new ArrayBuffer<TSource, TEnumerator>(source ?? throw Error.ArgumentNull(nameof(source)), initCapacity).ToArray();
        #endregion

        #region ToArraySegment
        public static ArraySegment<TSource> ToArraySegment<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source)
            where TEnumerator : IEnumerator<TSource>
            => new ArrayBuffer<TSource, TEnumerator>(source ?? throw Error.ArgumentNull(nameof(source))).ToArraySegment();

        public static ArraySegment<TSource> ToArraySegment<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, int initCapacity)
            where TEnumerator : IEnumerator<TSource>
            => new ArrayBuffer<TSource, TEnumerator>(source ?? throw Error.ArgumentNull(nameof(source)), initCapacity).ToArraySegment();
        #endregion

        #region ToDictionary
        public static Dictionary<TKey, TSource> ToDictionary<TSource, TEnumerator, TKey>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey> keySelector)
            where TEnumerator : IEnumerator<TSource>
            => ToDictionary(source, keySelector, null);

        public static Dictionary<TKey, TSource> ToDictionary<TSource, TEnumerator, TKey>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
            where TEnumerator : IEnumerator<TSource>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (keySelector == null) throw Error.ArgumentNull(nameof(keySelector));
            var d = source.GetCount(out int capacity) ? new Dictionary<TKey, TSource>(capacity, comparer) : new Dictionary<TKey, TSource>(comparer);
            foreach (var element in source)
                d.Add(keySelector(element), element);
            return d;
        }

        public static Dictionary<TKey, TElement> ToDictionary<TSource, TEnumerator, TKey, TElement>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
            where TEnumerator : IEnumerator<TSource>
            => ToDictionary(source, keySelector, elementSelector, null);

        public static Dictionary<TKey, TElement> ToDictionary<TSource, TEnumerator, TKey, TElement>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
            where TEnumerator : IEnumerator<TSource>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (keySelector == null) throw Error.ArgumentNull(nameof(keySelector));
            if (elementSelector == null) throw Error.ArgumentNull(nameof(elementSelector));
            var d = source.GetCount(out int capacity) ? new Dictionary<TKey, TElement>(capacity, comparer) : new Dictionary<TKey, TElement>(comparer);
            foreach (var element in source)
                d.Add(keySelector(element), elementSelector(element));
            return d;
        }
        #endregion

        #region ToList
        public static List<TSource> ToList<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source)
            where TEnumerator : IEnumerator<TSource>
            => ToListImpl(source ?? throw Error.ArgumentNull(nameof(source)), source.GetCount(out int capacity) ? new List<TSource>(capacity) : new List<TSource>());

        public static List<TSource> ToList<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, int initCapacity)
            where TEnumerator : IEnumerator<TSource>
            => ToListImpl(source ?? throw Error.ArgumentNull(nameof(source)), new List<TSource>(source.GetCount(out int capacity) ? capacity : initCapacity));

        private static List<TSource> ToListImpl<TSource, TEnumerator>(IEnumerable<TSource, TEnumerator> source, List<TSource> list)
            where TEnumerator : IEnumerator<TSource>
        {
            using (var e = source.GetEnumerator())
                while (e.MoveNext())
                    list.Add(e.Current);
            return list;
        }
        #endregion

        #region ToLookup
        public static Lookup<TKey, TSource, EquatableEqualityComparer<TKey>> ToLookup<TSource, TKey, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey> keySelector)
            where TKey : IEquatable<TKey>
            where TEnumerator : IEnumerator<TSource>
            => ToLookupImpl(source ?? throw Error.ArgumentNull(nameof(source)), keySelector ?? throw Error.ArgumentNull(nameof(keySelector)), Util.Identity<TSource>(), new EquatableEqualityComparer<TKey>());

        public static Lookup<TKey, TSource, IEqualityComparer<TKey>> ToLookup<TSource, TKey, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
            where TEnumerator : IEnumerator<TSource>
            => ToLookupImpl(source ?? throw Error.ArgumentNull(nameof(source)), keySelector ?? throw Error.ArgumentNull(nameof(keySelector)), Util.Identity<TSource>(), comparer ?? EqualityComparer<TKey>.Default);

        public static Lookup<TKey, TSource, TEqualityComparer> ToLookup<TSource, TKey, TEnumerator, TEqualityComparer>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey> keySelector, TEqualityComparer comparer)
            where TEnumerator : IEnumerator<TSource>
            where TEqualityComparer : struct, IEqualityComparer<TKey>
            => ToLookupImpl(source ?? throw Error.ArgumentNull(nameof(source)), keySelector ?? throw Error.ArgumentNull(nameof(keySelector)), Util.Identity<TSource>(), comparer);

        public static Lookup<TKey, TElement, EquatableEqualityComparer<TKey>> ToLookup<TSource, TKey, TElement, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
            where TKey : IEquatable<TKey>
            where TEnumerator : IEnumerator<TSource>
            => ToLookupImpl(source ?? throw Error.ArgumentNull(nameof(source)), keySelector ?? throw Error.ArgumentNull(nameof(keySelector)), elementSelector ?? throw Error.ArgumentNull(nameof(elementSelector)), new EquatableEqualityComparer<TKey>());

        public static Lookup<TKey, TElement, IEqualityComparer<TKey>> ToLookup<TSource, TKey, TElement, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
            where TEnumerator : IEnumerator<TSource>
            => ToLookupImpl(source ?? throw Error.ArgumentNull(nameof(source)), keySelector ?? throw Error.ArgumentNull(nameof(keySelector)), elementSelector ?? throw Error.ArgumentNull(nameof(elementSelector)), comparer ?? EqualityComparer<TKey>.Default);

        public static Lookup<TKey, TElement, TEqualityComparer> ToLookup<TSource, TKey, TElement, TEnumerator, TEqualityComparer>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, TEqualityComparer comparer)
            where TEnumerator : IEnumerator<TSource>
            where TEqualityComparer : struct, IEqualityComparer<TKey>
            => ToLookupImpl(source ?? throw Error.ArgumentNull(nameof(source)), keySelector ?? throw Error.ArgumentNull(nameof(keySelector)), elementSelector ?? throw Error.ArgumentNull(nameof(elementSelector)), comparer);

        #region String key optimization
        public static Lookup<string, TSource, Cmp.StringComparer> ToLookup<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, string> keySelector)
            where TEnumerator : IEnumerator<TSource>
            => ToLookupImpl(source ?? throw Error.ArgumentNull(nameof(source)), keySelector ?? throw Error.ArgumentNull(nameof(keySelector)), Util.Identity<TSource>(), new Cmp.StringComparer());

        public static Lookup<string, TElement, Cmp.StringComparer> ToLookup<TSource, TElement, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, string> keySelector, Func<TSource, TElement> elementSelector)
            where TEnumerator : IEnumerator<TSource>
            => ToLookupImpl(source ?? throw Error.ArgumentNull(nameof(source)), keySelector ?? throw Error.ArgumentNull(nameof(keySelector)), elementSelector ?? throw Error.ArgumentNull(nameof(elementSelector)), new Cmp.StringComparer());
        #endregion

        #region Nullable key optimization
        public static Lookup<TKey?, TSource, NullableEqualityComparer<TKey>> ToLookup<TSource, TKey, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey?> keySelector)
            where TKey : struct, IEquatable<TKey>
            where TEnumerator : IEnumerator<TSource>
            => ToLookupImpl(source ?? throw Error.ArgumentNull(nameof(source)), keySelector ?? throw Error.ArgumentNull(nameof(keySelector)), Util.Identity<TSource>(), new NullableEqualityComparer<TKey>());

        public static Lookup<TKey?, TElement, NullableEqualityComparer<TKey>> ToLookup<TSource, TKey, TElement, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey?> keySelector, Func<TSource, TElement> elementSelector)
            where TKey : struct, IEquatable<TKey>
            where TEnumerator : IEnumerator<TSource>
            => ToLookupImpl(source ?? throw Error.ArgumentNull(nameof(source)), keySelector ?? throw Error.ArgumentNull(nameof(keySelector)), elementSelector ?? throw Error.ArgumentNull(nameof(elementSelector)), new NullableEqualityComparer<TKey>());
        #endregion

        private static Lookup<TKey, TElement, TEqualityComparer> ToLookupImpl<TSource, TKey, TElement, TEnumerator, TEqualityComparer>(IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, TEqualityComparer comparer)
            where TEnumerator : IEnumerator<TSource>
            where TEqualityComparer : IEqualityComparer<TKey>
            => Lookup<TKey, TElement, TEqualityComparer>.Create(source, keySelector, elementSelector, comparer);
        #endregion

        #region Where
        public static WhereEnumerable<TSource, TEnumerator> Where<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, bool> predicate)
            where TEnumerator : IEnumerator<TSource>
            => new WhereEnumerable<TSource, TEnumerator>(source ?? throw Error.ArgumentNull(nameof(source)), predicate ?? throw Error.ArgumentNull(nameof(predicate)));
        #endregion

        #region Zip
        public static ZipEnumerable<TFirst, TSecond, TResult, TEnumerator1, TEnumerator2> Zip<TFirst, TSecond, TResult, TEnumerator1, TEnumerator2>(this IEnumerable<TFirst, TEnumerator1> first, IEnumerable<TSecond, TEnumerator2> second, Func<TFirst, TSecond, TResult> resultSelector)
            where TEnumerator1 : IEnumerator<TFirst>
            where TEnumerator2 : IEnumerator<TSecond>
            => new ZipEnumerable<TFirst, TSecond, TResult, TEnumerator1, TEnumerator2>(first ?? throw Error.ArgumentNull(nameof(first)), second ?? throw Error.ArgumentNull(nameof(second)), resultSelector ?? throw Error.ArgumentNull(nameof(resultSelector)));
        #endregion
    }
}
