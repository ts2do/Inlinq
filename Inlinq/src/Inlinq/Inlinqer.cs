using System;
using System.Collections;
using System.Collections.Generic;
using Inlinq.Impl;
using Inlinq.Cmp;

namespace Inlinq
{
    public static partial class Inlinqer
    {
        #region Aggregate
        public static TSource Aggregate<TSource, TEnumerator, TFunc>(this IEnumerable<TSource, TEnumerator> source, TFunc func)
            where TEnumerator : IEnumerator<TSource>
            where TFunc : IFunctor<TSource, TSource, TSource>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (func == null) throw Error.ArgumentNull(nameof(func));
            return AggregateImpl(source, func);
        }

        public static TSource Aggregate<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TSource, TSource> func)
            where TEnumerator : IEnumerator<TSource>
        {
            return AggregateImpl(source ?? throw Error.ArgumentNull(nameof(source)), new FuncFunc<TSource, TSource, TSource>(func ?? throw Error.ArgumentNull(nameof(func))));
        }

        private static TSource AggregateImpl<TSource, TEnumerator, TFunc>(this IEnumerable<TSource, TEnumerator> source, TFunc func)
            where TEnumerator : IEnumerator<TSource>
            where TFunc : IFunctor<TSource, TSource, TSource>
        {
            using (var e = source.GetEnumerator())
            {
                if (!e.MoveNext()) throw Error.NoElements();
                var f = func;
                var result = e.Current;
                while (e.MoveNext())
                    result = f.Invoke(result, e.Current);
                return result;
            }
        }

        public static TAccumulate Aggregate<TSource, TAccumulate, TEnumerator, TFunc>(this IEnumerable<TSource, TEnumerator> source, TAccumulate seed, TFunc func)
            where TEnumerator : IEnumerator<TSource>
            where TFunc : IFunctor<TAccumulate, TSource, TAccumulate>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (func == null) throw Error.ArgumentNull(nameof(func));
            return AggregateImpl(source, seed, func);
        }

        public static TAccumulate Aggregate<TSource, TAccumulate, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func)
            where TEnumerator : IEnumerator<TSource>
        {
            return AggregateImpl(source ?? throw Error.ArgumentNull(nameof(source)), seed, new FuncFunc<TAccumulate, TSource, TAccumulate>(func ?? throw Error.ArgumentNull(nameof(func))));
        }

        private static TAccumulate AggregateImpl<TSource, TAccumulate, TEnumerator, TFunc>(this IEnumerable<TSource, TEnumerator> source, TAccumulate seed, TFunc func)
            where TEnumerator : IEnumerator<TSource>
            where TFunc : IFunctor<TAccumulate, TSource, TAccumulate>
        {
            foreach (var element in source)
                seed = func.Invoke(seed, element);
            return seed;
        }

        public static TResult Aggregate<TSource, TEnumerator, TAccumulate, TResult, TFunc>(this IEnumerable<TSource, TEnumerator> source, TAccumulate seed, TFunc func, Func<TAccumulate, TResult> resultSelector)
            where TEnumerator : IEnumerator<TSource>
            where TFunc : IFunctor<TAccumulate, TSource, TAccumulate>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (func == null) throw Error.ArgumentNull(nameof(func));
            if (resultSelector == null) throw Error.ArgumentNull(nameof(resultSelector));
            return AggregateImpl(source, seed, func, resultSelector);
        }

        public static TResult Aggregate<TSource, TEnumerator, TAccumulate, TResult>(this IEnumerable<TSource, TEnumerator> source, TAccumulate seed,
            Func<TAccumulate, TSource, TAccumulate> func, Func<TAccumulate, TResult> resultSelector)
            where TEnumerator : IEnumerator<TSource>
        {
            return AggregateImpl(source ?? throw Error.ArgumentNull(nameof(source)), seed, new FuncFunc<TAccumulate, TSource, TAccumulate>(func ?? throw Error.ArgumentNull(nameof(func))),
                resultSelector ?? throw Error.ArgumentNull(nameof(resultSelector)));
        }

        private static TResult AggregateImpl<TSource, TEnumerator, TAccumulate, TResult, TFunc>(this IEnumerable<TSource, TEnumerator> source, TAccumulate seed, TFunc func, Func<TAccumulate, TResult> resultSelector)
            where TEnumerator : IEnumerator<TSource>
            where TFunc : IFunctor<TAccumulate, TSource, TAccumulate>
        {
            foreach (var element in source)
                seed = func.Invoke(seed, element);
            return resultSelector(seed);
        }
        #endregion

        #region All
        public static bool All<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, bool> predicate)
            where TEnumerator : IEnumerator<TSource>
        {
            return AllImpl(source ?? throw Error.ArgumentNull(nameof(source)), new FuncFunc<TSource, bool>(predicate ?? throw Error.ArgumentNull(nameof(predicate))));
        }

        public static bool All<TSource, TEnumerator, TPredicate>(this IEnumerable<TSource, TEnumerator> source, TPredicate predicate)
            where TEnumerator : IEnumerator<TSource>
            where TPredicate : IFunctor<TSource, bool>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (predicate == null) throw Error.ArgumentNull(nameof(predicate));
            return AllImpl(source, predicate);
        }

        private static bool AllImpl<TSource, TEnumerator, TPredicate>(this IEnumerable<TSource, TEnumerator> source, TPredicate predicate)
            where TEnumerator : IEnumerator<TSource>
            where TPredicate : IFunctor<TSource, bool>
        {
            foreach (var element in source)
                if (!predicate.Invoke(element))
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

        public static bool Any<TSource, TEnumerator, TPredicate>(this IEnumerable<TSource, TEnumerator> source, TPredicate predicate)
            where TEnumerator : IEnumerator<TSource>
            where TPredicate : IFunctor<TSource, bool>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (predicate == null) throw Error.ArgumentNull(nameof(predicate));
            return AnyImpl(source, predicate);
        }

        public static bool Any<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, bool> predicate)
            where TEnumerator : IEnumerator<TSource>
        {
            return Any(source ?? throw Error.ArgumentNull(nameof(source)), new FuncFunc<TSource, bool>(predicate ?? throw Error.ArgumentNull(nameof(predicate))));
        }

        private static bool AnyImpl<TSource, TEnumerator, TPredicate>(this IEnumerable<TSource, TEnumerator> source, TPredicate predicate)
            where TEnumerator : IEnumerator<TSource>
            where TPredicate : IFunctor<TSource, bool>
        {
            foreach (var element in source)
                if (predicate.Invoke(element))
                    return true;
            return false;
        }
        #endregion

        #region Cast
        public static CastEnumerable<TResult> Cast<TResult>(this IEnumerable source)
            => new CastEnumerable<TResult>(source ?? throw Error.ArgumentNull(nameof(source)));
        #endregion

        #region Concat
        public static ConcatEnumerable<TSource, TEnumerator1, TEnumerator2>
            Concat<TSource, TEnumerator1, TEnumerator2>(this IEnumerable<TSource, TEnumerator1> first, IEnumerable<TSource, TEnumerator2> second)
            where TEnumerator1 : IEnumerator<TSource>
            where TEnumerator2 : IEnumerator<TSource>
        {
            return new ConcatEnumerable<TSource, TEnumerator1, TEnumerator2>(first ?? throw Error.ArgumentNull(nameof(first)), second ?? throw Error.ArgumentNull(nameof(second)));
        }
        #endregion

        #region Count
        public static int Count<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source)
            where TEnumerator : IEnumerator<TSource>
        {
            if (source.GetCount(out int count))
                return count;

            using (var e = (source ?? throw Error.ArgumentNull(nameof(source))).GetEnumerator())
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

        public static int Count<TSource, TEnumerator, TPredicate>(this IEnumerable<TSource, TEnumerator> source, TPredicate predicate)
            where TEnumerator : IEnumerator<TSource>
            where TPredicate : IFunctor<TSource, bool>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (predicate == null) throw Error.ArgumentNull(nameof(predicate));
            return CountImpl(source, predicate);
        }

        public static int Count<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, bool> predicate)
            where TEnumerator : IEnumerator<TSource>
        {
            return CountImpl(source ?? throw Error.ArgumentNull(nameof(source)), new FuncFunc<TSource, bool>(predicate ?? throw Error.ArgumentNull(nameof(predicate))));
        }

        private static int CountImpl<TSource, TEnumerator, TPredicate>(IEnumerable<TSource, TEnumerator> source, TPredicate predicate)
            where TEnumerator : IEnumerator<TSource>
            where TPredicate : IFunctor<TSource, bool>
        {
            int count = 0;
            foreach (var element in source)
            {
                if (predicate.Invoke(element))
                    count = checked(count + 1);

            }
            return count;
        }
        #endregion

        #region DefaultIfEmpty
        public static DefaultIfEmptyEnumerable<TSource, TEnumerator> DefaultIfEmpty<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source)
                where TEnumerator : IEnumerator<TSource>
        {
            return new DefaultIfEmptyEnumerable<TSource, TEnumerator>(source ?? throw Error.ArgumentNull(nameof(source)), default(TSource));
        }

        public static DefaultIfEmptyEnumerable<TSource, TEnumerator> DefaultIfEmpty<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, TSource defaultValue)
            where TEnumerator : IEnumerator<TSource>
        {
            return new DefaultIfEmptyEnumerable<TSource, TEnumerator>(source ?? throw Error.ArgumentNull(nameof(source)), defaultValue);
        }
        #endregion

        #region ElementAt
        public static TSource ElementAt<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, int index)
            where TEnumerator : IEnumerator<TSource>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (index < 0) throw Error.ArgumentOutOfRange(nameof(index));
            if (source is IList<TSource> list)
                return list[index];

            if (!(source is ICollection<TSource> collection1) || collection1.Count > index)
            {
                if (!(source is ICollection collection2) || collection2.Count > index)
                {
                    using (var e = source.GetEnumerator())
                    {
                        for (; index >= 0; --index)
                            if (!e.MoveNext())
                                break;
                        return e.Current;
                    }
                }
            }
            throw Error.ArgumentOutOfRange(nameof(index));
        }
        #endregion

        #region ElementAtOrDefault
        public static TSource ElementAtOrDefault<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, int index)
            where TEnumerator : IEnumerator<TSource>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (index < 0) throw Error.ArgumentOutOfRange(nameof(index));
            if (source is IList<TSource> list)
                return list[index];

            if (!(source is ICollection<TSource> collection1) || collection1.Count > index)
            {
                if (!(source is ICollection collection2) || collection2.Count > index)
                {
                    using (var e = source.GetEnumerator())
                    {
                        for (; index >= 0; --index)
                            if (!e.MoveNext())
                                break;
                        return e.Current;
                    }
                }
            }
            return default(TSource);
        }
        #endregion

        #region Empty
        public static EmptyEnumerable<TSource> Empty<TSource>()
            => EmptyEnumerable<TSource>.Instance;
        #endregion

        #region First
        public static TSource First<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source)
            where TEnumerator : IEnumerator<TSource>
        {
            using (var e = (source ?? throw Error.ArgumentNull(nameof(source))).GetEnumerator())
            {
                if (!e.MoveNext())
                    throw Error.NoElements();
                return e.Current;
            }
        }

        public static TSource First<TSource, TEnumerator, TPredicate>(this IEnumerable<TSource, TEnumerator> source, TPredicate predicate)
            where TEnumerator : IEnumerator<TSource>
            where TPredicate : IFunctor<TSource, bool>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (predicate == null) throw Error.ArgumentNull(nameof(predicate));
            return FirstImpl(source, predicate);
        }

        public static TSource First<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, bool> predicate)
            where TEnumerator : IEnumerator<TSource>
        {
            return FirstImpl(source ?? throw Error.ArgumentNull(nameof(source)), new FuncFunc<TSource, bool>(predicate ?? throw Error.ArgumentNull(nameof(predicate))));
        }

        private static TSource FirstImpl<TSource, TEnumerator, TPredicate>(IEnumerable<TSource, TEnumerator> source, TPredicate predicate)
            where TEnumerator : IEnumerator<TSource>
            where TPredicate : IFunctor<TSource, bool>
        {
            foreach (var element in source)
                if (predicate.Invoke(element))
                    return element;
            throw Error.NoMatch();
        }
        #endregion

        #region FirstOrDefault
        public static TSource FirstOrDefault<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source)
            where TEnumerator : IEnumerator<TSource>
        {
            using (var e = (source ?? throw Error.ArgumentNull(nameof(source))).GetEnumerator())
            {
                if (!e.MoveNext())
                    return default(TSource);
                return e.Current;
            }
        }

        public static TSource FirstOrDefault<TSource, TEnumerator, TPredicate>(this IEnumerable<TSource, TEnumerator> source, TPredicate predicate)
            where TEnumerator : IEnumerator<TSource>
            where TPredicate : IFunctor<TSource, bool>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (predicate == null) throw Error.ArgumentNull(nameof(predicate));
            return FirstOrDefaultImpl(source, predicate);
        }

        public static TSource FirstOrDefault<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, bool> predicate)
            where TEnumerator : IEnumerator<TSource>
        {
            return FirstOrDefaultImpl(source ?? throw Error.ArgumentNull(nameof(source)), new FuncFunc<TSource, bool>(predicate ?? throw Error.ArgumentNull(nameof(predicate))));
        }

        private static TSource FirstOrDefaultImpl<TSource, TEnumerator, TPredicate>(IEnumerable<TSource, TEnumerator> source, TPredicate predicate)
            where TEnumerator : IEnumerator<TSource>
            where TPredicate : IFunctor<TSource, bool>
        {
            foreach (var element in source)
                if (predicate.Invoke(element))
                    return element;
            return default(TSource);
        }
        #endregion

        // Join

        #region Last
        public static TSource Last<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source)
            where TEnumerator : IEnumerator<TSource>
        {
            using (var e = (source ?? throw Error.ArgumentNull(nameof(source))).GetEnumerator())
            {
                if (!e.MoveNext())
                    throw Error.NoElements();
                var last = e.Current;
                while (e.MoveNext())
                    last = e.Current;
                return last;
            }
        }

        public static TSource Last<TSource, TEnumerator, TPredicate>(this IEnumerable<TSource, TEnumerator> source, TPredicate predicate)
            where TEnumerator : IEnumerator<TSource>
            where TPredicate : IFunctor<TSource, bool>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (predicate == null) throw Error.ArgumentNull(nameof(predicate));
            return LastImpl(source, predicate);
        }

        public static TSource Last<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, bool> predicate)
            where TEnumerator : IEnumerator<TSource>
        {
            return LastImpl(source ?? throw Error.ArgumentNull(nameof(source)), new FuncFunc<TSource, bool>(predicate ?? throw Error.ArgumentNull(nameof(predicate))));
        }

        private static TSource LastImpl<TSource, TEnumerator, TPredicate>(IEnumerable<TSource, TEnumerator> source, TPredicate predicate)
            where TEnumerator : IEnumerator<TSource>
            where TPredicate : IFunctor<TSource, bool>
        {
            using (var e = source.GetEnumerator())
            {
                while (e.MoveNext())
                {
                    var last = e.Current;
                    if (predicate.Invoke(last))
                    {
                        while (e.MoveNext())
                        {
                            var element = e.Current;
                            if (predicate.Invoke(last))
                                last = element;
                        }
                        return last;
                    }
                }
                throw Error.NoElements();
            }
        }
        #endregion

        #region LastOrDefault
        public static TSource LastOrDefault<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source)
            where TEnumerator : IEnumerator<TSource>
        {
            using (var e = (source ?? throw Error.ArgumentNull(nameof(source))).GetEnumerator())
            {
                if (!e.MoveNext())
                    return default(TSource);
                var last = e.Current;
                while (e.MoveNext())
                    last = e.Current;
                return last;
            }
        }

        public static TSource LastOrDefault<TSource, TEnumerator, TPredicate>(this IEnumerable<TSource, TEnumerator> source, TPredicate predicate)
            where TEnumerator : IEnumerator<TSource>
            where TPredicate : IFunctor<TSource, bool>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (predicate == null) throw Error.ArgumentNull(nameof(predicate));
            return LastOrDefaultImpl(source, predicate);
        }

        public static TSource LastOrDefault<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, bool> predicate)
            where TEnumerator : IEnumerator<TSource>
        {
            return LastOrDefaultImpl(source ?? throw Error.ArgumentNull(nameof(source)), new FuncFunc<TSource, bool>(predicate ?? throw Error.ArgumentNull(nameof(predicate))));
        }

        private static TSource LastOrDefaultImpl<TSource, TEnumerator, TPredicate>(IEnumerable<TSource, TEnumerator> source, TPredicate predicate)
            where TEnumerator : IEnumerator<TSource>
            where TPredicate : IFunctor<TSource, bool>
        {
            using (var e = source.GetEnumerator())
            {
                while (e.MoveNext())
                {
                    var last = e.Current;
                    if (predicate.Invoke(last))
                    {
                        while (e.MoveNext())
                        {
                            var element = e.Current;
                            if (predicate.Invoke(last))
                                last = element;
                        }
                        return last;
                    }
                }
                return default(TSource);
            }
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

        public static long LongCount<TSource, TEnumerator, TPredicate>(this IEnumerable<TSource, TEnumerator> source, TPredicate predicate)
            where TEnumerator : IEnumerator<TSource>
            where TPredicate : IFunctor<TSource, bool>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (predicate == null) throw Error.ArgumentNull(nameof(predicate));
            return LongCountImpl(source, predicate);
        }

        public static long LongCount<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, bool> predicate)
            where TEnumerator : IEnumerator<TSource>
        {
            return LongCountImpl(source ?? throw Error.ArgumentNull(nameof(source)), new FuncFunc<TSource, bool>(predicate ?? throw Error.ArgumentNull(nameof(predicate))));
        }

        private static long LongCountImpl<TSource, TEnumerator, TPredicate>(IEnumerable<TSource, TEnumerator> source, TPredicate predicate)
            where TEnumerator : IEnumerator<TSource>
            where TPredicate : IFunctor<TSource, bool>
        {
            long count = 0;
            foreach (var element in source)
            {
                if (predicate.Invoke(element))
                    count = checked(count + 1);

            }
            return count;
        }
        #endregion

        #region Max
        public static TSource Max<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source)
            where TSource : IComparable<TSource>
            where TEnumerator : IEnumerator<TSource>
        {
            return MaxImpl(source ?? throw Error.ArgumentNull(nameof(source)), Comparers.Comparable<TSource>());
        }

        public static TSource Max<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, IComparer<TSource> comparer)
            where TEnumerator : IEnumerator<TSource>
        {
            return MaxImpl(source ?? throw Error.ArgumentNull(nameof(source)), comparer ?? Comparer<TSource>.Default);
        }

        public static TSource Max<TSource, TEnumerator, TComparer>(this IEnumerable<TSource, TEnumerator> source, TComparer comparer)
            where TEnumerator : IEnumerator<TSource>
            where TComparer : struct, IComparer<TSource>
        {
            return MaxImpl(source ?? throw Error.ArgumentNull(nameof(source)), comparer);
        }

        public static TResult Max<TSource, TResult, TEnumerator, TSelector>(this IEnumerable<TSource, TEnumerator> source, ITFunctor<TSelector, TSource, TResult> selector)
            where TResult : IComparable<TResult>
            where TEnumerator : IEnumerator<TSource>
            where TSelector : IFunctor<TSource, TResult>
        {
            return MaxImpl<TSource, TResult, TEnumerator, TSelector, ComparableComparer<TResult>>(
                source ?? throw Error.ArgumentNull(nameof(source)), (selector ?? throw Error.ArgumentNull(nameof(selector))).Unwrap(), Comparers.Comparable<TResult>());
        }

        public static TResult Max<TSource, TResult, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TResult> selector)
            where TResult : IComparable<TResult>
            where TEnumerator : IEnumerator<TSource>
        {
            return MaxImpl<TSource, TResult, TEnumerator, FuncFunc<TSource, TResult>, ComparableComparer<TResult>>(
                source ?? throw Error.ArgumentNull(nameof(source)), new FuncFunc<TSource, TResult>(selector ?? throw Error.ArgumentNull(nameof(selector))), Comparers.Comparable<TResult>());
        }

        public static TResult Max<TSource, TResult, TEnumerator, TSelector>(this IEnumerable<TSource, TEnumerator> source, ITFunctor<TSelector, TSource, TResult> selector, IComparer<TResult> comparer)
            where TEnumerator : IEnumerator<TSource>
            where TSelector : IFunctor<TSource, TResult>
        {
            return MaxImpl<TSource, TResult, TEnumerator, TSelector, IComparer<TResult>>(
                source ?? throw Error.ArgumentNull(nameof(source)), (selector ?? throw Error.ArgumentNull(nameof(selector))).Unwrap(), comparer ?? Comparer<TResult>.Default);
        }

        public static TResult Max<TSource, TResult, TEnumerator, TSelector, TComparer>(this IEnumerable<TSource, TEnumerator> source, ITFunctor<TSelector, TSource, TResult> selector, TComparer comparer)
            where TEnumerator : IEnumerator<TSource>
            where TSelector : IFunctor<TSource, TResult>
            where TComparer : struct, IComparer<TResult>
        {
            return MaxImpl<TSource, TResult, TEnumerator, TSelector, TComparer>(
                source ?? throw Error.ArgumentNull(nameof(source)), (selector ?? throw Error.ArgumentNull(nameof(selector))).Unwrap(), comparer);
        }

        public static TResult Max<TSource, TResult, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TResult> selector, IComparer<TResult> comparer)
            where TEnumerator : IEnumerator<TSource>
        {
            return MaxImpl<TSource, TResult, TEnumerator, FuncFunc<TSource, TResult>, IComparer<TResult>>(
                source ?? throw Error.ArgumentNull(nameof(source)), new FuncFunc<TSource, TResult>(selector ?? throw Error.ArgumentNull(nameof(selector))), comparer ?? Comparer<TResult>.Default);
        }

        public static TResult Max<TSource, TResult, TEnumerator, TComparer>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TResult> selector, TComparer comparer)
            where TEnumerator : IEnumerator<TSource>
            where TComparer : struct, IComparer<TResult>
        {
            return MaxImpl<TSource, TResult, TEnumerator, FuncFunc<TSource, TResult>, TComparer>(
                source ?? throw Error.ArgumentNull(nameof(source)), new FuncFunc<TSource, TResult>(selector ?? throw Error.ArgumentNull(nameof(selector))), comparer);
        }

        #region Nullable optimization
        public static TSource? Max<TSource, TEnumerator>(this IEnumerable<TSource?, TEnumerator> source)
            where TSource : struct, IComparable<TSource>
            where TEnumerator : IEnumerator<TSource?>
        {
            return MaxImpl(source ?? throw Error.ArgumentNull(nameof(source)), Comparers.Nullable<TSource>());
        }

        public static TResult? Max<TSource, TResult, TEnumerator, TSelector>(this IEnumerable<TSource, TEnumerator> source, ITFunctor<TSelector, TSource, TResult?> selector)
            where TResult : struct, IComparable<TResult>
            where TEnumerator : IEnumerator<TSource>
            where TSelector : IFunctor<TSource, TResult?>
        {
            return MaxImpl<TSource, TResult?, TEnumerator, TSelector, NullableComparer<TResult, ComparableComparer<TResult>>>(
                source ?? throw Error.ArgumentNull(nameof(source)), (selector ?? throw Error.ArgumentNull(nameof(selector))).Unwrap(), Comparers.Nullable<TResult>());
        }

        public static TResult? Max<TSource, TResult, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TResult?> selector)
            where TResult : struct, IComparable<TResult>
            where TEnumerator : IEnumerator<TSource>
        {
            return MaxImpl<TSource, TResult?, TEnumerator, FuncFunc<TSource, TResult?>, NullableComparer<TResult, ComparableComparer<TResult>>>(
                source ?? throw Error.ArgumentNull(nameof(source)), new FuncFunc<TSource, TResult?>(selector ?? throw Error.ArgumentNull(nameof(selector))), Comparers.Nullable<TResult>());
        }
        #endregion

        private static TSource MaxImpl<TSource, TEnumerator, TComparer>(IEnumerable<TSource, TEnumerator> source, TComparer comparer)
            where TEnumerator : IEnumerator<TSource>
            where TComparer : IComparer<TSource>
        {
            using (var e = source.GetEnumerator())
                return MaxImpl<TSource, TEnumerator, TComparer>(e, comparer);
        }

        private static TResult MaxImpl<TSource, TResult, TEnumerator, TSelector, TComparer>(this IEnumerable<TSource, TEnumerator> source, TSelector selector, TComparer comparer)
            where TEnumerator : IEnumerator<TSource>
            where TSelector : IFunctor<TSource, TResult>
            where TComparer : IComparer<TResult>
        {
            using (var e = new SelectEnumerator<TSource, TResult, TEnumerator, TSelector>(source.GetEnumerator(), selector))
                return MaxImpl<TResult, SelectEnumerator<TSource, TResult, TEnumerator, TSelector>, TComparer>(e, comparer);
        }

        private static TSource MaxImpl<TSource, TEnumerator, TComparer>(TEnumerator source, TComparer comparer)
            where TEnumerator : IEnumerator<TSource>
            where TComparer : IComparer<TSource>
        {
            var defaultValue = default(TSource);
            if (!source.MoveNext())
            {
                if (defaultValue == null)
                    return defaultValue;
                throw Error.NoElements();
            }
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
        #endregion

        #region Min
        public static TSource Min<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source)
            where TSource : IComparable<TSource>
            where TEnumerator : IEnumerator<TSource>
        {
            return MinImpl(source ?? throw Error.ArgumentNull(nameof(source)), Comparers.Comparable<TSource>());
        }

        public static TSource Min<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, IComparer<TSource> comparer)
            where TEnumerator : IEnumerator<TSource>
        {
            return MinImpl(source ?? throw Error.ArgumentNull(nameof(source)), comparer ?? Comparer<TSource>.Default);
        }

        public static TSource Min<TSource, TEnumerator, TComparer>(this IEnumerable<TSource, TEnumerator> source, TComparer comparer)
            where TEnumerator : IEnumerator<TSource>
            where TComparer : struct, IComparer<TSource>
        {
            return MinImpl(source ?? throw Error.ArgumentNull(nameof(source)), comparer);
        }

        public static TResult Min<TSource, TResult, TEnumerator, TSelector>(this IEnumerable<TSource, TEnumerator> source, ITFunctor<TSelector, TSource, TResult> selector)
            where TResult : IComparable<TResult>
            where TEnumerator : IEnumerator<TSource>
            where TSelector : IFunctor<TSource, TResult>
        {
            return MinImpl<TSource, TResult, TEnumerator, TSelector, ComparableComparer<TResult>>(
                source ?? throw Error.ArgumentNull(nameof(source)), (selector ?? throw Error.ArgumentNull(nameof(selector))).Unwrap(), Comparers.Comparable<TResult>());
        }

        public static TResult Min<TSource, TResult, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TResult> selector)
            where TResult : IComparable<TResult>
            where TEnumerator : IEnumerator<TSource>
        {
            return MinImpl<TSource, TResult, TEnumerator, FuncFunc<TSource, TResult>, ComparableComparer<TResult>>(
                source ?? throw Error.ArgumentNull(nameof(source)), new FuncFunc<TSource, TResult>(selector ?? throw Error.ArgumentNull(nameof(selector))), Comparers.Comparable<TResult>());
        }

        public static TResult Min<TSource, TResult, TEnumerator, TSelector>(this IEnumerable<TSource, TEnumerator> source, ITFunctor<TSelector, TSource, TResult> selector, IComparer<TResult> comparer)
            where TEnumerator : IEnumerator<TSource>
            where TSelector : IFunctor<TSource, TResult>
        {
            return MinImpl<TSource, TResult, TEnumerator, TSelector, IComparer<TResult>>(
                source ?? throw Error.ArgumentNull(nameof(source)), (selector ?? throw Error.ArgumentNull(nameof(selector))).Unwrap(), comparer ?? Comparer<TResult>.Default);
        }

        public static TResult Min<TSource, TResult, TEnumerator, TSelector, TComparer>(this IEnumerable<TSource, TEnumerator> source, ITFunctor<TSelector, TSource, TResult> selector, TComparer comparer)
            where TEnumerator : IEnumerator<TSource>
            where TSelector : IFunctor<TSource, TResult>
            where TComparer : struct, IComparer<TResult>
        {
            return MinImpl<TSource, TResult, TEnumerator, TSelector, TComparer>(
                source ?? throw Error.ArgumentNull(nameof(source)), (selector ?? throw Error.ArgumentNull(nameof(selector))).Unwrap(), comparer);
        }

        public static TResult Min<TSource, TResult, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TResult> selector, IComparer<TResult> comparer)
            where TEnumerator : IEnumerator<TSource>
        {
            return MinImpl<TSource, TResult, TEnumerator, FuncFunc<TSource, TResult>, IComparer<TResult>>(
                source ?? throw Error.ArgumentNull(nameof(source)), new FuncFunc<TSource, TResult>(selector ?? throw Error.ArgumentNull(nameof(selector))), comparer ?? Comparer<TResult>.Default);
        }

        public static TResult Min<TSource, TResult, TEnumerator, TComparer>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TResult> selector, TComparer comparer)
            where TEnumerator : IEnumerator<TSource>
            where TComparer : struct, IComparer<TResult>
        {
            return MinImpl<TSource, TResult, TEnumerator, FuncFunc<TSource, TResult>, TComparer>(
                source ?? throw Error.ArgumentNull(nameof(source)), new FuncFunc<TSource, TResult>(selector ?? throw Error.ArgumentNull(nameof(selector))), comparer);
        }

        #region Nullable optimization
        public static TSource? Min<TSource, TEnumerator>(this IEnumerable<TSource?, TEnumerator> source)
            where TSource : struct, IComparable<TSource>
            where TEnumerator : IEnumerator<TSource?>
        {
            return MinImpl(source ?? throw Error.ArgumentNull(nameof(source)), Comparers.Nullable<TSource>());
        }

        public static TResult? Min<TSource, TResult, TEnumerator, TSelector>(this IEnumerable<TSource, TEnumerator> source, ITFunctor<TSelector, TSource, TResult?> selector)
            where TResult : struct, IComparable<TResult>
            where TEnumerator : IEnumerator<TSource>
            where TSelector : IFunctor<TSource, TResult?>
        {
            return MinImpl<TSource, TResult?, TEnumerator, TSelector, NullableComparer<TResult, ComparableComparer<TResult>>>(
                source ?? throw Error.ArgumentNull(nameof(source)), (selector ?? throw Error.ArgumentNull(nameof(selector))).Unwrap(), Comparers.Nullable<TResult>());
        }

        public static TResult? Min<TSource, TResult, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TResult?> selector)
            where TResult : struct, IComparable<TResult>
            where TEnumerator : IEnumerator<TSource>
        {
            return MinImpl<TSource, TResult?, TEnumerator, FuncFunc<TSource, TResult?>, NullableComparer<TResult, ComparableComparer<TResult>>>(
                source ?? throw Error.ArgumentNull(nameof(source)), new FuncFunc<TSource, TResult?>(selector ?? throw Error.ArgumentNull(nameof(selector))), Comparers.Nullable<TResult>());
        }
        #endregion

        private static TSource MinImpl<TSource, TEnumerator, TComparer>(IEnumerable<TSource, TEnumerator> source, TComparer comparer)
            where TEnumerator : IEnumerator<TSource>
            where TComparer : IComparer<TSource>
        {
            using (var e = source.GetEnumerator())
                return MinImpl<TSource, TEnumerator, TComparer>(e, comparer);
        }

        private static TResult MinImpl<TSource, TResult, TEnumerator, TSelector, TComparer>(this IEnumerable<TSource, TEnumerator> source, TSelector selector, TComparer comparer)
            where TEnumerator : IEnumerator<TSource>
            where TSelector : IFunctor<TSource, TResult>
            where TComparer : IComparer<TResult>
        {
            using (var e = new SelectEnumerator<TSource, TResult, TEnumerator, TSelector>(source.GetEnumerator(), selector))
                return MinImpl<TResult, SelectEnumerator<TSource, TResult, TEnumerator, TSelector>, TComparer>(e, comparer);
        }

        private static TSource MinImpl<TSource, TEnumerator, TComparer>(TEnumerator source, TComparer comparer)
            where TEnumerator : IEnumerator<TSource>
            where TComparer : IComparer<TSource>
        {
            var defaultValue = default(TSource);
            if (!source.MoveNext())
            {
                if (defaultValue == null)
                    return defaultValue;
                throw Error.NoElements();
            }
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
        #endregion

        #region OfType
        public static OfTypeEnumerable<TResult> OfType<TResult>(this IEnumerable source)
            => new OfTypeEnumerable<TResult>(source ?? throw Error.ArgumentNull(nameof(source)));
        #endregion

        // OrderBy

        // OrderByDescending

        #region Range
        public static Range32Enumerable Range(int start, int count)
            => start + count >= start ? new Range32Enumerable(start, count) : throw Error.ArgumentOutOfRange(nameof(count));

        public static Range64Enumerable Range(long start, long count)
            => start + count >= start ? new Range64Enumerable(start, count) : throw Error.ArgumentOutOfRange(nameof(count));
        #endregion

        #region Repeat
        public static Repeat32Enumerable<TResult> Repeat<TResult>(TResult element, int count)
            => count >= 0 ? new Repeat32Enumerable<TResult>(element, count) : throw Error.ArgumentOutOfRange(nameof(count));

        public static Repeat64Enumerable<TResult> Repeat<TResult>(TResult element, long count)
            => count >= 0 ? new Repeat64Enumerable<TResult>(element, count) : throw Error.ArgumentOutOfRange(nameof(count));
        #endregion

        #region Select
        public static SelectEnumerable<TSource, TResult, TEnumerator, TSelector> Select<TSource, TResult, TEnumerator, TSelector>(
            this IEnumerable<TSource, TEnumerator> source, ITFunctor<TSelector, TSource, TResult> selector)
            where TEnumerator : IEnumerator<TSource>
            where TSelector : IFunctor<TSource, TResult>
        {
            return new SelectEnumerable<TSource, TResult, TEnumerator, TSelector>(source ?? throw Error.ArgumentNull(nameof(source)), (selector ?? throw Error.ArgumentNull(nameof(selector))).Unwrap());
        }

        public static SelectEnumerable<TSource, TResult, TEnumerator, FuncFunc<TSource, TResult>> Select<TSource, TResult, TEnumerator>(
            this IEnumerable<TSource, TEnumerator> source, Func<TSource, TResult> selector)
            where TEnumerator : IEnumerator<TSource>
        {
            return new SelectEnumerable<TSource, TResult, TEnumerator, FuncFunc<TSource, TResult>>(
                source ?? throw Error.ArgumentNull(nameof(source)), new FuncFunc<TSource, TResult>(selector ?? throw Error.ArgumentNull(nameof(selector))));
        }
        #endregion

        #region SelectMany
        public static SelectManyEnumerableA<TSource, TResult, TEnumerator1, TEnumerator2, FuncFunc<TSource, IEnumerable<TResult, TEnumerator2>>>
            SelectMany<TSource, TResult, TEnumerator1, TEnumerator2>(this IEnumerable<TSource, TEnumerator1> source, Func<TSource, IEnumerable<TResult, TEnumerator2>> selector)
            where TEnumerator1 : IEnumerator<TSource>
            where TEnumerator2 : IEnumerator<TResult>
        {
            return new SelectManyEnumerableA<TSource, TResult, TEnumerator1, TEnumerator2, FuncFunc<TSource, IEnumerable<TResult, TEnumerator2>>>(
                source ?? throw Error.ArgumentNull(nameof(source)), new FuncFunc<TSource, IEnumerable<TResult, TEnumerator2>>(selector ?? throw Error.ArgumentNull(nameof(selector))));
        }

        public static SelectManyEnumerableB<TSource, TResult, TEnumerator1, TEnumerator2, FuncFunc<TSource, int, IEnumerable<TResult, TEnumerator2>>>
            SelectMany<TSource, TResult, TEnumerator1, TEnumerator2>(this IEnumerable<TSource, TEnumerator1> source, Func<TSource, int, IEnumerable<TResult, TEnumerator2>> selector)
            where TEnumerator1 : IEnumerator<TSource>
            where TEnumerator2 : IEnumerator<TResult>
        {
            return new SelectManyEnumerableB<TSource, TResult, TEnumerator1, TEnumerator2, FuncFunc<TSource, int, IEnumerable<TResult, TEnumerator2>>>(
                source ?? throw Error.ArgumentNull(nameof(source)), new FuncFunc<TSource, int, IEnumerable<TResult, TEnumerator2>>(selector ?? throw Error.ArgumentNull(nameof(selector))));
        }

        public static SelectManyEnumerableC<TSource, TResult, TEnumerator, FuncFunc<TSource, IEnumerable<TResult>>>
            SelectMany<TSource, TResult, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, IEnumerable<TResult>> selector)
            where TEnumerator : IEnumerator<TSource>
        {
            return new SelectManyEnumerableC<TSource, TResult, TEnumerator, FuncFunc<TSource, IEnumerable<TResult>>>(
                source ?? throw Error.ArgumentNull(nameof(source)), new FuncFunc<TSource, IEnumerable<TResult>>(selector ?? throw Error.ArgumentNull(nameof(selector))));
        }

        public static SelectManyEnumerableC<TSource, TResult, TEnumerator, TSelector>
            SelectMany<TSource, TResult, TEnumerator, TSelector>(this IEnumerable<TSource, TEnumerator> source, ITFunctor<TSelector, TSource, IEnumerable<TResult>> selector)
            where TEnumerator : IEnumerator<TSource>
            where TSelector : IFunctor<TSource, IEnumerable<TResult>>
        {
            return new SelectManyEnumerableC<TSource, TResult, TEnumerator, TSelector>(
                source ?? throw Error.ArgumentNull(nameof(source)), selector!= null ? selector.Unwrap() : throw Error.ArgumentNull(nameof(selector)));
        }

        public static SelectManyEnumerableD<TSource, TResult, TEnumerator, FuncFunc<TSource, int, IEnumerable<TResult>>>
            SelectMany<TSource, TResult, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, int, IEnumerable<TResult>> selector)
            where TEnumerator : IEnumerator<TSource>
        {
            return new SelectManyEnumerableD<TSource, TResult, TEnumerator, FuncFunc<TSource, int, IEnumerable<TResult>>>(
                source ?? throw Error.ArgumentNull(nameof(source)), new FuncFunc<TSource, int, IEnumerable<TResult>>(selector ?? throw Error.ArgumentNull(nameof(selector))));
        }

        public static SelectManyEnumerableE<TSource, TCollection, TResult, TEnumerator1, TEnumerator2, FuncFunc<TSource, IEnumerable<TCollection, TEnumerator2>>, FuncFunc<TSource, TCollection, TResult>>
            SelectMany<TSource, TCollection, TResult, TEnumerator1, TEnumerator2>(this IEnumerable<TSource, TEnumerator1> source, Func<TSource, IEnumerable<TCollection, TEnumerator2>> collectionSelector,
            Func<TSource, TCollection, TResult> resultSelector)
            where TEnumerator1 : IEnumerator<TSource>
            where TEnumerator2 : IEnumerator<TCollection>
        {
            return new SelectManyEnumerableE<TSource, TCollection, TResult, TEnumerator1, TEnumerator2, FuncFunc<TSource, IEnumerable<TCollection, TEnumerator2>>, FuncFunc<TSource, TCollection, TResult>>(
                source ?? throw Error.ArgumentNull(nameof(source)), new FuncFunc<TSource, IEnumerable<TCollection, TEnumerator2>>(collectionSelector ?? throw Error.ArgumentNull(nameof(collectionSelector))), 
                new FuncFunc<TSource, TCollection, TResult>(resultSelector ?? throw Error.ArgumentNull(nameof(resultSelector))));
        }

        public static SelectManyEnumerableF<TSource, TCollection, TResult, TEnumerator1, TEnumerator2, FuncFunc<TSource, int, IEnumerable<TCollection, TEnumerator2>>, FuncFunc<TSource, TCollection, TResult>>
            SelectMany<TSource, TCollection, TResult, TEnumerator1, TEnumerator2>(this IEnumerable<TSource, TEnumerator1> source, Func<TSource, int, IEnumerable<TCollection, TEnumerator2>> collectionSelector,
            Func<TSource, TCollection, TResult> resultSelector)
            where TEnumerator1 : IEnumerator<TSource>
            where TEnumerator2 : IEnumerator<TCollection>
        {
            return new SelectManyEnumerableF<TSource, TCollection, TResult, TEnumerator1, TEnumerator2, FuncFunc<TSource, int, IEnumerable<TCollection, TEnumerator2>>, FuncFunc<TSource, TCollection, TResult>>(
                source ?? throw Error.ArgumentNull(nameof(source)), new FuncFunc<TSource, int, IEnumerable<TCollection, TEnumerator2>>(collectionSelector ?? throw Error.ArgumentNull(nameof(collectionSelector))),
                new FuncFunc<TSource, TCollection, TResult>(resultSelector ?? throw Error.ArgumentNull(nameof(resultSelector))));
        }

        public static SelectManyEnumerableG<TSource, TCollection, TResult, TEnumerator, FuncFunc<TSource, IEnumerable<TCollection>>, FuncFunc<TSource, TCollection, TResult>>
            SelectMany<TSource, TCollection, TResult, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, IEnumerable<TCollection>> collectionSelector,
            Func<TSource, TCollection, TResult> resultSelector)
            where TEnumerator : IEnumerator<TSource>
        {
            return new SelectManyEnumerableG<TSource, TCollection, TResult, TEnumerator, FuncFunc<TSource, IEnumerable<TCollection>>, FuncFunc<TSource, TCollection, TResult>>(
                source ?? throw Error.ArgumentNull(nameof(source)), new FuncFunc<TSource, IEnumerable<TCollection>>(collectionSelector ?? throw Error.ArgumentNull(nameof(collectionSelector))),
                new FuncFunc<TSource, TCollection, TResult>(resultSelector ?? throw Error.ArgumentNull(nameof(resultSelector))));
        }

        public static SelectManyEnumerableH<TSource, TCollection, TResult, TEnumerator, FuncFunc<TSource, int, IEnumerable<TCollection>>, FuncFunc<TSource, TCollection, TResult>>
            SelectMany<TSource, TCollection, TResult, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, int, IEnumerable<TCollection>> collectionSelector,
            Func<TSource, TCollection, TResult> resultSelector)
            where TEnumerator : IEnumerator<TSource>
        {
            return new SelectManyEnumerableH<TSource, TCollection, TResult, TEnumerator, FuncFunc<TSource, int, IEnumerable<TCollection>>, FuncFunc<TSource, TCollection, TResult>>(
                source ?? throw Error.ArgumentNull(nameof(source)), new FuncFunc<TSource, int, IEnumerable<TCollection>>(collectionSelector ?? throw Error.ArgumentNull(nameof(collectionSelector))),
                new FuncFunc<TSource, TCollection, TResult>(resultSelector ?? throw Error.ArgumentNull(nameof(resultSelector))));
        }
        #endregion

        #region SequenceEqual
        public static bool SequenceEqual<TSource, TEnumerator1, TEnumerator2>(this IEnumerable<TSource, TEnumerator1> first, IEnumerable<TSource, TEnumerator2> second)
            where TSource : IEquatable<TSource>
            where TEnumerator1 : IEnumerator<TSource>
            where TEnumerator2 : IEnumerator<TSource>
        {
            return SequenceEqualImpl(first ?? throw Error.ArgumentNull(nameof(first)), second ?? throw Error.ArgumentNull(nameof(second)), EqualityComparers.Equatable<TSource>());
        }

        public static bool SequenceEqual<TSource, TEnumerator1, TEnumerator2>(this IEnumerable<TSource, TEnumerator1> first, IEnumerable<TSource, TEnumerator2> second, IEqualityComparer<TSource> comparer)
            where TEnumerator1 : IEnumerator<TSource>
            where TEnumerator2 : IEnumerator<TSource>
        {
            return SequenceEqualImpl(first ?? throw Error.ArgumentNull(nameof(first)), second ?? throw Error.ArgumentNull(nameof(second)), comparer ?? EqualityComparer<TSource>.Default);
        }

        public static bool SequenceEqual<TSource, TEnumerator1, TEnumerator2, TEqualityComparer>(this IEnumerable<TSource, TEnumerator1> first, IEnumerable<TSource, TEnumerator2> second, TEqualityComparer comparer)
            where TEnumerator1 : IEnumerator<TSource>
            where TEnumerator2 : IEnumerator<TSource>
            where TEqualityComparer : struct, IEqualityComparer<TSource>
        {
            return SequenceEqualImpl(first ?? throw Error.ArgumentNull(nameof(first)), second ?? throw Error.ArgumentNull(nameof(second)), comparer);
        }

        public static bool SequenceEqual<TEnumerator1, TEnumerator2>(this IEnumerable<string, TEnumerator1> first, IEnumerable<string, TEnumerator2> second)
            where TEnumerator1 : IEnumerator<string>
            where TEnumerator2 : IEnumerator<string>
        {
            return SequenceEqualImpl(first ?? throw Error.ArgumentNull(nameof(first)), second ?? throw Error.ArgumentNull(nameof(second)), new Cmp.StringComparer());
        }

        public static bool SequenceEqual<TSource, TEnumerator1, TEnumerator2>(this IEnumerable<TSource?, TEnumerator1> first, IEnumerable<TSource?, TEnumerator2> second)
            where TSource : struct, IEquatable<TSource>
            where TEnumerator1 : IEnumerator<TSource?>
            where TEnumerator2 : IEnumerator<TSource?>
        {
            return SequenceEqualImpl(first ?? throw Error.ArgumentNull(nameof(first)), second ?? throw Error.ArgumentNull(nameof(second)), EqualityComparers.Nullable<TSource>());
        }

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
        {
            using (var e = (source ?? throw Error.ArgumentNull(nameof(source))).GetEnumerator())
            {
                if (!e.MoveNext())
                    throw Error.NoElements();
                var element = e.Current;
                if (e.MoveNext())
                    throw Error.MoreThanOneElement();
                return element;
            }
        }

        public static TSource Single<TSource, TEnumerator, TPredicate>(this IEnumerable<TSource, TEnumerator> source, TPredicate predicate)
            where TEnumerator : IEnumerator<TSource>
            where TPredicate : IFunctor<TSource, bool>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (predicate == null) throw Error.ArgumentNull(nameof(predicate));
            return SingleImpl(source, predicate);
        }

        public static TSource Single<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, bool> predicate)
            where TEnumerator : IEnumerator<TSource>
        {
            return SingleImpl(source ?? throw Error.ArgumentNull(nameof(source)), new FuncFunc<TSource, bool>(predicate ?? throw Error.ArgumentNull(nameof(predicate))));
        }

        private static TSource SingleImpl<TSource, TEnumerator, TPredicate>(IEnumerable<TSource, TEnumerator> source, TPredicate predicate)
            where TEnumerator : IEnumerator<TSource>
            where TPredicate : IFunctor<TSource, bool>
        {
            using (var e = source.GetEnumerator())
            {
                while (e.MoveNext())
                {
                    var element = e.Current;
                    if (predicate.Invoke(element))
                    {
                        while (e.MoveNext())
                            if (predicate.Invoke(e.Current))
                                throw Error.MoreThanOneMatch();
                        return element;
                    }
                }
                throw Error.NoElements();
            }
        }
        #endregion

        #region SingleOrDefault
        public static TSource SingleOrDefault<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source)
            where TEnumerator : IEnumerator<TSource>
        {
            using (var e = (source ?? throw Error.ArgumentNull(nameof(source))).GetEnumerator())
            {
                if (!e.MoveNext())
                    return default(TSource);
                var element = e.Current;
                if (!e.MoveNext())
                    return element;
                throw Error.MoreThanOneElement();
            }
        }

        public static TSource SingleOrDefault<TSource, TEnumerator, TPredicate>(this IEnumerable<TSource, TEnumerator> source, TPredicate predicate)
            where TEnumerator : IEnumerator<TSource>
            where TPredicate : IFunctor<TSource, bool>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (predicate == null) throw Error.ArgumentNull(nameof(predicate));
            return SingleOrDefaultImpl(source, predicate);
        }

        public static TSource SingleOrDefault<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, bool> predicate)
            where TEnumerator : IEnumerator<TSource>
        {
            return SingleOrDefaultImpl(source ?? throw Error.ArgumentNull(nameof(source)), new FuncFunc<TSource, bool>(predicate ?? throw Error.ArgumentNull(nameof(predicate))));
        }

        private static TSource SingleOrDefaultImpl<TSource, TEnumerator, TPredicate>(IEnumerable<TSource, TEnumerator> source, TPredicate predicate)
            where TEnumerator : IEnumerator<TSource>
            where TPredicate : IFunctor<TSource, bool>
        {
            using (var e = source.GetEnumerator())
            {
                while (e.MoveNext())
                {
                    var element = e.Current;
                    if (predicate.Invoke(element))
                    {
                        while (e.MoveNext())
                            if (predicate.Invoke(e.Current))
                                throw Error.MoreThanOneMatch();
                        return element;
                    }
                }
                return default(TSource);
            }
        }
        
        #endregion

        #region Skip
        public static SkipEnumerable<TSource, TEnumerator> Skip<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, int count)
            where TEnumerator : IEnumerator<TSource>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (count < 0) throw Error.ArgumentOutOfRange(nameof(count));
            return new SkipEnumerable<TSource, TEnumerator>(source, count);
        }
        #endregion

        #region SkipWhile
        public static SkipWhileEnumerableA<TSource, TEnumerator, TPredicate> SkipWhile<TSource, TEnumerator, TPredicate>(this IEnumerable<TSource, TEnumerator> source, TPredicate predicate)
            where TEnumerator : IEnumerator<TSource>
            where TPredicate : IFunctor<TSource, bool>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (predicate == null) throw Error.ArgumentNull(nameof(predicate));
            return new SkipWhileEnumerableA<TSource, TEnumerator, TPredicate>(source, predicate);
        }

        public static SkipWhileEnumerableA<TSource, TEnumerator, FuncFunc<TSource, bool>> SkipWhile<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, bool> predicate)
            where TEnumerator : IEnumerator<TSource>
        {
            return new SkipWhileEnumerableA<TSource, TEnumerator, FuncFunc<TSource, bool>>(source ?? throw Error.ArgumentNull(nameof(source)),
                new FuncFunc<TSource, bool>(predicate ?? throw Error.ArgumentNull(nameof(predicate))));
        }

        public static SkipWhileEnumerableB<TSource, TEnumerator, TPredicate> SkipWhile<TSource, TEnumerator, TPredicate>(this IEnumerable<TSource, TEnumerator> source, ITFunctor<TPredicate, TSource, int, bool> predicate)
            where TEnumerator : IEnumerator<TSource>
            where TPredicate : IFunctor<TSource, int, bool>
        {
            return new SkipWhileEnumerableB<TSource, TEnumerator, TPredicate>(source ?? throw Error.ArgumentNull(nameof(source)), (predicate ?? throw Error.ArgumentNull(nameof(predicate))).Unwrap());
        }

        public static SkipWhileEnumerableB<TSource, TEnumerator, FuncFunc<TSource, int, bool>> SkipWhile<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, int, bool> predicate)
            where TEnumerator : IEnumerator<TSource>
        {
            return new SkipWhileEnumerableB<TSource, TEnumerator, FuncFunc<TSource, int, bool>>(source ?? throw Error.ArgumentNull(nameof(source)),
                new FuncFunc<TSource, int, bool>(predicate ?? throw Error.ArgumentNull(nameof(predicate))));
        }
        #endregion

        #region Take
        public static TakeEnumerable<TSource, TEnumerator> Take<TSource, TEnumerable, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, int count)
            where TEnumerator : IEnumerator<TSource>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (count < 0) throw Error.ArgumentOutOfRange(nameof(count));
            return new TakeEnumerable<TSource, TEnumerator>(source, count);
        }
        #endregion

        #region TakeWhile
        public static TakeWhileEnumerableA<TSource, TEnumerator, TPredicate> TakeWhile<TSource, TEnumerator, TPredicate>(this IEnumerable<TSource, TEnumerator> source, TPredicate predicate)
            where TEnumerator : IEnumerator<TSource>
            where TPredicate : IFunctor<TSource, bool>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (predicate == null) throw Error.ArgumentNull(nameof(predicate));
            return new TakeWhileEnumerableA<TSource, TEnumerator, TPredicate>(source, predicate);
        }

        public static TakeWhileEnumerableA<TSource, TEnumerator, FuncFunc<TSource, bool>> TakeWhile<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, bool> predicate)
            where TEnumerator : IEnumerator<TSource>
        {
            return new TakeWhileEnumerableA<TSource, TEnumerator, FuncFunc<TSource, bool>>(source ?? throw Error.ArgumentNull(nameof(source)),
                new FuncFunc<TSource, bool>(predicate ?? throw Error.ArgumentNull(nameof(predicate))));
        }

        public static TakeWhileEnumerableB<TSource, TEnumerator, TPredicate> TakeWhile<TSource, TEnumerator, TPredicate>(this IEnumerable<TSource, TEnumerator> source, ITFunctor<TPredicate, TSource, int, bool> predicate)
            where TEnumerator : IEnumerator<TSource>
            where TPredicate : IFunctor<TSource, int, bool>
        {
            return new TakeWhileEnumerableB<TSource, TEnumerator, TPredicate>(source ?? throw Error.ArgumentNull(nameof(source)), (predicate ?? throw Error.ArgumentNull(nameof(predicate))).Unwrap());
        }

        public static TakeWhileEnumerableB<TSource, TEnumerator, FuncFunc<TSource, int, bool>> TakeWhile<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, int, bool> predicate)
            where TEnumerator : IEnumerator<TSource>
        {
            return new TakeWhileEnumerableB<TSource, TEnumerator, FuncFunc<TSource, int, bool>>(source ?? throw Error.ArgumentNull(nameof(source)),
                new FuncFunc<TSource, int, bool>(predicate ?? throw Error.ArgumentNull(nameof(predicate))));
        }
        #endregion

        // ThenBy

        // ThenByDescending

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

        public static Dictionary<TKey, TElement>
            ToDictionary<TSource, TEnumerator, TKey, TElement>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
            where TEnumerator : IEnumerator<TSource>
            => ToDictionary(source, keySelector, elementSelector, null);

        public static Dictionary<TKey, TElement>
            ToDictionary<TSource, TEnumerator, TKey, TElement>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
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
        public static Lookup<TKey, TSource, EquatableEqualityComparer<TKey>>
            ToLookup<TSource, TKey, TEnumerator, TKeySelector>(this IEnumerable<TSource, TEnumerator> source, ITFunctor<TKeySelector, TSource, TKey> keySelector)
            where TKey : IEquatable<TKey>
            where TEnumerator : IEnumerator<TSource>
            where TKeySelector : IFunctor<TSource, TKey>
        {
            return Lookup<TKey, TSource, EquatableEqualityComparer<TKey>>.Create(
                source ?? throw Error.ArgumentNull(nameof(source)), (keySelector ?? throw Error.ArgumentNull(nameof(keySelector))).Unwrap(), new Identity<TSource>(), EqualityComparers.Equatable<TKey>());
        }

        public static Lookup<TKey, TSource, EquatableEqualityComparer<TKey>> ToLookup<TSource, TKey, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey> keySelector)
            where TKey : IEquatable<TKey>
            where TEnumerator : IEnumerator<TSource>
        {
            return Lookup<TKey, TSource, EquatableEqualityComparer<TKey>>.Create(
                source ?? throw Error.ArgumentNull(nameof(source)), new FuncFunc<TSource, TKey>(keySelector ?? throw Error.ArgumentNull(nameof(keySelector))),
                new Identity<TSource>(), EqualityComparers.Equatable<TKey>());
        }

        public static Lookup<TKey, TSource, IEqualityComparer<TKey>>
            ToLookup<TSource, TKey, TEnumerator, TKeySelector>(this IEnumerable<TSource, TEnumerator> source, ITFunctor<TKeySelector, TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
            where TEnumerator : IEnumerator<TSource>
            where TKeySelector : IFunctor<TSource, TKey>
        {
            return Lookup<TKey, TSource, IEqualityComparer<TKey>>.Create(
                source ?? throw Error.ArgumentNull(nameof(source)), (keySelector ?? throw Error.ArgumentNull(nameof(keySelector))).Unwrap(), new Identity<TSource>(), comparer ?? EqualityComparer<TKey>.Default);
        }

        public static Lookup<TKey, TSource, IEqualityComparer<TKey>>
            ToLookup<TSource, TKey, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
            where TEnumerator : IEnumerator<TSource>
        {
            return Lookup<TKey, TSource, IEqualityComparer<TKey>>.Create(
                source ?? throw Error.ArgumentNull(nameof(source)), new FuncFunc<TSource, TKey>(keySelector ?? throw Error.ArgumentNull(nameof(keySelector))),
                new Identity<TSource>(), comparer ?? EqualityComparer<TKey>.Default);
        }

        public static Lookup<TKey, TSource, TEqualityComparer>
            ToLookup<TSource, TKey, TEnumerator, TKeySelector, TEqualityComparer>(this IEnumerable<TSource, TEnumerator> source, ITFunctor<TKeySelector, TSource, TKey> keySelector, TEqualityComparer comparer)
            where TEnumerator : IEnumerator<TSource>
            where TKeySelector : IFunctor<TSource, TKey>
            where TEqualityComparer : struct, IEqualityComparer<TKey>
        {
            return Lookup<TKey, TSource, TEqualityComparer>.Create(
                source ?? throw Error.ArgumentNull(nameof(source)), (keySelector ?? throw Error.ArgumentNull(nameof(keySelector))).Unwrap(), new Identity<TSource>(), comparer);
        }

        public static Lookup<TKey, TSource, TEqualityComparer>
            ToLookup<TSource, TKey, TEnumerator, TEqualityComparer>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey> keySelector, TEqualityComparer comparer)
            where TEnumerator : IEnumerator<TSource>
            where TEqualityComparer : struct, IEqualityComparer<TKey>
        {
            return Lookup<TKey, TSource, TEqualityComparer>.Create(
                source ?? throw Error.ArgumentNull(nameof(source)), new FuncFunc<TSource, TKey>(keySelector ?? throw Error.ArgumentNull(nameof(keySelector))), new Identity<TSource>(), comparer);
        }

        public static Lookup<TKey, TElement, EquatableEqualityComparer<TKey>>
            ToLookup<TSource, TKey, TElement, TEnumerator, TKeySelector, TElementSelector>(this IEnumerable<TSource, TEnumerator> source,
            ITFunctor<TKeySelector, TSource, TKey> keySelector, ITFunctor<TElementSelector, TSource, TElement> elementSelector)
            where TKey : IEquatable<TKey>
            where TEnumerator : IEnumerator<TSource>
            where TKeySelector : IFunctor<TSource, TKey>
            where TElementSelector : IFunctor<TSource, TElement>
        {
            return Lookup<TKey, TElement, EquatableEqualityComparer<TKey>>.Create(
                source ?? throw Error.ArgumentNull(nameof(source)), (keySelector ?? throw Error.ArgumentNull(nameof(keySelector))).Unwrap(),
                (elementSelector ?? throw Error.ArgumentNull(nameof(elementSelector))).Unwrap(), EqualityComparers.Equatable<TKey>());
        }

        public static Lookup<TKey, TElement, EquatableEqualityComparer<TKey>>
            ToLookup<TSource, TKey, TElement, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
            where TKey : IEquatable<TKey>
            where TEnumerator : IEnumerator<TSource>
        {
            return Lookup<TKey, TElement, EquatableEqualityComparer<TKey>>.Create(
                source ?? throw Error.ArgumentNull(nameof(source)), new FuncFunc<TSource, TKey>(keySelector ?? throw Error.ArgumentNull(nameof(keySelector))),
                new FuncFunc<TSource, TElement>(elementSelector ?? throw Error.ArgumentNull(nameof(elementSelector))), EqualityComparers.Equatable<TKey>());
        }

        public static Lookup<TKey, TElement, IEqualityComparer<TKey>>
            ToLookup<TSource, TKey, TElement, TEnumerator, TKeySelector, TElementSelector>(this IEnumerable<TSource, TEnumerator> source,
            ITFunctor<TKeySelector, TSource, TKey> keySelector, ITFunctor<TElementSelector, TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
            where TEnumerator : IEnumerator<TSource>
            where TKeySelector : IFunctor<TSource, TKey>
            where TElementSelector : IFunctor<TSource, TElement>
        {
            return Lookup<TKey, TElement, IEqualityComparer<TKey>>.Create(
                source ?? throw Error.ArgumentNull(nameof(source)), (keySelector ?? throw Error.ArgumentNull(nameof(keySelector))).Unwrap(),
                (elementSelector ?? throw Error.ArgumentNull(nameof(elementSelector))).Unwrap(), comparer ?? EqualityComparer<TKey>.Default);
        }

        public static Lookup<TKey, TElement, IEqualityComparer<TKey>>
            ToLookup<TSource, TKey, TElement, TEnumerator>(this IEnumerable<TSource, TEnumerator> source,
            Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
            where TEnumerator : IEnumerator<TSource>
        {
            return Lookup<TKey, TElement, IEqualityComparer<TKey>>.Create(
                source ?? throw Error.ArgumentNull(nameof(source)), new FuncFunc<TSource, TKey>(keySelector ?? throw Error.ArgumentNull(nameof(keySelector))),
                new FuncFunc<TSource, TElement>(elementSelector ?? throw Error.ArgumentNull(nameof(elementSelector))), comparer ?? EqualityComparer<TKey>.Default);
        }

        public static Lookup<TKey, TElement, TEqualityComparer>
            ToLookup<TSource, TKey, TElement, TEnumerator, TKeySelector, TElementSelector, TEqualityComparer>(this IEnumerable<TSource, TEnumerator> source,
            ITFunctor<TKeySelector, TSource, TKey> keySelector, ITFunctor<TElementSelector, TSource, TElement> elementSelector, TEqualityComparer comparer)
            where TEnumerator : IEnumerator<TSource>
            where TKeySelector : IFunctor<TSource, TKey>
            where TElementSelector : IFunctor<TSource, TElement>
            where TEqualityComparer : struct, IEqualityComparer<TKey>
        {
            return Lookup<TKey, TElement, TEqualityComparer>.Create(
                source ?? throw Error.ArgumentNull(nameof(source)), (keySelector ?? throw Error.ArgumentNull(nameof(keySelector))).Unwrap(),
                (elementSelector ?? throw Error.ArgumentNull(nameof(elementSelector))).Unwrap(), comparer);
        }

        public static Lookup<TKey, TElement, TEqualityComparer>
            ToLookup<TSource, TKey, TElement, TEnumerator, TEqualityComparer>(this IEnumerable<TSource, TEnumerator> source,
            Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, TEqualityComparer comparer)
            where TEnumerator : IEnumerator<TSource>
            where TEqualityComparer : struct, IEqualityComparer<TKey>
        {
            return Lookup<TKey, TElement, TEqualityComparer>.Create(
                source ?? throw Error.ArgumentNull(nameof(source)), new FuncFunc<TSource, TKey>(keySelector ?? throw Error.ArgumentNull(nameof(keySelector))),
                new FuncFunc<TSource, TElement>(elementSelector ?? throw Error.ArgumentNull(nameof(elementSelector))), comparer);
        }

        #region String key optimization
        public static Lookup<string, TSource, Cmp.StringComparer> ToLookup<TSource, TEnumerator, TKeySelector>(this IEnumerable<TSource, TEnumerator> source, ITFunctor<TKeySelector, TSource, string> keySelector)
            where TEnumerator : IEnumerator<TSource>
            where TKeySelector : IFunctor<TSource, string>
        {
            return Lookup<string, TSource, Cmp.StringComparer>.Create(
                source ?? throw Error.ArgumentNull(nameof(source)), (keySelector ?? throw Error.ArgumentNull(nameof(keySelector))).Unwrap(), new Identity<TSource>(), new Cmp.StringComparer());
        }

        public static Lookup<string, TSource, Cmp.StringComparer> ToLookup<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, string> keySelector)
            where TEnumerator : IEnumerator<TSource>
        {
            return Lookup<string, TSource, Cmp.StringComparer>.Create(
                source ?? throw Error.ArgumentNull(nameof(source)), new FuncFunc<TSource, string>(keySelector ?? throw Error.ArgumentNull(nameof(keySelector))), new Identity<TSource>(), new Cmp.StringComparer());
        }

        public static Lookup<string, TElement, Cmp.StringComparer>
            ToLookup<TSource, TElement, TEnumerator, TKeySelector, TElementSelector>(this IEnumerable<TSource, TEnumerator> source,
            ITFunctor<TKeySelector, TSource, string> keySelector, ITFunctor<TElementSelector, TSource, TElement> elementSelector)
            where TEnumerator : IEnumerator<TSource>
            where TKeySelector : IFunctor<TSource, string>
            where TElementSelector : IFunctor<TSource, TElement>
        {
            return Lookup<string, TElement, Cmp.StringComparer>.Create(
                source ?? throw Error.ArgumentNull(nameof(source)), (keySelector ?? throw Error.ArgumentNull(nameof(keySelector))).Unwrap(),
                (elementSelector ?? throw Error.ArgumentNull(nameof(elementSelector))).Unwrap(), new Cmp.StringComparer());
        }

        public static Lookup<string, TElement, Cmp.StringComparer>
            ToLookup<TSource, TElement, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, string> keySelector, Func<TSource, TElement> elementSelector)
            where TEnumerator : IEnumerator<TSource>
        {
            return Lookup<string, TElement, Cmp.StringComparer>.Create(
                source ?? throw Error.ArgumentNull(nameof(source)), new FuncFunc<TSource, string>(keySelector ?? throw Error.ArgumentNull(nameof(keySelector))),
                new FuncFunc<TSource, TElement>(elementSelector ?? throw Error.ArgumentNull(nameof(elementSelector))), new Cmp.StringComparer());
        }
        #endregion

        #region Nullable key optimization
        public static Lookup<TKey?, TSource, NullableEqualityComparer<TKey, EquatableEqualityComparer<TKey>>>
            ToLookup<TSource, TKey, TEnumerator, TKeySelector>(this IEnumerable<TSource, TEnumerator> source, ITFunctor<TKeySelector, TSource, TKey?> keySelector)
            where TKey : struct, IEquatable<TKey>
            where TEnumerator : IEnumerator<TSource>
            where TKeySelector : IFunctor<TSource, TKey?>
        {
            return Lookup<TKey?, TSource, NullableEqualityComparer<TKey, EquatableEqualityComparer<TKey>>>.Create(
                source ?? throw Error.ArgumentNull(nameof(source)), (keySelector ?? throw Error.ArgumentNull(nameof(keySelector))).Unwrap(), new Identity<TSource>(), EqualityComparers.Nullable<TKey>());
        }

        public static Lookup<TKey?, TSource, NullableEqualityComparer<TKey, EquatableEqualityComparer<TKey>>>
            ToLookup<TSource, TKey, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey?> keySelector)
            where TKey : struct, IEquatable<TKey>
            where TEnumerator : IEnumerator<TSource>
        {
            return Lookup<TKey?, TSource, NullableEqualityComparer<TKey, EquatableEqualityComparer<TKey>>>.Create(
                source ?? throw Error.ArgumentNull(nameof(source)), new FuncFunc<TSource, TKey?>(keySelector ?? throw Error.ArgumentNull(nameof(keySelector))),
                new Identity<TSource>(), EqualityComparers.Nullable<TKey>());
        }

        public static Lookup<TKey?, TElement, NullableEqualityComparer<TKey, EquatableEqualityComparer<TKey>>>
            ToLookup<TSource, TKey, TElement, TEnumerator, TKeySelector, TElementSelector>(this IEnumerable<TSource, TEnumerator> source,
            ITFunctor<TKeySelector, TSource, TKey?> keySelector, ITFunctor<TElementSelector, TSource, TElement> elementSelector)
            where TKey : struct, IEquatable<TKey>
            where TEnumerator : IEnumerator<TSource>
            where TKeySelector : IFunctor<TSource, TKey?>
            where TElementSelector : IFunctor<TSource, TElement>
        {
            return Lookup<TKey?, TElement, NullableEqualityComparer<TKey, EquatableEqualityComparer<TKey>>>.Create(
                source ?? throw Error.ArgumentNull(nameof(source)), (keySelector ?? throw Error.ArgumentNull(nameof(keySelector))).Unwrap(),
                (elementSelector ?? throw Error.ArgumentNull(nameof(elementSelector))).Unwrap(), EqualityComparers.Nullable<TKey>());
        }

        public static Lookup<TKey?, TElement, NullableEqualityComparer<TKey, EquatableEqualityComparer<TKey>>>
            ToLookup<TSource, TKey, TElement, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey?> keySelector, Func<TSource, TElement> elementSelector)
            where TKey : struct, IEquatable<TKey>
            where TEnumerator : IEnumerator<TSource>
        {
            return Lookup<TKey?, TElement, NullableEqualityComparer<TKey, EquatableEqualityComparer<TKey>>>.Create(
                source ?? throw Error.ArgumentNull(nameof(source)), new FuncFunc<TSource, TKey?>(keySelector ?? throw Error.ArgumentNull(nameof(keySelector))),
                new FuncFunc<TSource, TElement>(elementSelector ?? throw Error.ArgumentNull(nameof(elementSelector))), EqualityComparers.Nullable<TKey>());
        }
        #endregion
        #endregion

        #region Where
        public static WhereEnumerable<TSource, TEnumerator, TPredicate> Where<TSource, TEnumerator, TPredicate>(this IEnumerable<TSource, TEnumerator> source, TPredicate predicate)
            where TEnumerator : IEnumerator<TSource>
            where TPredicate : IFunctor<TSource, bool>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (predicate == null) throw Error.ArgumentNull(nameof(predicate));
            return new WhereEnumerable<TSource, TEnumerator, TPredicate>(source, predicate);
        }

        public static WhereEnumerable<TSource, TEnumerator, FuncFunc<TSource, bool>> Where<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, bool> predicate)
            where TEnumerator : IEnumerator<TSource>
        {
            return new WhereEnumerable<TSource, TEnumerator, FuncFunc<TSource, bool>>(
                source ?? throw Error.ArgumentNull(nameof(source)), new FuncFunc<TSource, bool>(predicate ?? throw Error.ArgumentNull(nameof(predicate))));
        }
        #endregion

        #region Zip
        public static ZipEnumerable<TFirst, TSecond, TResult, TEnumerator1, TEnumerator2, TResultSelector>
            Zip<TFirst, TSecond, TResult, TEnumerator1, TEnumerator2, TResultSelector>(this IEnumerable<TFirst, TEnumerator1> first,
            IEnumerable<TSecond, TEnumerator2> second, ITFunctor<TResultSelector, TFirst, TSecond, TResult> resultSelector)
            where TEnumerator1 : IEnumerator<TFirst>
            where TEnumerator2 : IEnumerator<TSecond>
            where TResultSelector : IFunctor<TFirst, TSecond, TResult>
        {
            return new ZipEnumerable<TFirst, TSecond, TResult, TEnumerator1, TEnumerator2, TResultSelector>(
                first ?? throw Error.ArgumentNull(nameof(first)), second ?? throw Error.ArgumentNull(nameof(second)),
                (resultSelector ?? throw Error.ArgumentNull(nameof(resultSelector))).Unwrap());
        }

        public static ZipEnumerable<TFirst, TSecond, TResult, TEnumerator1, TEnumerator2, FuncFunc<TFirst, TSecond, TResult>>
            Zip<TFirst, TSecond, TResult, TEnumerator1, TEnumerator2>(this IEnumerable<TFirst, TEnumerator1> first,
            IEnumerable<TSecond, TEnumerator2> second, Func<TFirst, TSecond, TResult> resultSelector)
            where TEnumerator1 : IEnumerator<TFirst>
            where TEnumerator2 : IEnumerator<TSecond>
        {
            return new ZipEnumerable<TFirst, TSecond, TResult, TEnumerator1, TEnumerator2, FuncFunc<TFirst, TSecond, TResult>>(
                first ?? throw Error.ArgumentNull(nameof(first)), second ?? throw Error.ArgumentNull(nameof(second)),
                new FuncFunc<TFirst, TSecond, TResult>(resultSelector ?? throw Error.ArgumentNull(nameof(resultSelector))));
        }
        #endregion
    }
}
