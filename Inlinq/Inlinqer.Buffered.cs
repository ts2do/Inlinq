using Inlinq.Cmp;
using Inlinq.Impl;
using System;
using System.Collections.Generic;

namespace Inlinq
{
    public static partial class Inlinqer
    {
        #region Distinct
        public static WhereEnumerable<TSource, TEnumerator> Distinct<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source)
            where TSource : IEquatable<TSource>
            where TEnumerator : IEnumerator<TSource>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            return DistinctImpl(source);
        }

        private static WhereEnumerable<TSource, TEnumerator> DistinctImpl<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source)
            where TSource : IEquatable<TSource>
            where TEnumerator : IEnumerator<TSource>
        {
            var set = new Set<TSource, EquatableEqualityComparer<TSource>>(new EquatableEqualityComparer<TSource>());
            return new WhereEnumerable<TSource, TEnumerator>(source, set.Add);
        }

        public static WhereEnumerable<TSource, TEnumerator> Distinct<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, IEqualityComparer<TSource> comparer)
            where TEnumerator : IEnumerator<TSource>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            return DistinctImpl(source, comparer);
        }

        private static WhereEnumerable<TSource, TEnumerator> DistinctImpl<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, IEqualityComparer<TSource> comparer)
            where TEnumerator : IEnumerator<TSource>
        {
            var set = new Set<TSource, IEqualityComparer<TSource>>(comparer ?? EqualityComparer<TSource>.Default);
            return new WhereEnumerable<TSource, TEnumerator>(source, set.Add);
        }

        public static WhereEnumerable<TSource, TEnumerator> Distinct<TSource, TEnumerator, TEqualityComparer>(this IEnumerable<TSource, TEnumerator> source, TEqualityComparer comparer)
            where TEnumerator : IEnumerator<TSource>
            where TEqualityComparer : struct, IEqualityComparer<TSource>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            return DistinctImpl(source, comparer);
        }

        private static WhereEnumerable<TSource, TEnumerator> DistinctImpl<TSource, TEnumerator, TEqualityComparer>(this IEnumerable<TSource, TEnumerator> source, TEqualityComparer comparer)
            where TEnumerator : IEnumerator<TSource>
            where TEqualityComparer : struct, IEqualityComparer<TSource>
        {
            var set = new Set<TSource, TEqualityComparer>(comparer);
            return new WhereEnumerable<TSource, TEnumerator>(source, set.Add);
        }

        public static WhereEnumerable<string, TEnumerator> Distinct<TEnumerator>(this IEnumerable<string, TEnumerator> source)
            where TEnumerator : IEnumerator<string>
            => DistinctImpl(source, new Cmp.StringComparer());

        public static WhereEnumerable<TSource?, TEnumerator>
            Distinct<TSource, TEnumerator>(this IEnumerable<TSource?, TEnumerator> source)
            where TSource : struct, IEquatable<TSource>
            where TEnumerator : IEnumerator<TSource?>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            return DistinctImpl(source);
        }

        public static WhereEnumerable<TSource?, TEnumerator> DistinctImpl<TSource, TEnumerator>(this IEnumerable<TSource?, TEnumerator> source)
            where TSource : struct, IEquatable<TSource>
            where TEnumerator : IEnumerator<TSource?>
            => DistinctImpl(source, new NullableEqualityComparer<TSource, EquatableEqualityComparer<TSource>>(new EquatableEqualityComparer<TSource>()));
        #endregion

        #region Except
        public static ExceptEnumerable<TSource, TEnumerator1, TEnumerator2, EquatableEqualityComparer<TSource>>
            Except<TSource, TEnumerator1, TEnumerator2>(this IEnumerable<TSource, TEnumerator1> first, IEnumerable<TSource, TEnumerator2> second)
            where TSource : IEquatable<TSource>
            where TEnumerator1 : IEnumerator<TSource>
            where TEnumerator2 : IEnumerator<TSource>
        {
            return new ExceptEnumerable<TSource, TEnumerator1, TEnumerator2, EquatableEqualityComparer<TSource>>(
                first ?? throw Error.ArgumentNull(nameof(first)), second ?? throw Error.ArgumentNull(nameof(second)), new EquatableEqualityComparer<TSource>());
        }

        public static ExceptEnumerable<TSource, TEnumerator1, TEnumerator2, IEqualityComparer<TSource>>
            Except<TSource, TEnumerator1, TEnumerator2>(this IEnumerable<TSource, TEnumerator1> first, IEnumerable<TSource, TEnumerator2> second, IEqualityComparer<TSource> comparer)
            where TEnumerator1 : IEnumerator<TSource>
            where TEnumerator2 : IEnumerator<TSource>
        {
            return new ExceptEnumerable<TSource, TEnumerator1, TEnumerator2, IEqualityComparer<TSource>>(
                first ?? throw Error.ArgumentNull(nameof(first)), second ?? throw Error.ArgumentNull(nameof(second)), comparer ?? EqualityComparer<TSource>.Default);
        }

        public static ExceptEnumerable<TSource, TEnumerator1, TEnumerator2, TEqualityComparer>
            Except<TSource, TEnumerator1, TEnumerator2, TEqualityComparer>(this IEnumerable<TSource, TEnumerator1> first, IEnumerable<TSource, TEnumerator2> second, TEqualityComparer comparer)
            where TEnumerator1 : IEnumerator<TSource>
            where TEnumerator2 : IEnumerator<TSource>
            where TEqualityComparer : struct, IEqualityComparer<TSource>
        {
            return new ExceptEnumerable<TSource, TEnumerator1, TEnumerator2, TEqualityComparer>(
                first ?? throw Error.ArgumentNull(nameof(first)), second ?? throw Error.ArgumentNull(nameof(second)), comparer);
        }

        public static ExceptEnumerable<string, TEnumerator1, TEnumerator2, Cmp.StringComparer>
            Except<TEnumerator1, TEnumerator2>(this IEnumerable<string, TEnumerator1> first, IEnumerable<string, TEnumerator2> second)
            where TEnumerator1 : IEnumerator<string>
            where TEnumerator2 : IEnumerator<string>
        {
            return new ExceptEnumerable<string, TEnumerator1, TEnumerator2, Cmp.StringComparer>(
                first ?? throw Error.ArgumentNull(nameof(first)), second ?? throw Error.ArgumentNull(nameof(second)), new Cmp.StringComparer());
        }

        public static ExceptEnumerable<TSource?, TEnumerator1, TEnumerator2, NullableEqualityComparer<TSource, EquatableEqualityComparer<TSource>>>
            Except<TSource, TEnumerator1, TEnumerator2>(this IEnumerable<TSource?, TEnumerator1> first, IEnumerable<TSource?, TEnumerator2> second)
            where TSource : struct, IEquatable<TSource>
            where TEnumerator1 : IEnumerator<TSource?>
            where TEnumerator2 : IEnumerator<TSource?>
        {
            return new ExceptEnumerable<TSource?, TEnumerator1, TEnumerator2, NullableEqualityComparer<TSource, EquatableEqualityComparer<TSource>>>(
                first ?? throw Error.ArgumentNull(nameof(first)), second ?? throw Error.ArgumentNull(nameof(second)),
                new NullableEqualityComparer<TSource, EquatableEqualityComparer<TSource>>(new EquatableEqualityComparer<TSource>()));
        }
        #endregion

        #region GroupBy
        public static GroupByEnumerable<TSource, TEnumerator, TKey, TSource, EquatableEqualityComparer<TKey>>
            GroupBy<TSource, TEnumerator, TKey>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey> keySelector)
            where TKey : IEquatable<TKey>
            where TEnumerator : IEnumerator<TSource>
        {
            return new GroupByEnumerable<TSource, TEnumerator, TKey, TSource, EquatableEqualityComparer<TKey>>(
                source ?? throw Error.ArgumentNull(nameof(source)), keySelector ?? throw Error.ArgumentNull(nameof(keySelector)),
                Util.Identity<TSource>(), new EquatableEqualityComparer<TKey>());
        }

        public static GroupByEnumerable<TSource, TEnumerator, TKey, TSource, IEqualityComparer<TKey>>
            GroupBy<TSource, TEnumerator, TKey>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
            where TEnumerator : IEnumerator<TSource>
        {
            return new GroupByEnumerable<TSource, TEnumerator, TKey, TSource, IEqualityComparer<TKey>>(
                source ?? throw Error.ArgumentNull(nameof(source)), keySelector ?? throw Error.ArgumentNull(nameof(keySelector)),
                Util.Identity<TSource>(), comparer ?? EqualityComparer<TKey>.Default);
        }

        public static GroupByEnumerable<TSource, TEnumerator, TKey, TSource, IEqualityComparer<TKey>>
            GroupBy<TSource, TEnumerator, TKey, TEqualityComparer>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey> keySelector, TEqualityComparer comparer)
            where TEnumerator : IEnumerator<TSource>
            where TEqualityComparer : struct, IEqualityComparer<TKey>
        {
            return new GroupByEnumerable<TSource, TEnumerator, TKey, TSource, IEqualityComparer<TKey>>(
                source ?? throw Error.ArgumentNull(nameof(source)), keySelector ?? throw Error.ArgumentNull(nameof(keySelector)), Util.Identity<TSource>(), comparer);
        }

        public static GroupByEnumerable<TSource, TEnumerator, TKey, TElement, EquatableEqualityComparer<TKey>>
            GroupBy<TSource, TEnumerator, TKey, TElement>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
            where TKey : IEquatable<TKey>
            where TEnumerator : IEnumerator<TSource>
        {
            return new GroupByEnumerable<TSource, TEnumerator, TKey, TElement, EquatableEqualityComparer<TKey>>(
                source ?? throw Error.ArgumentNull(nameof(source)), keySelector ?? throw Error.ArgumentNull(nameof(keySelector)),
                elementSelector ?? throw Error.ArgumentNull(nameof(elementSelector)), new EquatableEqualityComparer<TKey>());
        }

        public static GroupByEnumerable<TSource, TEnumerator, TKey, TElement, IEqualityComparer<TKey>>
            GroupBy<TSource, TEnumerator, TKey, TElement>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
            where TEnumerator : IEnumerator<TSource>
        {
            return new GroupByEnumerable<TSource, TEnumerator, TKey, TElement, IEqualityComparer<TKey>>(
                source ?? throw Error.ArgumentNull(nameof(source)), keySelector ?? throw Error.ArgumentNull(nameof(keySelector)),
                elementSelector ?? throw Error.ArgumentNull(nameof(elementSelector)), comparer ?? EqualityComparer<TKey>.Default);
        }

        public static GroupByEnumerable<TSource, TEnumerator, TKey, TElement, TEqualityComparer>
            GroupBy<TSource, TEnumerator, TKey, TElement, TEqualityComparer>(this IEnumerable<TSource, TEnumerator> source,
            Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, TEqualityComparer comparer)
            where TEnumerator : IEnumerator<TSource>
            where TEqualityComparer : struct, IEqualityComparer<TKey>
        {
            return new GroupByEnumerable<TSource, TEnumerator, TKey, TElement, TEqualityComparer>(
                source ?? throw Error.ArgumentNull(nameof(source)), keySelector ?? throw Error.ArgumentNull(nameof(keySelector)),
                elementSelector ?? throw Error.ArgumentNull(nameof(elementSelector)), comparer);
        }

        public static SelectEnumerable<Grouping<TKey, TSource>, TResult, GroupingEnumerator<TKey, TSource>>
            GroupBy<TSource, TEnumerator, TKey, TResult>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey> keySelector, Func<Grouping<TKey, TSource>, TResult> resultSelector)
            where TKey : IEquatable<TKey>
            where TEnumerator : IEnumerator<TSource>
        {
            return new SelectEnumerable<Grouping<TKey, TSource>, TResult, GroupingEnumerator<TKey, TSource>>(
                new GroupByEnumerable<TSource, TEnumerator, TKey, TSource, EquatableEqualityComparer<TKey>>(
                    source ?? throw Error.ArgumentNull(nameof(source)), keySelector ?? throw Error.ArgumentNull(nameof(keySelector)),
                    Util.Identity<TSource>(), new EquatableEqualityComparer<TKey>()),
                resultSelector ?? throw Error.ArgumentNull(nameof(resultSelector)));
        }

        public static SelectEnumerable<Grouping<TKey, TSource>, TResult, GroupingEnumerator<TKey, TSource>>
            GroupBy<TSource, TEnumerator, TKey, TResult>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey> keySelector,
            Func<Grouping<TKey, TSource>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
            where TEnumerator : IEnumerator<TSource>
        {
            return new SelectEnumerable<Grouping<TKey, TSource>, TResult, GroupingEnumerator<TKey, TSource>>(
                new GroupByEnumerable<TSource, TEnumerator, TKey, TSource, IEqualityComparer<TKey>>(
                    source ?? throw Error.ArgumentNull(nameof(source)), keySelector ?? throw Error.ArgumentNull(nameof(keySelector)),
                    Util.Identity<TSource>(), comparer ?? EqualityComparer<TKey>.Default),
                resultSelector ?? throw Error.ArgumentNull(nameof(resultSelector)));
        }

        public static SelectEnumerable<Grouping<TKey, TSource>, TResult, GroupingEnumerator<TKey, TSource>>
            GroupBy<TSource, TEnumerator, TKey, TResult, TEqualityComparer>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey> keySelector,
            Func<Grouping<TKey, TSource>, TResult> resultSelector, TEqualityComparer comparer)
            where TEnumerator : IEnumerator<TSource>
            where TEqualityComparer : struct, IEqualityComparer<TKey>
        {
            return new SelectEnumerable<Grouping<TKey, TSource>, TResult, GroupingEnumerator<TKey, TSource>>(
                new GroupByEnumerable<TSource, TEnumerator, TKey, TSource, IEqualityComparer<TKey>>(
                    source ?? throw Error.ArgumentNull(nameof(source)), keySelector ?? throw Error.ArgumentNull(nameof(keySelector)),
                    Util.Identity<TSource>(), comparer),
                resultSelector ?? throw Error.ArgumentNull(nameof(resultSelector)));
        }

        public static SelectEnumerable<Grouping<TKey, TElement>, TResult, GroupingEnumerator<TKey, TElement>>
            GroupBy<TSource, TEnumerator, TKey, TElement, TResult>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey> keySelector,
            Func<TSource, TElement> elementSelector, Func<Grouping<TKey, TElement>, TResult> resultSelector)
            where TKey : IEquatable<TKey>
            where TEnumerator : IEnumerator<TSource>
        {
            return new SelectEnumerable<Grouping<TKey, TElement>, TResult, GroupingEnumerator<TKey, TElement>>(
                new GroupByEnumerable<TSource, TEnumerator, TKey, TElement, EquatableEqualityComparer<TKey>>(
                    source ?? throw Error.ArgumentNull(nameof(source)), keySelector ?? throw Error.ArgumentNull(nameof(keySelector)),
                    elementSelector ?? throw Error.ArgumentNull(nameof(elementSelector)), new EquatableEqualityComparer<TKey>()),
                resultSelector ?? throw Error.ArgumentNull(nameof(resultSelector)));
        }

        public static SelectEnumerable<Grouping<TKey, TElement>, TResult, GroupingEnumerator<TKey, TElement>>
            GroupBy<TSource, TEnumerator, TKey, TElement, TResult>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey> keySelector,
            Func<TSource, TElement> elementSelector, Func<Grouping<TKey, TElement>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
            where TEnumerator : IEnumerator<TSource>
        {
            return new SelectEnumerable<Grouping<TKey, TElement>, TResult, GroupingEnumerator<TKey, TElement>>(
                new GroupByEnumerable<TSource, TEnumerator, TKey, TElement, IEqualityComparer<TKey>>(
                    source ?? throw Error.ArgumentNull(nameof(source)), keySelector ?? throw Error.ArgumentNull(nameof(keySelector)),
                    elementSelector ?? throw Error.ArgumentNull(nameof(elementSelector)), comparer ?? EqualityComparer<TKey>.Default),
                resultSelector ?? throw Error.ArgumentNull(nameof(resultSelector)));
        }

        public static SelectEnumerable<Grouping<TKey, TElement>, TResult, GroupingEnumerator<TKey, TElement>>
            GroupBy<TSource, TEnumerator, TKey, TElement, TResult, TEqualityComparer>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey> keySelector,
            Func<TSource, TElement> elementSelector, Func<Grouping<TKey, TElement>, TResult> resultSelector, TEqualityComparer comparer)
            where TEnumerator : IEnumerator<TSource>
            where TEqualityComparer : struct, IEqualityComparer<TKey>
        {
            return new SelectEnumerable<Grouping<TKey, TElement>, TResult, GroupingEnumerator<TKey, TElement>>(
                new GroupByEnumerable<TSource, TEnumerator, TKey, TElement, TEqualityComparer>(
                    source ?? throw Error.ArgumentNull(nameof(source)), keySelector ?? throw Error.ArgumentNull(nameof(keySelector)),
                    elementSelector ?? throw Error.ArgumentNull(nameof(elementSelector)), comparer),
                resultSelector ?? throw Error.ArgumentNull(nameof(resultSelector)));
        }
        #endregion

        #region GroupJoin
        public static GroupJoinEnumerable<TOuter, TOuterEnumerator, TInner, TInnerEnumerator, TKey, TResult, EquatableEqualityComparer<TKey>>
            GroupJoin<TOuter, TOuterEnumerator, TInner, TInnerEnumerator, TKey, TResult>(this IEnumerable<TOuter, TOuterEnumerator> outer, IEnumerable<TInner, TInnerEnumerator> inner,
            Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, InlinqArray<TInner>, TResult> resultSelector)
            where TOuterEnumerator : IEnumerator<TOuter>
            where TInnerEnumerator : IEnumerator<TInner>
            where TKey : IEquatable<TKey>
        {
            return new GroupJoinEnumerable<TOuter, TOuterEnumerator, TInner, TInnerEnumerator, TKey, TResult, EquatableEqualityComparer<TKey>>(
                outer ?? throw Error.ArgumentNull(nameof(outer)), inner ?? throw Error.ArgumentNull(nameof(inner)), outerKeySelector ?? throw Error.ArgumentNull(nameof(outerKeySelector)),
                innerKeySelector ?? throw Error.ArgumentNull(nameof(innerKeySelector)),
                resultSelector ?? throw Error.ArgumentNull(nameof(resultSelector)), new EquatableEqualityComparer<TKey>());
        }

        public static GroupJoinEnumerable<TOuter, TOuterEnumerator, TInner, TInnerEnumerator, TKey, TResult, IEqualityComparer<TKey>>
            GroupJoin<TOuter, TOuterEnumerator, TInner, TInnerEnumerator, TKey, TResult>(this IEnumerable<TOuter, TOuterEnumerator> outer,
            IEnumerable<TInner, TInnerEnumerator> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector,
            Func<TOuter, InlinqArray<TInner>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
            where TOuterEnumerator : IEnumerator<TOuter>
            where TInnerEnumerator : IEnumerator<TInner>
        {
            return new GroupJoinEnumerable<TOuter, TOuterEnumerator, TInner, TInnerEnumerator, TKey, TResult, IEqualityComparer<TKey>>(
                outer ?? throw Error.ArgumentNull(nameof(outer)), inner ?? throw Error.ArgumentNull(nameof(inner)), outerKeySelector ?? throw Error.ArgumentNull(nameof(outerKeySelector)),
                innerKeySelector ?? throw Error.ArgumentNull(nameof(innerKeySelector)),
                resultSelector ?? throw Error.ArgumentNull(nameof(resultSelector)), comparer ?? EqualityComparer<TKey>.Default);
        }

        public static GroupJoinEnumerable<TOuter, TOuterEnumerator, TInner, TInnerEnumerator, TKey, TResult, TEqualityComparer>
            GroupJoin<TOuter, TOuterEnumerator, TInner, TInnerEnumerator, TKey, TResult, TEqualityComparer>(this IEnumerable<TOuter, TOuterEnumerator> outer,
            IEnumerable<TInner, TInnerEnumerator> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector,
            Func<TOuter, InlinqArray<TInner>, TResult> resultSelector, TEqualityComparer comparer)
            where TOuterEnumerator : IEnumerator<TOuter>
            where TInnerEnumerator : IEnumerator<TInner>
            where TEqualityComparer : struct, IEqualityComparer<TKey>
        {
            return new GroupJoinEnumerable<TOuter, TOuterEnumerator, TInner, TInnerEnumerator, TKey, TResult, TEqualityComparer>(
                outer ?? throw Error.ArgumentNull(nameof(outer)), inner ?? throw Error.ArgumentNull(nameof(inner)), outerKeySelector ?? throw Error.ArgumentNull(nameof(outerKeySelector)),
                innerKeySelector ?? throw Error.ArgumentNull(nameof(innerKeySelector)),
                resultSelector ?? throw Error.ArgumentNull(nameof(resultSelector)), comparer);
        }
        #endregion

        #region Intersect
        public static IntersectEnumerable<TSource, TEnumerator1, TEnumerator2, EquatableEqualityComparer<TSource>>
            Intersect<TSource, TEnumerator1, TEnumerator2>(this IEnumerable<TSource, TEnumerator1> first, IEnumerable<TSource, TEnumerator2> second)
            where TSource : IEquatable<TSource>
            where TEnumerator1 : IEnumerator<TSource>
            where TEnumerator2 : IEnumerator<TSource>
        {
            return new IntersectEnumerable<TSource, TEnumerator1, TEnumerator2, EquatableEqualityComparer<TSource>>(
                first ?? throw Error.ArgumentNull(nameof(first)), second ?? throw Error.ArgumentNull(nameof(second)), new EquatableEqualityComparer<TSource>());
        }

        public static IntersectEnumerable<TSource, TEnumerator1, TEnumerator2, IEqualityComparer<TSource>>
            Intersect<TSource, TEnumerator1, TEnumerator2>(this IEnumerable<TSource, TEnumerator1> first, IEnumerable<TSource, TEnumerator2> second, IEqualityComparer<TSource> comparer)
            where TEnumerator1 : IEnumerator<TSource>
            where TEnumerator2 : IEnumerator<TSource>
        {
            return new IntersectEnumerable<TSource, TEnumerator1, TEnumerator2, IEqualityComparer<TSource>>(
                first ?? throw Error.ArgumentNull(nameof(first)), second ?? throw Error.ArgumentNull(nameof(second)), comparer ?? EqualityComparer<TSource>.Default);
        }

        public static IntersectEnumerable<TSource, TEnumerator1, TEnumerator2, TEqualityComparer>
            Intersect<TSource, TEnumerator1, TEnumerator2, TEqualityComparer>(this IEnumerable<TSource, TEnumerator1> first, IEnumerable<TSource, TEnumerator2> second, TEqualityComparer comparer)
            where TEnumerator1 : IEnumerator<TSource>
            where TEnumerator2 : IEnumerator<TSource>
            where TEqualityComparer : struct, IEqualityComparer<TSource>
        {
            return new IntersectEnumerable<TSource, TEnumerator1, TEnumerator2, TEqualityComparer>(first ?? throw Error.ArgumentNull(nameof(first)), second ?? throw Error.ArgumentNull(nameof(second)), comparer);
        }

        public static IntersectEnumerable<string, TEnumerator1, TEnumerator2, Cmp.StringComparer>
            Intersect<TEnumerator1, TEnumerator2>(this IEnumerable<string, TEnumerator1> first, IEnumerable<string, TEnumerator2> second)
            where TEnumerator1 : IEnumerator<string>
            where TEnumerator2 : IEnumerator<string>
        {
            return new IntersectEnumerable<string, TEnumerator1, TEnumerator2, Cmp.StringComparer>(
                first ?? throw Error.ArgumentNull(nameof(first)), second ?? throw Error.ArgumentNull(nameof(second)), new Cmp.StringComparer());
        }

        public static IntersectEnumerable<TSource?, TEnumerator1, TEnumerator2, NullableEqualityComparer<TSource, EquatableEqualityComparer<TSource>>>
            Intersect<TSource, TEnumerator1, TEnumerator2>(this IEnumerable<TSource?, TEnumerator1> first, IEnumerable<TSource?, TEnumerator2> second)
            where TSource : struct, IEquatable<TSource>
            where TEnumerator1 : IEnumerator<TSource?>
            where TEnumerator2 : IEnumerator<TSource?>
        {
            return new IntersectEnumerable<TSource?, TEnumerator1, TEnumerator2, NullableEqualityComparer<TSource, EquatableEqualityComparer<TSource>>>(
                first ?? throw Error.ArgumentNull(nameof(first)), second ?? throw Error.ArgumentNull(nameof(second)),
                new NullableEqualityComparer<TSource, EquatableEqualityComparer<TSource>>(new EquatableEqualityComparer<TSource>()));
        }
        #endregion

        #region Reverse
        public static ReverseEnumerable<TSource, TEnumerator> Reverse<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source)
            where TEnumerator : IEnumerator<TSource>
            => new ReverseEnumerable<TSource, TEnumerator>(source ?? throw Error.ArgumentNull(nameof(source)));
        #endregion

        #region Union
        public static WhereEnumerable<TSource, ConcatEnumerator<TSource, TEnumerator1, TEnumerator2>>
            Union<TSource, TEnumerator1, TEnumerator2>(this IEnumerable<TSource, TEnumerator1> first, IEnumerable<TSource, TEnumerator2> second)
            where TSource : IEquatable<TSource>
            where TEnumerator1 : IEnumerator<TSource>
            where TEnumerator2 : IEnumerator<TSource>
            => DistinctImpl(new ConcatEnumerable<TSource, TEnumerator1, TEnumerator2>(first ?? throw Error.ArgumentNull(nameof(first)), second ?? throw Error.ArgumentNull(nameof(second))));

        public static WhereEnumerable<TSource, ConcatEnumerator<TSource, TEnumerator1, TEnumerator2>>
            Union<TSource, TEnumerator1, TEnumerator2>(this IEnumerable<TSource, TEnumerator1> first, IEnumerable<TSource, TEnumerator2> second, IEqualityComparer<TSource> comparer)
            where TEnumerator1 : IEnumerator<TSource>
            where TEnumerator2 : IEnumerator<TSource>
            => DistinctImpl(new ConcatEnumerable<TSource, TEnumerator1, TEnumerator2>(first ?? throw Error.ArgumentNull(nameof(first)), second ?? throw Error.ArgumentNull(nameof(second))), comparer);

        public static WhereEnumerable<TSource, ConcatEnumerator<TSource, TEnumerator1, TEnumerator2>>
            Union<TSource, TEnumerator1, TEnumerator2, TEqualityComparer>(this IEnumerable<TSource, TEnumerator1> first, IEnumerable<TSource, TEnumerator2> second, TEqualityComparer comparer)
            where TEnumerator1 : IEnumerator<TSource>
            where TEnumerator2 : IEnumerator<TSource>
            where TEqualityComparer : struct, IEqualityComparer<TSource>
            => DistinctImpl(new ConcatEnumerable<TSource, TEnumerator1, TEnumerator2>(first ?? throw Error.ArgumentNull(nameof(first)), second ?? throw Error.ArgumentNull(nameof(second))), comparer);

        public static WhereEnumerable<string, ConcatEnumerator<string, TEnumerator1, TEnumerator2>>
            Union<TEnumerator1, TEnumerator2>(this IEnumerable<string, TEnumerator1> first, IEnumerable<string, TEnumerator2> second)
            where TEnumerator1 : IEnumerator<string>
            where TEnumerator2 : IEnumerator<string>
            => DistinctImpl(new ConcatEnumerable<string, TEnumerator1, TEnumerator2>(first ?? throw Error.ArgumentNull(nameof(first)), second ?? throw Error.ArgumentNull(nameof(second))));

        public static WhereEnumerable<TSource?, ConcatEnumerator<TSource?, TEnumerator1, TEnumerator2>>
            Union<TSource, TEnumerator1, TEnumerator2>(this IEnumerable<TSource?, TEnumerator1> first, IEnumerable<TSource?, TEnumerator2> second)
            where TSource : struct, IEquatable<TSource>
            where TEnumerator1 : IEnumerator<TSource?>
            where TEnumerator2 : IEnumerator<TSource?>
            => DistinctImpl(new ConcatEnumerable<TSource?, TEnumerator1, TEnumerator2>(first ?? throw Error.ArgumentNull(nameof(first)), second ?? throw Error.ArgumentNull(nameof(second))));
        #endregion
    }
}
