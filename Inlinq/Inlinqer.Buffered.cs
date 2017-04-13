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
            => DistinctImpl(source ?? throw Error.ArgumentNull(nameof(source)));

        public static WhereEnumerable<TSource, TEnumerator> Distinct<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, IEqualityComparer<TSource> comparer)
            where TEnumerator : IEnumerator<TSource>
            => DistinctImpl(source ?? throw Error.ArgumentNull(nameof(source)), comparer ?? EqualityComparer<TSource>.Default);

        public static WhereEnumerable<TSource, TEnumerator> Distinct<TSource, TEnumerator, TEqualityComparer>(this IEnumerable<TSource, TEnumerator> source, TEqualityComparer comparer)
            where TEnumerator : IEnumerator<TSource>
            where TEqualityComparer : struct, IEqualityComparer<TSource>
            => DistinctImpl(source ?? throw Error.ArgumentNull(nameof(source)), comparer);

        #region String optimization
        public static WhereEnumerable<string, TEnumerator> Distinct<TEnumerator>(this IEnumerable<string, TEnumerator> source)
            where TEnumerator : IEnumerator<string>
            => DistinctImpl(source, new Cmp.StringComparer());
        #endregion

        #region Nullable optimization
        public static WhereEnumerable<TSource?, TEnumerator>
            Distinct<TSource, TEnumerator>(this IEnumerable<TSource?, TEnumerator> source)
            where TSource : struct, IEquatable<TSource>
            where TEnumerator : IEnumerator<TSource?>
            => DistinctImpl(source ?? throw Error.ArgumentNull(nameof(source)));
        #endregion

        public static WhereEnumerable<TSource?, TEnumerator> DistinctImpl<TSource, TEnumerator>(this IEnumerable<TSource?, TEnumerator> source)
            where TSource : struct, IEquatable<TSource>
            where TEnumerator : IEnumerator<TSource?>
            => DistinctImpl(source, new NullableEqualityComparer<TSource>());

        private static WhereEnumerable<TSource, TEnumerator> DistinctImpl<TSource, TEnumerator>(IEnumerable<TSource, TEnumerator> source)
            where TSource : IEquatable<TSource>
            where TEnumerator : IEnumerator<TSource>
            => DistinctImpl(source, new EquatableEqualityComparer<TSource>());

        private static WhereEnumerable<TSource, TEnumerator> DistinctImpl<TSource, TEnumerator, TEqualityComparer>(IEnumerable<TSource, TEnumerator> source, TEqualityComparer comparer)
            where TEnumerator : IEnumerator<TSource>
            where TEqualityComparer : IEqualityComparer<TSource>
            => new WhereEnumerable<TSource, TEnumerator>(source, new Set<TSource, TEqualityComparer>(comparer).Add);
        #endregion

        #region Except
        public static ExceptEnumerable<TSource, TEnumerator1, TEnumerator2, EquatableEqualityComparer<TSource>> Except<TSource, TEnumerator1, TEnumerator2>(this IEnumerable<TSource, TEnumerator1> first, IEnumerable<TSource, TEnumerator2> second)
            where TSource : IEquatable<TSource>
            where TEnumerator1 : IEnumerator<TSource>
            where TEnumerator2 : IEnumerator<TSource>
            => ExceptImpl(first ?? throw Error.ArgumentNull(nameof(first)), second ?? throw Error.ArgumentNull(nameof(second)), new EquatableEqualityComparer<TSource>());

        public static ExceptEnumerable<TSource, TEnumerator1, TEnumerator2, IEqualityComparer<TSource>> Except<TSource, TEnumerator1, TEnumerator2>(this IEnumerable<TSource, TEnumerator1> first, IEnumerable<TSource, TEnumerator2> second, IEqualityComparer<TSource> comparer)
            where TEnumerator1 : IEnumerator<TSource>
            where TEnumerator2 : IEnumerator<TSource>
            => ExceptImpl(first ?? throw Error.ArgumentNull(nameof(first)), second ?? throw Error.ArgumentNull(nameof(second)), comparer ?? EqualityComparer<TSource>.Default);

        public static ExceptEnumerable<TSource, TEnumerator1, TEnumerator2, TEqualityComparer> Except<TSource, TEnumerator1, TEnumerator2, TEqualityComparer>(this IEnumerable<TSource, TEnumerator1> first, IEnumerable<TSource, TEnumerator2> second, TEqualityComparer comparer)
            where TEnumerator1 : IEnumerator<TSource>
            where TEnumerator2 : IEnumerator<TSource>
            where TEqualityComparer : struct, IEqualityComparer<TSource>
            => ExceptImpl(first ?? throw Error.ArgumentNull(nameof(first)), second ?? throw Error.ArgumentNull(nameof(second)), comparer);

        #region String optimization
        public static ExceptEnumerable<string, TEnumerator1, TEnumerator2, Cmp.StringComparer> Except<TEnumerator1, TEnumerator2>(this IEnumerable<string, TEnumerator1> first, IEnumerable<string, TEnumerator2> second)
            where TEnumerator1 : IEnumerator<string>
            where TEnumerator2 : IEnumerator<string>
            => ExceptImpl(first ?? throw Error.ArgumentNull(nameof(first)), second ?? throw Error.ArgumentNull(nameof(second)), new Cmp.StringComparer());
        #endregion

        #region Nullable optimization
        public static ExceptEnumerable<TSource?, TEnumerator1, TEnumerator2, NullableEqualityComparer<TSource>> Except<TSource, TEnumerator1, TEnumerator2>(this IEnumerable<TSource?, TEnumerator1> first, IEnumerable<TSource?, TEnumerator2> second)
            where TSource : struct, IEquatable<TSource>
            where TEnumerator1 : IEnumerator<TSource?>
            where TEnumerator2 : IEnumerator<TSource?>
            => ExceptImpl(first ?? throw Error.ArgumentNull(nameof(first)), second ?? throw Error.ArgumentNull(nameof(second)), new NullableEqualityComparer<TSource>());
        #endregion

        private static ExceptEnumerable<TSource, TEnumerator1, TEnumerator2, TEqualityComparer> ExceptImpl<TSource, TEnumerator1, TEnumerator2, TEqualityComparer>(IEnumerable<TSource, TEnumerator1> first, IEnumerable<TSource, TEnumerator2> second, TEqualityComparer comparer)
            where TEnumerator1 : IEnumerator<TSource>
            where TEnumerator2 : IEnumerator<TSource>
            where TEqualityComparer : IEqualityComparer<TSource>
            => new ExceptEnumerable<TSource, TEnumerator1, TEnumerator2, TEqualityComparer>(first, second, comparer);
        #endregion

        #region GroupBy
        public static GroupByEnumerable<TSource, TEnumerator, TKey, TSource, EquatableEqualityComparer<TKey>> GroupBy<TSource, TEnumerator, TKey>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey> keySelector)
            where TKey : IEquatable<TKey>
            where TEnumerator : IEnumerator<TSource>
            => GroupByImpl(source ?? throw Error.ArgumentNull(nameof(source)), keySelector ?? throw Error.ArgumentNull(nameof(keySelector)), Util.Identity<TSource>(), new EquatableEqualityComparer<TKey>());

        public static GroupByEnumerable<TSource, TEnumerator, TKey, TSource, IEqualityComparer<TKey>> GroupBy<TSource, TEnumerator, TKey>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
            where TEnumerator : IEnumerator<TSource>
            => GroupByImpl(source ?? throw Error.ArgumentNull(nameof(source)), keySelector ?? throw Error.ArgumentNull(nameof(keySelector)), Util.Identity<TSource>(), comparer ?? EqualityComparer<TKey>.Default);

        public static GroupByEnumerable<TSource, TEnumerator, TKey, TSource, TEqualityComparer> GroupBy<TSource, TEnumerator, TKey, TEqualityComparer>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey> keySelector, TEqualityComparer comparer)
            where TEnumerator : IEnumerator<TSource>
            where TEqualityComparer : struct, IEqualityComparer<TKey>
            => GroupByImpl(source ?? throw Error.ArgumentNull(nameof(source)), keySelector ?? throw Error.ArgumentNull(nameof(keySelector)), Util.Identity<TSource>(), comparer);

        public static GroupByEnumerable<TSource, TEnumerator, TKey, TElement, EquatableEqualityComparer<TKey>> GroupBy<TSource, TEnumerator, TKey, TElement>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
            where TKey : IEquatable<TKey>
            where TEnumerator : IEnumerator<TSource>
            => GroupByImpl(source ?? throw Error.ArgumentNull(nameof(source)), keySelector ?? throw Error.ArgumentNull(nameof(keySelector)), elementSelector ?? throw Error.ArgumentNull(nameof(elementSelector)), new EquatableEqualityComparer<TKey>());

        public static GroupByEnumerable<TSource, TEnumerator, TKey, TElement, IEqualityComparer<TKey>> GroupBy<TSource, TEnumerator, TKey, TElement>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
            where TEnumerator : IEnumerator<TSource>
            => GroupByImpl(source ?? throw Error.ArgumentNull(nameof(source)), keySelector ?? throw Error.ArgumentNull(nameof(keySelector)), elementSelector ?? throw Error.ArgumentNull(nameof(elementSelector)), comparer ?? EqualityComparer<TKey>.Default);

        public static GroupByEnumerable<TSource, TEnumerator, TKey, TElement, TEqualityComparer> GroupBy<TSource, TEnumerator, TKey, TElement, TEqualityComparer>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, TEqualityComparer comparer)
            where TEnumerator : IEnumerator<TSource>
            where TEqualityComparer : struct, IEqualityComparer<TKey>
            => GroupByImpl(source ?? throw Error.ArgumentNull(nameof(source)), keySelector ?? throw Error.ArgumentNull(nameof(keySelector)), elementSelector ?? throw Error.ArgumentNull(nameof(elementSelector)), comparer);

        public static SelectEnumerable<Grouping<TKey, TSource>, TResult, GroupingEnumerator<TKey, TSource>> GroupBy<TSource, TEnumerator, TKey, TResult>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey> keySelector, Func<Grouping<TKey, TSource>, TResult> resultSelector)
            where TKey : IEquatable<TKey>
            where TEnumerator : IEnumerator<TSource>
            => GroupByImpl(source ?? throw Error.ArgumentNull(nameof(source)), keySelector ?? throw Error.ArgumentNull(nameof(keySelector)), Util.Identity<TSource>(), resultSelector ?? throw Error.ArgumentNull(nameof(resultSelector)), new EquatableEqualityComparer<TKey>());

        public static SelectEnumerable<Grouping<TKey, TSource>, TResult, GroupingEnumerator<TKey, TSource>> GroupBy<TSource, TEnumerator, TKey, TResult>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey> keySelector, Func<Grouping<TKey, TSource>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
            where TEnumerator : IEnumerator<TSource>
            => GroupByImpl(source ?? throw Error.ArgumentNull(nameof(source)), keySelector ?? throw Error.ArgumentNull(nameof(keySelector)), Util.Identity<TSource>(), resultSelector ?? throw Error.ArgumentNull(nameof(resultSelector)), comparer ?? EqualityComparer<TKey>.Default);

        public static SelectEnumerable<Grouping<TKey, TSource>, TResult, GroupingEnumerator<TKey, TSource>> GroupBy<TSource, TEnumerator, TKey, TResult, TEqualityComparer>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey> keySelector, Func<Grouping<TKey, TSource>, TResult> resultSelector, TEqualityComparer comparer)
            where TEnumerator : IEnumerator<TSource>
            where TEqualityComparer : struct, IEqualityComparer<TKey>
            => GroupByImpl(source ?? throw Error.ArgumentNull(nameof(source)), keySelector ?? throw Error.ArgumentNull(nameof(keySelector)), Util.Identity<TSource>(), resultSelector ?? throw Error.ArgumentNull(nameof(resultSelector)), comparer);

        public static SelectEnumerable<Grouping<TKey, TElement>, TResult, GroupingEnumerator<TKey, TElement>> GroupBy<TSource, TEnumerator, TKey, TElement, TResult>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, Func<Grouping<TKey, TElement>, TResult> resultSelector)
            where TKey : IEquatable<TKey>
            where TEnumerator : IEnumerator<TSource>
            => GroupByImpl(source ?? throw Error.ArgumentNull(nameof(source)), keySelector ?? throw Error.ArgumentNull(nameof(keySelector)), elementSelector ?? throw Error.ArgumentNull(nameof(elementSelector)), resultSelector ?? throw Error.ArgumentNull(nameof(resultSelector)), new EquatableEqualityComparer<TKey>());

        public static SelectEnumerable<Grouping<TKey, TElement>, TResult, GroupingEnumerator<TKey, TElement>> GroupBy<TSource, TEnumerator, TKey, TElement, TResult>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, Func<Grouping<TKey, TElement>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
            where TEnumerator : IEnumerator<TSource>
            => GroupByImpl(source ?? throw Error.ArgumentNull(nameof(source)), keySelector ?? throw Error.ArgumentNull(nameof(keySelector)), elementSelector ?? throw Error.ArgumentNull(nameof(elementSelector)), resultSelector ?? throw Error.ArgumentNull(nameof(resultSelector)), comparer ?? EqualityComparer<TKey>.Default);

        public static SelectEnumerable<Grouping<TKey, TElement>, TResult, GroupingEnumerator<TKey, TElement>> GroupBy<TSource, TEnumerator, TKey, TElement, TResult, TEqualityComparer>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, Func<Grouping<TKey, TElement>, TResult> resultSelector, TEqualityComparer comparer)
            where TEnumerator : IEnumerator<TSource>
            where TEqualityComparer : struct, IEqualityComparer<TKey>
            => GroupByImpl(source ?? throw Error.ArgumentNull(nameof(source)), keySelector ?? throw Error.ArgumentNull(nameof(keySelector)), elementSelector ?? throw Error.ArgumentNull(nameof(elementSelector)), resultSelector ?? throw Error.ArgumentNull(nameof(resultSelector)), comparer);

        #region String key optimization
        public static GroupByEnumerable<TSource, TEnumerator, string, TSource, Cmp.StringComparer> GroupBy<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, string> keySelector)
            where TEnumerator : IEnumerator<TSource>
            => GroupByImpl(source ?? throw Error.ArgumentNull(nameof(source)), keySelector ?? throw Error.ArgumentNull(nameof(keySelector)), Util.Identity<TSource>(), new Cmp.StringComparer());

        public static GroupByEnumerable<TSource, TEnumerator, string, TElement, Cmp.StringComparer> GroupBy<TSource, TEnumerator, TElement>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, string> keySelector, Func<TSource, TElement> elementSelector)
            where TEnumerator : IEnumerator<TSource>
            => GroupByImpl(source ?? throw Error.ArgumentNull(nameof(source)), keySelector ?? throw Error.ArgumentNull(nameof(keySelector)), elementSelector ?? throw Error.ArgumentNull(nameof(elementSelector)), new Cmp.StringComparer());

        public static SelectEnumerable<Grouping<string, TSource>, TResult, GroupingEnumerator<string, TSource>> GroupBy<TSource, TEnumerator, TResult>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, string> keySelector, Func<Grouping<string, TSource>, TResult> resultSelector)
            where TEnumerator : IEnumerator<TSource>
            => GroupByImpl(source ?? throw Error.ArgumentNull(nameof(source)), keySelector ?? throw Error.ArgumentNull(nameof(keySelector)), Util.Identity<TSource>(), resultSelector ?? throw Error.ArgumentNull(nameof(resultSelector)), new Cmp.StringComparer());

        public static SelectEnumerable<Grouping<string, TElement>, TResult, GroupingEnumerator<string, TElement>> GroupBy<TSource, TEnumerator, TElement, TResult>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, string> keySelector, Func<TSource, TElement> elementSelector, Func<Grouping<string, TElement>, TResult> resultSelector)
            where TEnumerator : IEnumerator<TSource>
            => GroupByImpl(source ?? throw Error.ArgumentNull(nameof(source)), keySelector ?? throw Error.ArgumentNull(nameof(keySelector)), elementSelector ?? throw Error.ArgumentNull(nameof(elementSelector)), resultSelector ?? throw Error.ArgumentNull(nameof(resultSelector)), new Cmp.StringComparer());
        #endregion

        #region Nullable key optimization
        public static GroupByEnumerable<TSource, TEnumerator, TKey?, TSource, NullableEqualityComparer<TKey>> GroupBy<TSource, TEnumerator, TKey>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey?> keySelector)
            where TKey : struct, IEquatable<TKey>
            where TEnumerator : IEnumerator<TSource>
            => GroupByImpl(source ?? throw Error.ArgumentNull(nameof(source)), keySelector ?? throw Error.ArgumentNull(nameof(keySelector)), Util.Identity<TSource>(), new NullableEqualityComparer<TKey>());

        public static GroupByEnumerable<TSource, TEnumerator, TKey?, TElement, NullableEqualityComparer<TKey>> GroupBy<TSource, TEnumerator, TKey, TElement>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey?> keySelector, Func<TSource, TElement> elementSelector)
            where TKey : struct, IEquatable<TKey>
            where TEnumerator : IEnumerator<TSource>
            => GroupByImpl(source ?? throw Error.ArgumentNull(nameof(source)), keySelector ?? throw Error.ArgumentNull(nameof(keySelector)), elementSelector ?? throw Error.ArgumentNull(nameof(elementSelector)), new NullableEqualityComparer<TKey>());

        public static SelectEnumerable<Grouping<TKey?, TSource>, TResult, GroupingEnumerator<TKey?, TSource>> GroupBy<TSource, TEnumerator, TKey, TResult>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey?> keySelector, Func<Grouping<TKey?, TSource>, TResult> resultSelector)
            where TKey : struct, IEquatable<TKey>
            where TEnumerator : IEnumerator<TSource>
            => GroupByImpl(source ?? throw Error.ArgumentNull(nameof(source)), keySelector ?? throw Error.ArgumentNull(nameof(keySelector)), Util.Identity<TSource>(), resultSelector ?? throw Error.ArgumentNull(nameof(resultSelector)), new NullableEqualityComparer<TKey>());

        public static SelectEnumerable<Grouping<TKey?, TElement>, TResult, GroupingEnumerator<TKey?, TElement>> GroupBy<TSource, TEnumerator, TKey, TElement, TResult>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey?> keySelector, Func<TSource, TElement> elementSelector, Func<Grouping<TKey?, TElement>, TResult> resultSelector)
            where TKey : struct, IEquatable<TKey>
            where TEnumerator : IEnumerator<TSource>
            => GroupByImpl(source ?? throw Error.ArgumentNull(nameof(source)), keySelector ?? throw Error.ArgumentNull(nameof(keySelector)), elementSelector ?? throw Error.ArgumentNull(nameof(elementSelector)), resultSelector ?? throw Error.ArgumentNull(nameof(resultSelector)), new NullableEqualityComparer<TKey>());
        #endregion

        private static GroupByEnumerable<TSource, TEnumerator, TKey, TElement, TEqualityComparer> GroupByImpl<TSource, TEnumerator, TKey, TElement, TEqualityComparer>(IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, TEqualityComparer comparer)
            where TEnumerator : IEnumerator<TSource>
            where TEqualityComparer : IEqualityComparer<TKey>
            => new GroupByEnumerable<TSource, TEnumerator, TKey, TElement, TEqualityComparer>(source, keySelector, elementSelector, comparer);

        private static SelectEnumerable<Grouping<TKey, TElement>, TResult, GroupingEnumerator<TKey, TElement>> GroupByImpl<TSource, TEnumerator, TKey, TElement, TResult, TEqualityComparer>(IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, Func<Grouping<TKey, TElement>, TResult> resultSelector, TEqualityComparer comparer)
            where TEnumerator : IEnumerator<TSource>
            where TEqualityComparer : IEqualityComparer<TKey>
            => SelectImpl(GroupByImpl(source, keySelector, elementSelector, comparer), resultSelector);
        #endregion

        #region GroupJoin
        public static GroupJoinEnumerable<TOuter, TOuterEnumerator, TInner, TInnerEnumerator, TKey, TResult, EquatableEqualityComparer<TKey>> GroupJoin<TOuter, TOuterEnumerator, TInner, TInnerEnumerator, TKey, TResult>(this IEnumerable<TOuter, TOuterEnumerator> outer, IEnumerable<TInner, TInnerEnumerator> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, InlinqArray<TInner>, TResult> resultSelector)
            where TOuterEnumerator : IEnumerator<TOuter>
            where TInnerEnumerator : IEnumerator<TInner>
            where TKey : IEquatable<TKey>
            => GroupJoinImpl(outer ?? throw Error.ArgumentNull(nameof(outer)), inner ?? throw Error.ArgumentNull(nameof(inner)), outerKeySelector ?? throw Error.ArgumentNull(nameof(outerKeySelector)), innerKeySelector ?? throw Error.ArgumentNull(nameof(innerKeySelector)), resultSelector ?? throw Error.ArgumentNull(nameof(resultSelector)), new EquatableEqualityComparer<TKey>());

        public static GroupJoinEnumerable<TOuter, TOuterEnumerator, TInner, TInnerEnumerator, TKey, TResult, IEqualityComparer<TKey>> GroupJoin<TOuter, TOuterEnumerator, TInner, TInnerEnumerator, TKey, TResult>(this IEnumerable<TOuter, TOuterEnumerator> outer, IEnumerable<TInner, TInnerEnumerator> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, InlinqArray<TInner>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
            where TOuterEnumerator : IEnumerator<TOuter>
            where TInnerEnumerator : IEnumerator<TInner>
            => GroupJoinImpl(outer ?? throw Error.ArgumentNull(nameof(outer)), inner ?? throw Error.ArgumentNull(nameof(inner)), outerKeySelector ?? throw Error.ArgumentNull(nameof(outerKeySelector)), innerKeySelector ?? throw Error.ArgumentNull(nameof(innerKeySelector)), resultSelector ?? throw Error.ArgumentNull(nameof(resultSelector)), comparer ?? EqualityComparer<TKey>.Default);

        public static GroupJoinEnumerable<TOuter, TOuterEnumerator, TInner, TInnerEnumerator, TKey, TResult, TEqualityComparer> GroupJoin<TOuter, TOuterEnumerator, TInner, TInnerEnumerator, TKey, TResult, TEqualityComparer>(this IEnumerable<TOuter, TOuterEnumerator> outer, IEnumerable<TInner, TInnerEnumerator> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, InlinqArray<TInner>, TResult> resultSelector, TEqualityComparer comparer)
            where TOuterEnumerator : IEnumerator<TOuter>
            where TInnerEnumerator : IEnumerator<TInner>
            where TEqualityComparer : struct, IEqualityComparer<TKey>
            => GroupJoinImpl(outer ?? throw Error.ArgumentNull(nameof(outer)), inner ?? throw Error.ArgumentNull(nameof(inner)), outerKeySelector ?? throw Error.ArgumentNull(nameof(outerKeySelector)), innerKeySelector ?? throw Error.ArgumentNull(nameof(innerKeySelector)), resultSelector ?? throw Error.ArgumentNull(nameof(resultSelector)), comparer);

        #region String key optimization
        public static GroupJoinEnumerable<TOuter, TOuterEnumerator, TInner, TInnerEnumerator, string, TResult, Cmp.StringComparer> GroupJoin<TOuter, TOuterEnumerator, TInner, TInnerEnumerator, TResult>(this IEnumerable<TOuter, TOuterEnumerator> outer, IEnumerable<TInner, TInnerEnumerator> inner, Func<TOuter, string> outerKeySelector, Func<TInner, string> innerKeySelector, Func<TOuter, InlinqArray<TInner>, TResult> resultSelector)
            where TOuterEnumerator : IEnumerator<TOuter>
            where TInnerEnumerator : IEnumerator<TInner>
            => GroupJoin(outer, inner, outerKeySelector, innerKeySelector, resultSelector, new Cmp.StringComparer());

        #endregion

        #region Nullable key optimization
        public static GroupJoinEnumerable<TOuter, TOuterEnumerator, TInner, TInnerEnumerator, TKey?, TResult, NullableEqualityComparer<TKey>> GroupJoin<TOuter, TOuterEnumerator, TInner, TInnerEnumerator, TKey, TResult>(this IEnumerable<TOuter, TOuterEnumerator> outer, IEnumerable<TInner, TInnerEnumerator> inner, Func<TOuter, TKey?> outerKeySelector, Func<TInner, TKey?> innerKeySelector, Func<TOuter, InlinqArray<TInner>, TResult> resultSelector)
            where TOuterEnumerator : IEnumerator<TOuter>
            where TInnerEnumerator : IEnumerator<TInner>
            where TKey : struct, IEquatable<TKey>
            => GroupJoin(outer, inner, outerKeySelector, innerKeySelector, resultSelector, new NullableEqualityComparer<TKey>());
        #endregion

        private static GroupJoinEnumerable<TOuter, TOuterEnumerator, TInner, TInnerEnumerator, TKey, TResult, TEqualityComparer> GroupJoinImpl<TOuter, TOuterEnumerator, TInner, TInnerEnumerator, TKey, TResult, TEqualityComparer>(IEnumerable<TOuter, TOuterEnumerator> outer, IEnumerable<TInner, TInnerEnumerator> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, InlinqArray<TInner>, TResult> resultSelector, TEqualityComparer comparer)
            where TOuterEnumerator : IEnumerator<TOuter>
            where TInnerEnumerator : IEnumerator<TInner>
            where TEqualityComparer : IEqualityComparer<TKey>
            => new GroupJoinEnumerable<TOuter, TOuterEnumerator, TInner, TInnerEnumerator, TKey, TResult, TEqualityComparer>(outer, inner, outerKeySelector, innerKeySelector, resultSelector, comparer);
        #endregion

        #region Intersect
        public static IntersectEnumerable<TSource, TEnumerator1, TEnumerator2, EquatableEqualityComparer<TSource>> Intersect<TSource, TEnumerator1, TEnumerator2>(this IEnumerable<TSource, TEnumerator1> first, IEnumerable<TSource, TEnumerator2> second)
            where TSource : IEquatable<TSource>
            where TEnumerator1 : IEnumerator<TSource>
            where TEnumerator2 : IEnumerator<TSource>
            => IntersectImpl(first ?? throw Error.ArgumentNull(nameof(first)), second ?? throw Error.ArgumentNull(nameof(second)), new EquatableEqualityComparer<TSource>());

        public static IntersectEnumerable<TSource, TEnumerator1, TEnumerator2, IEqualityComparer<TSource>> Intersect<TSource, TEnumerator1, TEnumerator2>(this IEnumerable<TSource, TEnumerator1> first, IEnumerable<TSource, TEnumerator2> second, IEqualityComparer<TSource> comparer)
            where TEnumerator1 : IEnumerator<TSource>
            where TEnumerator2 : IEnumerator<TSource>
            => IntersectImpl(first ?? throw Error.ArgumentNull(nameof(first)), second ?? throw Error.ArgumentNull(nameof(second)), comparer ?? EqualityComparer<TSource>.Default);

        public static IntersectEnumerable<TSource, TEnumerator1, TEnumerator2, TEqualityComparer>
            Intersect<TSource, TEnumerator1, TEnumerator2, TEqualityComparer>(this IEnumerable<TSource, TEnumerator1> first, IEnumerable<TSource, TEnumerator2> second, TEqualityComparer comparer)
            where TEnumerator1 : IEnumerator<TSource>
            where TEnumerator2 : IEnumerator<TSource>
            where TEqualityComparer : struct, IEqualityComparer<TSource>
            => IntersectImpl(first ?? throw Error.ArgumentNull(nameof(first)), second ?? throw Error.ArgumentNull(nameof(second)), comparer);

        #region String optimization
        public static IntersectEnumerable<string, TEnumerator1, TEnumerator2, Cmp.StringComparer>
            Intersect<TEnumerator1, TEnumerator2>(this IEnumerable<string, TEnumerator1> first, IEnumerable<string, TEnumerator2> second)
            where TEnumerator1 : IEnumerator<string>
            where TEnumerator2 : IEnumerator<string>
            => IntersectImpl(first ?? throw Error.ArgumentNull(nameof(first)), second ?? throw Error.ArgumentNull(nameof(second)), new Cmp.StringComparer());
        #endregion

        #region Nullable optimization
        public static IntersectEnumerable<TSource?, TEnumerator1, TEnumerator2, NullableEqualityComparer<TSource>>
            Intersect<TSource, TEnumerator1, TEnumerator2>(this IEnumerable<TSource?, TEnumerator1> first, IEnumerable<TSource?, TEnumerator2> second)
            where TSource : struct, IEquatable<TSource>
            where TEnumerator1 : IEnumerator<TSource?>
            where TEnumerator2 : IEnumerator<TSource?>
            => IntersectImpl(first ?? throw Error.ArgumentNull(nameof(first)), second ?? throw Error.ArgumentNull(nameof(second)), new NullableEqualityComparer<TSource>());
        #endregion

        private static IntersectEnumerable<TSource, TEnumerator1, TEnumerator2, TEqualityComparer> IntersectImpl<TSource, TEnumerator1, TEnumerator2, TEqualityComparer>(IEnumerable<TSource, TEnumerator1> first, IEnumerable<TSource, TEnumerator2> second, TEqualityComparer comparer)
            where TEnumerator1 : IEnumerator<TSource>
            where TEnumerator2 : IEnumerator<TSource>
            where TEqualityComparer : IEqualityComparer<TSource>
            => new IntersectEnumerable<TSource, TEnumerator1, TEnumerator2, TEqualityComparer>(first, second, comparer);
        #endregion

        #region Reverse
        public static ReverseEnumerable<TSource, TEnumerator> Reverse<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source)
            where TEnumerator : IEnumerator<TSource>
            => new ReverseEnumerable<TSource, TEnumerator>(source ?? throw Error.ArgumentNull(nameof(source)));
        #endregion

        #region Union
        public static WhereEnumerable<TSource, ConcatEnumerator<TSource, TEnumerator1, TEnumerator2>> Union<TSource, TEnumerator1, TEnumerator2>(this IEnumerable<TSource, TEnumerator1> first, IEnumerable<TSource, TEnumerator2> second)
            where TSource : IEquatable<TSource>
            where TEnumerator1 : IEnumerator<TSource>
            where TEnumerator2 : IEnumerator<TSource>
            => UnionImpl(first ?? throw Error.ArgumentNull(nameof(first)), second ?? throw Error.ArgumentNull(nameof(second)), new EquatableEqualityComparer<TSource>());

        public static WhereEnumerable<TSource, ConcatEnumerator<TSource, TEnumerator1, TEnumerator2>> Union<TSource, TEnumerator1, TEnumerator2>(this IEnumerable<TSource, TEnumerator1> first, IEnumerable<TSource, TEnumerator2> second, IEqualityComparer<TSource> comparer)
            where TEnumerator1 : IEnumerator<TSource>
            where TEnumerator2 : IEnumerator<TSource>
            => UnionImpl(first ?? throw Error.ArgumentNull(nameof(first)), second ?? throw Error.ArgumentNull(nameof(second)), comparer ?? EqualityComparer<TSource>.Default);

        public static WhereEnumerable<TSource, ConcatEnumerator<TSource, TEnumerator1, TEnumerator2>> Union<TSource, TEnumerator1, TEnumerator2, TEqualityComparer>(this IEnumerable<TSource, TEnumerator1> first, IEnumerable<TSource, TEnumerator2> second, TEqualityComparer comparer)
            where TEnumerator1 : IEnumerator<TSource>
            where TEnumerator2 : IEnumerator<TSource>
            where TEqualityComparer : struct, IEqualityComparer<TSource>
            => UnionImpl(first ?? throw Error.ArgumentNull(nameof(first)), second ?? throw Error.ArgumentNull(nameof(second)), comparer);

        #region String optimization
        public static WhereEnumerable<string, ConcatEnumerator<string, TEnumerator1, TEnumerator2>> Union<TEnumerator1, TEnumerator2>(this IEnumerable<string, TEnumerator1> first, IEnumerable<string, TEnumerator2> second)
            where TEnumerator1 : IEnumerator<string>
            where TEnumerator2 : IEnumerator<string>
            => UnionImpl(first ?? throw Error.ArgumentNull(nameof(first)), second ?? throw Error.ArgumentNull(nameof(second)), new Cmp.StringComparer());
        #endregion

        #region Nullable optimization
        public static WhereEnumerable<TSource?, ConcatEnumerator<TSource?, TEnumerator1, TEnumerator2>> Union<TSource, TEnumerator1, TEnumerator2>(this IEnumerable<TSource?, TEnumerator1> first, IEnumerable<TSource?, TEnumerator2> second)
            where TSource : struct, IEquatable<TSource>
            where TEnumerator1 : IEnumerator<TSource?>
            where TEnumerator2 : IEnumerator<TSource?>
            => UnionImpl(first ?? throw Error.ArgumentNull(nameof(first)), second ?? throw Error.ArgumentNull(nameof(second)), new NullableEqualityComparer<TSource>());
        #endregion

        private static WhereEnumerable<TSource, ConcatEnumerator<TSource, TEnumerator1, TEnumerator2>> UnionImpl<TSource, TEnumerator1, TEnumerator2, TEqualityComparer>(IEnumerable<TSource, TEnumerator1> first, IEnumerable<TSource, TEnumerator2> second, TEqualityComparer comparer)
            where TEnumerator1 : IEnumerator<TSource>
            where TEnumerator2 : IEnumerator<TSource>
            where TEqualityComparer : IEqualityComparer<TSource>
            => DistinctImpl(ConcatImpl(first, second), comparer);
        #endregion
    }
}
