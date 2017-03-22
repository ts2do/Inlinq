using Inlinq.Cmp;
using Inlinq.Impl;
using System;
using System.Collections.Generic;

namespace Inlinq
{
    public static partial class Inlinqer
    {
        #region Distinct
        public static WhereEnumerable<TSource, TEnumerator, DistinctPredicate<TSource, EquatableEqualityComparer<TSource>>> Distinct<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source)
            where TSource : IEquatable<TSource>
            where TEnumerator : IEnumerator<TSource>
        {
            return new WhereEnumerable<TSource, TEnumerator, DistinctPredicate<TSource, EquatableEqualityComparer<TSource>>>(
                source ?? throw Error.ArgumentNull(nameof(source)), new DistinctPredicate<TSource, EquatableEqualityComparer<TSource>>(EqualityComparers.Equatable<TSource>()));
        }

        public static WhereEnumerable<TSource, TEnumerator, DistinctPredicate<TSource, IEqualityComparer<TSource>>>
            Distinct<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source, IEqualityComparer<TSource> comparer)
            where TEnumerator : IEnumerator<TSource>
        {
            return new WhereEnumerable<TSource, TEnumerator, DistinctPredicate<TSource, IEqualityComparer<TSource>>>(
                source ?? throw Error.ArgumentNull(nameof(source)), new DistinctPredicate<TSource, IEqualityComparer<TSource>>(comparer ?? EqualityComparer<TSource>.Default));
        }

        public static WhereEnumerable<TSource, TEnumerator, DistinctPredicate<TSource, TEqualityComparer>>
            Distinct<TSource, TEnumerator, TEqualityComparer>(this IEnumerable<TSource, TEnumerator> source, TEqualityComparer comparer)
            where TEnumerator : IEnumerator<TSource>
            where TEqualityComparer : struct, IEqualityComparer<TSource>
        {
            return new WhereEnumerable<TSource, TEnumerator, DistinctPredicate<TSource, TEqualityComparer>>(
                source ?? throw Error.ArgumentNull(nameof(source)), new DistinctPredicate<TSource, TEqualityComparer>(comparer));
        }

        public static WhereEnumerable<string, TEnumerator, DistinctPredicate<string, Cmp.StringComparer>> Distinct<TEnumerator>(this IEnumerable<string, TEnumerator> source)
            where TEnumerator : IEnumerator<string>
        {
            return new WhereEnumerable<string, TEnumerator, DistinctPredicate<string, Cmp.StringComparer>>(
                source ?? throw Error.ArgumentNull(nameof(source)), new DistinctPredicate<string, Cmp.StringComparer>(new Cmp.StringComparer()));
        }

        public static WhereEnumerable<TSource?, TEnumerator, DistinctPredicate<TSource?, NullableEqualityComparer<TSource, EquatableEqualityComparer<TSource>>>>
            Distinct<TSource, TEnumerator>(this IEnumerable<TSource?, TEnumerator> source)
            where TSource : struct, IEquatable<TSource>
            where TEnumerator : IEnumerator<TSource?>
        {
            return new WhereEnumerable<TSource?, TEnumerator, DistinctPredicate<TSource?, NullableEqualityComparer<TSource, EquatableEqualityComparer<TSource>>>>(
                source ?? throw Error.ArgumentNull(nameof(source)), new DistinctPredicate<TSource?, NullableEqualityComparer<TSource, EquatableEqualityComparer<TSource>>>(EqualityComparers.Nullable<TSource>()));
        }
        #endregion

        #region Except
        public static ExceptEnumerable<TSource, TEnumerator1, TEnumerator2, EquatableEqualityComparer<TSource>>
            Except<TSource, TEnumerator1, TEnumerator2>(this IEnumerable<TSource, TEnumerator1> first, IEnumerable<TSource, TEnumerator2> second)
            where TSource : IEquatable<TSource>
            where TEnumerator1 : IEnumerator<TSource>
            where TEnumerator2 : IEnumerator<TSource>
        {
            return new ExceptEnumerable<TSource, TEnumerator1, TEnumerator2, EquatableEqualityComparer<TSource>>(
                first ?? throw Error.ArgumentNull(nameof(first)), second ?? throw Error.ArgumentNull(nameof(second)), EqualityComparers.Equatable<TSource>());
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
                first ?? throw Error.ArgumentNull(nameof(first)), second ?? throw Error.ArgumentNull(nameof(second)), EqualityComparers.Nullable<TSource>());
        }
        #endregion

        #region GroupBy
        public static GroupByEnumerable<TSource, TEnumerator, TKey, TSource, TKeySelector, Identity<TSource>, EquatableEqualityComparer<TKey>>
            GroupBy<TSource, TEnumerator, TKeySelector, TKey>(this IEnumerable<TSource, TEnumerator> source, ITFunctor<TKeySelector, TSource, TKey> keySelector)
            where TKey : IEquatable<TKey>
            where TEnumerator : IEnumerator<TSource>
            where TKeySelector : IFunctor<TSource, TKey>
        {
            return new GroupByEnumerable<TSource, TEnumerator, TKey, TSource, TKeySelector, Identity<TSource>, EquatableEqualityComparer<TKey>>(
                source ?? throw Error.ArgumentNull(nameof(source)), (keySelector ?? throw Error.ArgumentNull(nameof(keySelector))).Unwrap(), new Identity<TSource>(), EqualityComparers.Equatable<TKey>());
        }

        public static GroupByEnumerable<TSource, TEnumerator, TKey, TSource, FuncFunctor<TSource, TKey>, Identity<TSource>, EquatableEqualityComparer<TKey>>
            GroupBy<TSource, TEnumerator, TKey>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey> keySelector)
            where TKey : IEquatable<TKey>
            where TEnumerator : IEnumerator<TSource>
        {
            return new GroupByEnumerable<TSource, TEnumerator, TKey, TSource, FuncFunctor<TSource, TKey>, Identity<TSource>, EquatableEqualityComparer<TKey>>(
                source ?? throw Error.ArgumentNull(nameof(source)), new FuncFunctor<TSource, TKey>(keySelector ?? throw Error.ArgumentNull(nameof(keySelector))),
                new Identity<TSource>(), EqualityComparers.Equatable<TKey>());
        }

        public static GroupByEnumerable<TSource, TEnumerator, TKey, TSource, TKeySelector, Identity<TSource>, IEqualityComparer<TKey>>
            GroupBy<TSource, TEnumerator, TKeySelector, TKey>(this IEnumerable<TSource, TEnumerator> source, ITFunctor<TKeySelector, TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
            where TEnumerator : IEnumerator<TSource>
            where TKeySelector : IFunctor<TSource, TKey>
        {
            return new GroupByEnumerable<TSource, TEnumerator, TKey, TSource, TKeySelector, Identity<TSource>, IEqualityComparer<TKey>>(
                source ?? throw Error.ArgumentNull(nameof(source)), (keySelector ?? throw Error.ArgumentNull(nameof(keySelector))).Unwrap(), new Identity<TSource>(), comparer ?? EqualityComparer<TKey>.Default);
        }

        public static GroupByEnumerable<TSource, TEnumerator, TKey, TSource, TKeySelector, Identity<TSource>, IEqualityComparer<TKey>>
            GroupBy<TSource, TEnumerator, TKeySelector, TKey, TEqualityComparer>(this IEnumerable<TSource, TEnumerator> source, ITFunctor<TKeySelector, TSource, TKey> keySelector, TEqualityComparer comparer)
            where TEnumerator : IEnumerator<TSource>
            where TKeySelector : IFunctor<TSource, TKey>
            where TEqualityComparer : struct, IEqualityComparer<TKey>
        {
            return new GroupByEnumerable<TSource, TEnumerator, TKey, TSource, TKeySelector, Identity<TSource>, IEqualityComparer<TKey>>(
                source ?? throw Error.ArgumentNull(nameof(source)), (keySelector ?? throw Error.ArgumentNull(nameof(keySelector))).Unwrap(), new Identity<TSource>(), comparer);
        }

        public static GroupByEnumerable<TSource, TEnumerator, TKey, TSource, FuncFunctor<TSource, TKey>, Identity<TSource>, IEqualityComparer<TKey>>
            GroupBy<TSource, TEnumerator, TKey>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
            where TEnumerator : IEnumerator<TSource>
        {
            return new GroupByEnumerable<TSource, TEnumerator, TKey, TSource, FuncFunctor<TSource, TKey>, Identity<TSource>, IEqualityComparer<TKey>>(
                source ?? throw Error.ArgumentNull(nameof(source)), new FuncFunctor<TSource, TKey>(keySelector ?? throw Error.ArgumentNull(nameof(keySelector))),
                new Identity<TSource>(), comparer ?? EqualityComparer<TKey>.Default);
        }

        public static GroupByEnumerable<TSource, TEnumerator, TKey, TSource, FuncFunctor<TSource, TKey>, Identity<TSource>, IEqualityComparer<TKey>>
            GroupBy<TSource, TEnumerator, TKey, TEqualityComparer>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey> keySelector, TEqualityComparer comparer)
            where TEnumerator : IEnumerator<TSource>
            where TEqualityComparer : struct, IEqualityComparer<TKey>
        {
            return new GroupByEnumerable<TSource, TEnumerator, TKey, TSource, FuncFunctor<TSource, TKey>, Identity<TSource>, IEqualityComparer<TKey>>(
                source ?? throw Error.ArgumentNull(nameof(source)), new FuncFunctor<TSource, TKey>(keySelector ?? throw Error.ArgumentNull(nameof(keySelector))), new Identity<TSource>(), comparer);
        }

        public static GroupByEnumerable<TSource, TEnumerator, TKey, TElement, TKeySelector, TElementSelector, EquatableEqualityComparer<TKey>>
            GroupBy<TSource, TEnumerator, TKeySelector, TKey, TElementSelector, TElement>(this IEnumerable<TSource, TEnumerator> source,
            ITFunctor<TKeySelector, TSource, TKey> keySelector, ITFunctor<TElementSelector, TSource, TElement> elementSelector)
            where TKey : IEquatable<TKey>
            where TEnumerator : IEnumerator<TSource>
            where TKeySelector : IFunctor<TSource, TKey>
            where TElementSelector : IFunctor<TSource, TElement>
        {
            return new GroupByEnumerable<TSource, TEnumerator, TKey, TElement, TKeySelector, TElementSelector, EquatableEqualityComparer<TKey>>(
                source ?? throw Error.ArgumentNull(nameof(source)), (keySelector ?? throw Error.ArgumentNull(nameof(keySelector))).Unwrap(),
                (elementSelector ?? throw Error.ArgumentNull(nameof(elementSelector))).Unwrap(), EqualityComparers.Equatable<TKey>());
        }

        public static GroupByEnumerable<TSource, TEnumerator, TKey, TElement, FuncFunctor<TSource, TKey>, FuncFunctor<TSource, TElement>, EquatableEqualityComparer<TKey>>
            GroupBy<TSource, TEnumerator, TKey, TElement>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
            where TKey : IEquatable<TKey>
            where TEnumerator : IEnumerator<TSource>
        {
            return new GroupByEnumerable<TSource, TEnumerator, TKey, TElement, FuncFunctor<TSource, TKey>, FuncFunctor<TSource, TElement>, EquatableEqualityComparer<TKey>>(
                source ?? throw Error.ArgumentNull(nameof(source)), new FuncFunctor<TSource, TKey>(keySelector ?? throw Error.ArgumentNull(nameof(keySelector))),
                new FuncFunctor<TSource, TElement>(elementSelector ?? throw Error.ArgumentNull(nameof(elementSelector))), EqualityComparers.Equatable<TKey>());
        }

        public static GroupByEnumerable<TSource, TEnumerator, TKey, TElement, TKeySelector, TElementSelector, IEqualityComparer<TKey>>
            GroupBy<TSource, TEnumerator, TKeySelector, TKey, TElementSelector, TElement>(this IEnumerable<TSource, TEnumerator> source,
            ITFunctor<TKeySelector, TSource, TKey> keySelector, ITFunctor<TElementSelector, TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
            where TEnumerator : IEnumerator<TSource>
            where TKeySelector : IFunctor<TSource, TKey>
            where TElementSelector : IFunctor<TSource, TElement>
        {
            return new GroupByEnumerable<TSource, TEnumerator, TKey, TElement, TKeySelector, TElementSelector, IEqualityComparer<TKey>>(
                source ?? throw Error.ArgumentNull(nameof(source)), (keySelector ?? throw Error.ArgumentNull(nameof(keySelector))).Unwrap(),
                (elementSelector ?? throw Error.ArgumentNull(nameof(elementSelector))).Unwrap(), comparer ?? EqualityComparer<TKey>.Default);
        }

        public static GroupByEnumerable<TSource, TEnumerator, TKey, TElement, TKeySelector, TElementSelector, TEqualityComparer>
            GroupBy<TSource, TEnumerator, TKeySelector, TKey, TElementSelector, TElement, TEqualityComparer>(this IEnumerable<TSource, TEnumerator> source,
            ITFunctor<TKeySelector, TSource, TKey> keySelector, ITFunctor<TElementSelector, TSource, TElement> elementSelector, TEqualityComparer comparer)
            where TEnumerator : IEnumerator<TSource>
            where TKeySelector : IFunctor<TSource, TKey>
            where TElementSelector : IFunctor<TSource, TElement>
            where TEqualityComparer : struct, IEqualityComparer<TKey>
        {
            return new GroupByEnumerable<TSource, TEnumerator, TKey, TElement, TKeySelector, TElementSelector, TEqualityComparer>(
                source ?? throw Error.ArgumentNull(nameof(source)), (keySelector ?? throw Error.ArgumentNull(nameof(keySelector))).Unwrap(),
                (elementSelector ?? throw Error.ArgumentNull(nameof(elementSelector))).Unwrap(), comparer);
        }

        public static GroupByEnumerable<TSource, TEnumerator, TKey, TElement, FuncFunctor<TSource, TKey>, FuncFunctor<TSource, TElement>, IEqualityComparer<TKey>>
            GroupBy<TSource, TEnumerator, TKey, TElement>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
            where TEnumerator : IEnumerator<TSource>
        {
            return new GroupByEnumerable<TSource, TEnumerator, TKey, TElement, FuncFunctor<TSource, TKey>, FuncFunctor<TSource, TElement>, IEqualityComparer<TKey>>(
                source ?? throw Error.ArgumentNull(nameof(source)), new FuncFunctor<TSource, TKey>(keySelector ?? throw Error.ArgumentNull(nameof(keySelector))),
                new FuncFunctor<TSource, TElement>(elementSelector ?? throw Error.ArgumentNull(nameof(elementSelector))), comparer ?? EqualityComparer<TKey>.Default);
        }

        public static GroupByEnumerable<TSource, TEnumerator, TKey, TElement, FuncFunctor<TSource, TKey>, FuncFunctor<TSource, TElement>, TEqualityComparer>
            GroupBy<TSource, TEnumerator, TKey, TElement, TEqualityComparer>(this IEnumerable<TSource, TEnumerator> source,
            Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, TEqualityComparer comparer)
            where TEnumerator : IEnumerator<TSource>
            where TEqualityComparer : struct, IEqualityComparer<TKey>
        {
            return new GroupByEnumerable<TSource, TEnumerator, TKey, TElement, FuncFunctor<TSource, TKey>, FuncFunctor<TSource, TElement>, TEqualityComparer>(
                source ?? throw Error.ArgumentNull(nameof(source)), new FuncFunctor<TSource, TKey>(keySelector ?? throw Error.ArgumentNull(nameof(keySelector))),
                new FuncFunctor<TSource, TElement>(elementSelector ?? throw Error.ArgumentNull(nameof(elementSelector))), comparer);
        }

        public static SelectEnumerable<Grouping<TKey, TSource>, TResult, GroupingEnumerator<TKey, TSource>, TResultSelector>
            GroupBy<TSource, TEnumerator, TKeySelector, TKey, TResultSelector, TResult>(this IEnumerable<TSource, TEnumerator> source,
            ITFunctor<TKeySelector, TSource, TKey> keySelector, ITFunctor<TResultSelector, Grouping<TKey, TSource>, TResult> resultSelector)
            where TKey : IEquatable<TKey>
            where TEnumerator : IEnumerator<TSource>
            where TKeySelector : IFunctor<TSource, TKey>
            where TResultSelector : IFunctor<Grouping<TKey, TSource>, TResult>
        {
            return new SelectEnumerable<Grouping<TKey, TSource>, TResult, GroupingEnumerator<TKey, TSource>, TResultSelector>(
                new GroupByEnumerable<TSource, TEnumerator, TKey, TSource, TKeySelector, Identity<TSource>, EquatableEqualityComparer<TKey>>(
                    source ?? throw Error.ArgumentNull(nameof(source)), (keySelector ?? throw Error.ArgumentNull(nameof(keySelector))).Unwrap(),
                    new Identity<TSource>(), EqualityComparers.Equatable<TKey>()), (resultSelector ?? throw Error.ArgumentNull(nameof(resultSelector))).Unwrap());
        }

        public static SelectEnumerable<Grouping<TKey, TSource>, TResult, GroupingEnumerator<TKey, TSource>, FuncFunctor<Grouping<TKey, TSource>, TResult>>
            GroupBy<TSource, TEnumerator, TKey, TResult>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey> keySelector, Func<Grouping<TKey, TSource>, TResult> resultSelector)
            where TKey : IEquatable<TKey>
            where TEnumerator : IEnumerator<TSource>
        {
            return new SelectEnumerable<Grouping<TKey, TSource>, TResult, GroupingEnumerator<TKey, TSource>, FuncFunctor<Grouping<TKey, TSource>, TResult>>(
                new GroupByEnumerable<TSource, TEnumerator, TKey, TSource, FuncFunctor<TSource, TKey>, Identity<TSource>, EquatableEqualityComparer<TKey>>(
                    source ?? throw Error.ArgumentNull(nameof(source)), new FuncFunctor<TSource, TKey>(keySelector ?? throw Error.ArgumentNull(nameof(keySelector))),
                    new Identity<TSource>(), EqualityComparers.Equatable<TKey>()), new FuncFunctor<Grouping<TKey, TSource>, TResult>(resultSelector ?? throw Error.ArgumentNull(nameof(resultSelector))));
        }

        public static SelectEnumerable<Grouping<TKey, TSource>, TResult, GroupingEnumerator<TKey, TSource>, TResultSelector>
            GroupBy<TSource, TEnumerator, TKeySelector, TKey, TResultSelector, TResult>(this IEnumerable<TSource, TEnumerator> source, ITFunctor<TKeySelector, TSource, TKey> keySelector,
            ITFunctor<TResultSelector, Grouping<TKey, TSource>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
            where TEnumerator : IEnumerator<TSource>
            where TKeySelector : IFunctor<TSource, TKey>
            where TResultSelector : IFunctor<Grouping<TKey, TSource>, TResult>
        {
            return new SelectEnumerable<Grouping<TKey, TSource>, TResult, GroupingEnumerator<TKey, TSource>, TResultSelector>(
                new GroupByEnumerable<TSource, TEnumerator, TKey, TSource, TKeySelector, Identity<TSource>, IEqualityComparer<TKey>>(
                    source ?? throw Error.ArgumentNull(nameof(source)), (keySelector ?? throw Error.ArgumentNull(nameof(keySelector))).Unwrap(),
                    new Identity<TSource>(), comparer ?? EqualityComparer<TKey>.Default), (resultSelector ?? throw Error.ArgumentNull(nameof(resultSelector))).Unwrap());
        }

        public static SelectEnumerable<Grouping<TKey, TSource>, TResult, GroupingEnumerator<TKey, TSource>, TResultSelector>
            GroupBy<TSource, TEnumerator, TKeySelector, TKey, TResultSelector, TResult, TEqualityComparer>(this IEnumerable<TSource, TEnumerator> source,
            ITFunctor<TKeySelector, TSource, TKey> keySelector, ITFunctor<TResultSelector, Grouping<TKey, TSource>, TResult> resultSelector, TEqualityComparer comparer)
            where TEnumerator : IEnumerator<TSource>
            where TKeySelector : IFunctor<TSource, TKey>
            where TResultSelector : IFunctor<Grouping<TKey, TSource>, TResult>
            where TEqualityComparer : struct, IEqualityComparer<TKey>
        {
            return new SelectEnumerable<Grouping<TKey, TSource>, TResult, GroupingEnumerator<TKey, TSource>, TResultSelector>(
                new GroupByEnumerable<TSource, TEnumerator, TKey, TSource, TKeySelector, Identity<TSource>, IEqualityComparer<TKey>>(
                    source ?? throw Error.ArgumentNull(nameof(source)), (keySelector ?? throw Error.ArgumentNull(nameof(keySelector))).Unwrap(),
                    new Identity<TSource>(), comparer), (resultSelector ?? throw Error.ArgumentNull(nameof(resultSelector))).Unwrap());
        }

        public static SelectEnumerable<Grouping<TKey, TSource>, TResult, GroupingEnumerator<TKey, TSource>, FuncFunctor<Grouping<TKey, TSource>, TResult>>
            GroupBy<TSource, TEnumerator, TKey, TResult>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey> keySelector,
            Func<Grouping<TKey, TSource>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
            where TEnumerator : IEnumerator<TSource>
        {
            return new SelectEnumerable<Grouping<TKey, TSource>, TResult, GroupingEnumerator<TKey, TSource>, FuncFunctor<Grouping<TKey, TSource>, TResult>>(
                new GroupByEnumerable<TSource, TEnumerator, TKey, TSource, FuncFunctor<TSource, TKey>, Identity<TSource>, IEqualityComparer<TKey>>(
                    source ?? throw Error.ArgumentNull(nameof(source)), new FuncFunctor<TSource, TKey>(keySelector ?? throw Error.ArgumentNull(nameof(keySelector))),
                    new Identity<TSource>(), comparer ?? EqualityComparer<TKey>.Default), new FuncFunctor<Grouping<TKey, TSource>, TResult>(resultSelector ?? throw Error.ArgumentNull(nameof(resultSelector))));
        }

        public static SelectEnumerable<Grouping<TKey, TSource>, TResult, GroupingEnumerator<TKey, TSource>, FuncFunctor<Grouping<TKey, TSource>, TResult>>
            GroupBy<TSource, TEnumerator, TKey, TResult, TEqualityComparer>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey> keySelector,
            Func<Grouping<TKey, TSource>, TResult> resultSelector, TEqualityComparer comparer)
            where TEnumerator : IEnumerator<TSource>
            where TEqualityComparer : struct, IEqualityComparer<TKey>
        {
            return new SelectEnumerable<Grouping<TKey, TSource>, TResult, GroupingEnumerator<TKey, TSource>, FuncFunctor<Grouping<TKey, TSource>, TResult>>(
                new GroupByEnumerable<TSource, TEnumerator, TKey, TSource, FuncFunctor<TSource, TKey>, Identity<TSource>, IEqualityComparer<TKey>>(
                    source ?? throw Error.ArgumentNull(nameof(source)), new FuncFunctor<TSource, TKey>(keySelector ?? throw Error.ArgumentNull(nameof(keySelector))),
                    new Identity<TSource>(), comparer), new FuncFunctor<Grouping<TKey, TSource>, TResult>(resultSelector ?? throw Error.ArgumentNull(nameof(resultSelector))));
        }

        public static SelectEnumerable<Grouping<TKey, TElement>, TResult, GroupingEnumerator<TKey, TElement>, TResultSelector>
            GroupBy<TSource, TEnumerator, TKeySelector, TKey, TElementSelector, TElement, TResultSelector, TResult>(this IEnumerable<TSource, TEnumerator> source,
            ITFunctor<TKeySelector, TSource, TKey> keySelector, ITFunctor<TElementSelector, TSource, TElement> elementSelector,
            ITFunctor<TResultSelector, Grouping<TKey, TElement>, TResult> resultSelector)
            where TKey : IEquatable<TKey>
            where TEnumerator : IEnumerator<TSource>
            where TKeySelector : IFunctor<TSource, TKey>
            where TElementSelector : IFunctor<TSource, TElement>
            where TResultSelector : IFunctor<Grouping<TKey, TElement>, TResult>
        {
            return new SelectEnumerable<Grouping<TKey, TElement>, TResult, GroupingEnumerator<TKey, TElement>, TResultSelector>(
                new GroupByEnumerable<TSource, TEnumerator, TKey, TElement, TKeySelector, TElementSelector, EquatableEqualityComparer<TKey>>(
                    source ?? throw Error.ArgumentNull(nameof(source)), (keySelector ?? throw Error.ArgumentNull(nameof(keySelector))).Unwrap(),
                    (elementSelector ?? throw Error.ArgumentNull(nameof(elementSelector))).Unwrap(), EqualityComparers.Equatable<TKey>()),
                (resultSelector ?? throw Error.ArgumentNull(nameof(resultSelector))).Unwrap());
        }

        public static SelectEnumerable<Grouping<TKey, TElement>, TResult, GroupingEnumerator<TKey, TElement>, FuncFunctor<Grouping<TKey, TElement>, TResult>>
            GroupBy<TSource, TEnumerator, TKey, TElement, TResult>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey> keySelector,
            Func<TSource, TElement> elementSelector, Func<Grouping<TKey, TElement>, TResult> resultSelector)
            where TKey : IEquatable<TKey>
            where TEnumerator : IEnumerator<TSource>
        {
            return new SelectEnumerable<Grouping<TKey, TElement>, TResult, GroupingEnumerator<TKey, TElement>, FuncFunctor<Grouping<TKey, TElement>, TResult>>(
                new GroupByEnumerable<TSource, TEnumerator, TKey, TElement, FuncFunctor<TSource, TKey>, FuncFunctor<TSource, TElement>, EquatableEqualityComparer<TKey>>(
                    source ?? throw Error.ArgumentNull(nameof(source)), new FuncFunctor<TSource, TKey>(keySelector ?? throw Error.ArgumentNull(nameof(keySelector))),
                    new FuncFunctor<TSource, TElement>(elementSelector ?? throw Error.ArgumentNull(nameof(elementSelector))), EqualityComparers.Equatable<TKey>()),
                new FuncFunctor<Grouping<TKey, TElement>, TResult>(resultSelector ?? throw Error.ArgumentNull(nameof(resultSelector))));
        }

        public static SelectEnumerable<Grouping<TKey, TElement>, TResult, GroupingEnumerator<TKey, TElement>, TResultSelector>
            GroupBy<TSource, TEnumerator, TKeySelector, TKey, TElementSelector, TElement, TResultSelector, TResult>(this IEnumerable<TSource, TEnumerator> source,
            ITFunctor<TKeySelector, TSource, TKey> keySelector, ITFunctor<TElementSelector, TSource, TElement> elementSelector,
            ITFunctor<TResultSelector, Grouping<TKey, TElement>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
            where TEnumerator : IEnumerator<TSource>
            where TKeySelector : IFunctor<TSource, TKey>
            where TElementSelector : IFunctor<TSource, TElement>
            where TResultSelector : IFunctor<Grouping<TKey, TElement>, TResult>
        {
            return new SelectEnumerable<Grouping<TKey, TElement>, TResult, GroupingEnumerator<TKey, TElement>, TResultSelector>(
                new GroupByEnumerable<TSource, TEnumerator, TKey, TElement, TKeySelector, TElementSelector, IEqualityComparer<TKey>>(
                    source ?? throw Error.ArgumentNull(nameof(source)), (keySelector ?? throw Error.ArgumentNull(nameof(keySelector))).Unwrap(),
                    (elementSelector ?? throw Error.ArgumentNull(nameof(elementSelector))).Unwrap(), comparer ?? EqualityComparer<TKey>.Default),
                (resultSelector ?? throw Error.ArgumentNull(nameof(resultSelector))).Unwrap());
        }

        public static SelectEnumerable<Grouping<TKey, TElement>, TResult, GroupingEnumerator<TKey, TElement>, TResultSelector>
            GroupBy<TSource, TEnumerator, TKeySelector, TKey, TElementSelector, TElement, TResultSelector, TResult, TEqualityComparer>(this IEnumerable<TSource, TEnumerator> source,
            ITFunctor<TKeySelector, TSource, TKey> keySelector, ITFunctor<TElementSelector, TSource, TElement> elementSelector,
            ITFunctor<TResultSelector, Grouping<TKey, TElement>, TResult> resultSelector, TEqualityComparer comparer)
            where TEnumerator : IEnumerator<TSource>
            where TKeySelector : IFunctor<TSource, TKey>
            where TElementSelector : IFunctor<TSource, TElement>
            where TResultSelector : IFunctor<Grouping<TKey, TElement>, TResult>
            where TEqualityComparer : struct, IEqualityComparer<TKey>
        {
            return new SelectEnumerable<Grouping<TKey, TElement>, TResult, GroupingEnumerator<TKey, TElement>, TResultSelector>(
                new GroupByEnumerable<TSource, TEnumerator, TKey, TElement, TKeySelector, TElementSelector, TEqualityComparer>(
                    source ?? throw Error.ArgumentNull(nameof(source)), (keySelector ?? throw Error.ArgumentNull(nameof(keySelector))).Unwrap(),
                    (elementSelector ?? throw Error.ArgumentNull(nameof(elementSelector))).Unwrap(), comparer), (resultSelector ?? throw Error.ArgumentNull(nameof(resultSelector))).Unwrap());
        }

        public static SelectEnumerable<Grouping<TKey, TElement>, TResult, GroupingEnumerator<TKey, TElement>, FuncFunctor<Grouping<TKey, TElement>, TResult>>
            GroupBy<TSource, TEnumerator, TKey, TElement, TResult>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey> keySelector,
            Func<TSource, TElement> elementSelector, Func<Grouping<TKey, TElement>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
            where TEnumerator : IEnumerator<TSource>
        {
            return new SelectEnumerable<Grouping<TKey, TElement>, TResult, GroupingEnumerator<TKey, TElement>, FuncFunctor<Grouping<TKey, TElement>, TResult>>(
                new GroupByEnumerable<TSource, TEnumerator, TKey, TElement, FuncFunctor<TSource, TKey>, FuncFunctor<TSource, TElement>, IEqualityComparer<TKey>>(
                    source ?? throw Error.ArgumentNull(nameof(source)), new FuncFunctor<TSource, TKey>(keySelector ?? throw Error.ArgumentNull(nameof(keySelector))),
                    new FuncFunctor<TSource, TElement>(elementSelector ?? throw Error.ArgumentNull(nameof(elementSelector))), comparer ?? EqualityComparer<TKey>.Default),
                new FuncFunctor<Grouping<TKey, TElement>, TResult>(resultSelector ?? throw Error.ArgumentNull(nameof(resultSelector))));
        }

        public static SelectEnumerable<Grouping<TKey, TElement>, TResult, GroupingEnumerator<TKey, TElement>, FuncFunctor<Grouping<TKey, TElement>, TResult>>
            GroupBy<TSource, TEnumerator, TKey, TElement, TResult, TEqualityComparer>(this IEnumerable<TSource, TEnumerator> source, Func<TSource, TKey> keySelector,
            Func<TSource, TElement> elementSelector, Func<Grouping<TKey, TElement>, TResult> resultSelector, TEqualityComparer comparer)
            where TEnumerator : IEnumerator<TSource>
            where TEqualityComparer : struct, IEqualityComparer<TKey>
        {
            return new SelectEnumerable<Grouping<TKey, TElement>, TResult, GroupingEnumerator<TKey, TElement>, FuncFunctor<Grouping<TKey, TElement>, TResult>>(
                new GroupByEnumerable<TSource, TEnumerator, TKey, TElement, FuncFunctor<TSource, TKey>, FuncFunctor<TSource, TElement>, TEqualityComparer>(
                    source ?? throw Error.ArgumentNull(nameof(source)), new FuncFunctor<TSource, TKey>(keySelector ?? throw Error.ArgumentNull(nameof(keySelector))),
                    new FuncFunctor<TSource, TElement>(elementSelector ?? throw Error.ArgumentNull(nameof(elementSelector))), comparer),
                new FuncFunctor<Grouping<TKey, TElement>, TResult>(resultSelector ?? throw Error.ArgumentNull(nameof(resultSelector))));
        }
        #endregion

        #region GroupJoin
        public static GroupJoinEnumerable<TOuter, TOuterEnumerator, TOuterKeySelector, TInner, TInnerEnumerator, TInnerKeySelector, TKey, TResultSelector, TResult, EquatableEqualityComparer<TKey>>
            GroupJoin<TOuter, TOuterEnumerator, TOuterKeySelector, TInner, TInnerEnumerator, TInnerKeySelector, TKey, TResultSelector, TResult>(this IEnumerable<TOuter, TOuterEnumerator> outer,
            IEnumerable<TInner, TInnerEnumerator> inner, ITFunctor<TOuterKeySelector, TOuter, TKey> outerKeySelector, ITFunctor<TInnerKeySelector, TInner, TKey> innerKeySelector,
            ITFunctor<TResultSelector, TOuter, InlinqArray<TInner>, TResult> resultSelector)
            where TOuterEnumerator : IEnumerator<TOuter>
            where TOuterKeySelector : IFunctor<TOuter, TKey>
            where TInnerEnumerator : IEnumerator<TInner>
            where TInnerKeySelector : IFunctor<TInner, TKey>
            where TKey : IEquatable<TKey>
            where TResultSelector : IFunctor<TOuter, InlinqArray<TInner>, TResult>
        {
            return new GroupJoinEnumerable<TOuter, TOuterEnumerator, TOuterKeySelector, TInner, TInnerEnumerator, TInnerKeySelector, TKey, TResultSelector, TResult, EquatableEqualityComparer<TKey>>(
                outer ?? throw Error.ArgumentNull(nameof(outer)), inner ?? throw Error.ArgumentNull(nameof(inner)),
                (outerKeySelector ?? throw Error.ArgumentNull(nameof(outerKeySelector))).Unwrap(), (innerKeySelector ?? throw Error.ArgumentNull(nameof(innerKeySelector))).Unwrap(),
                (resultSelector ?? throw Error.ArgumentNull(nameof(resultSelector))).Unwrap(), EqualityComparers.Equatable<TKey>());
        }

        public static GroupJoinEnumerable<TOuter, TOuterEnumerator, FuncFunctor<TOuter, TKey>, TInner, TInnerEnumerator, FuncFunctor<TInner, TKey>, TKey, FuncFunctor<TOuter, InlinqArray<TInner>, TResult>, TResult, EquatableEqualityComparer<TKey>>
            GroupJoin<TOuter, TOuterEnumerator, TInner, TInnerEnumerator, TKey, TResult>(this IEnumerable<TOuter, TOuterEnumerator> outer, IEnumerable<TInner, TInnerEnumerator> inner,
            Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, InlinqArray<TInner>, TResult> resultSelector)
            where TOuterEnumerator : IEnumerator<TOuter>
            where TInnerEnumerator : IEnumerator<TInner>
            where TKey : IEquatable<TKey>
        {
            return new GroupJoinEnumerable<TOuter, TOuterEnumerator, FuncFunctor<TOuter, TKey>, TInner, TInnerEnumerator, FuncFunctor<TInner, TKey>, TKey, FuncFunctor<TOuter, InlinqArray<TInner>, TResult>, TResult, EquatableEqualityComparer<TKey>>(
                outer ?? throw Error.ArgumentNull(nameof(outer)), inner ?? throw Error.ArgumentNull(nameof(inner)), new FuncFunctor<TOuter, TKey>(outerKeySelector ?? throw Error.ArgumentNull(nameof(outerKeySelector))),
                new FuncFunctor<TInner, TKey>(innerKeySelector ?? throw Error.ArgumentNull(nameof(innerKeySelector))),
                new FuncFunctor<TOuter, InlinqArray<TInner>, TResult>(resultSelector ?? throw Error.ArgumentNull(nameof(resultSelector))), EqualityComparers.Equatable<TKey>());
        }

        public static GroupJoinEnumerable<TOuter, TOuterEnumerator, TOuterKeySelector, TInner, TInnerEnumerator, TInnerKeySelector, TKey, TResultSelector, TResult, IEqualityComparer<TKey>>
            GroupJoin<TOuter, TOuterEnumerator, TOuterKeySelector, TInner, TInnerEnumerator, TInnerKeySelector, TKey, TResultSelector, TResult>(this IEnumerable<TOuter, TOuterEnumerator> outer,
            IEnumerable<TInner, TInnerEnumerator> inner, ITFunctor<TOuterKeySelector, TOuter, TKey> outerKeySelector, ITFunctor<TInnerKeySelector, TInner, TKey> innerKeySelector,
            ITFunctor<TResultSelector, TOuter, InlinqArray<TInner>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
            where TOuterEnumerator : IEnumerator<TOuter>
            where TOuterKeySelector : IFunctor<TOuter, TKey>
            where TInnerEnumerator : IEnumerator<TInner>
            where TInnerKeySelector : IFunctor<TInner, TKey>
            where TKey : IEquatable<TKey>
            where TResultSelector : IFunctor<TOuter, InlinqArray<TInner>, TResult>
        {
            return new GroupJoinEnumerable<TOuter, TOuterEnumerator, TOuterKeySelector, TInner, TInnerEnumerator, TInnerKeySelector, TKey, TResultSelector, TResult, IEqualityComparer<TKey>>(
                outer ?? throw Error.ArgumentNull(nameof(outer)), inner ?? throw Error.ArgumentNull(nameof(inner)), (outerKeySelector ?? throw Error.ArgumentNull(nameof(outerKeySelector))).Unwrap(), (innerKeySelector ?? throw Error.ArgumentNull(nameof(innerKeySelector))).Unwrap(), (resultSelector ?? throw Error.ArgumentNull(nameof(resultSelector))).Unwrap(), comparer ?? EqualityComparer<TKey>.Default);
        }

        public static GroupJoinEnumerable<TOuter, TOuterEnumerator, FuncFunctor<TOuter, TKey>, TInner, TInnerEnumerator, FuncFunctor<TInner, TKey>, TKey, FuncFunctor<TOuter, InlinqArray<TInner>, TResult>, TResult, IEqualityComparer<TKey>>
            GroupJoin<TOuter, TOuterEnumerator, TInner, TInnerEnumerator, TKey, TResult>(this IEnumerable<TOuter, TOuterEnumerator> outer,
            IEnumerable<TInner, TInnerEnumerator> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector,
            Func<TOuter, InlinqArray<TInner>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
            where TOuterEnumerator : IEnumerator<TOuter>
            where TInnerEnumerator : IEnumerator<TInner>
        {
            return new GroupJoinEnumerable<TOuter, TOuterEnumerator, FuncFunctor<TOuter, TKey>, TInner, TInnerEnumerator, FuncFunctor<TInner, TKey>, TKey, FuncFunctor<TOuter, InlinqArray<TInner>, TResult>, TResult, IEqualityComparer<TKey>>(
                outer ?? throw Error.ArgumentNull(nameof(outer)), inner ?? throw Error.ArgumentNull(nameof(inner)), new FuncFunctor<TOuter, TKey>(outerKeySelector ?? throw Error.ArgumentNull(nameof(outerKeySelector))),
                new FuncFunctor<TInner, TKey>(innerKeySelector ?? throw Error.ArgumentNull(nameof(innerKeySelector))),
                new FuncFunctor<TOuter, InlinqArray<TInner>, TResult>(resultSelector ?? throw Error.ArgumentNull(nameof(resultSelector))), comparer ?? EqualityComparer<TKey>.Default);
        }

        public static GroupJoinEnumerable<TOuter, TOuterEnumerator, TOuterKeySelector, TInner, TInnerEnumerator, TInnerKeySelector, TKey, TResultSelector, TResult, TEqualityComparer>
            GroupJoin<TOuter, TOuterEnumerator, TOuterKeySelector, TInner, TInnerEnumerator, TInnerKeySelector, TKey, TResultSelector, TResult, TEqualityComparer>(this IEnumerable<TOuter, TOuterEnumerator> outer,
            IEnumerable<TInner, TInnerEnumerator> inner, ITFunctor<TOuterKeySelector, TOuter, TKey> outerKeySelector, ITFunctor<TInnerKeySelector, TInner, TKey> innerKeySelector,
            ITFunctor<TResultSelector, TOuter, InlinqArray<TInner>, TResult> resultSelector, TEqualityComparer comparer)
            where TOuterEnumerator : IEnumerator<TOuter>
            where TOuterKeySelector : IFunctor<TOuter, TKey>
            where TInnerEnumerator : IEnumerator<TInner>
            where TInnerKeySelector : IFunctor<TInner, TKey>
            where TKey : IEquatable<TKey>
            where TResultSelector : IFunctor<TOuter, InlinqArray<TInner>, TResult>
            where TEqualityComparer : struct, IEqualityComparer<TKey>
        {
            return new GroupJoinEnumerable<TOuter, TOuterEnumerator, TOuterKeySelector, TInner, TInnerEnumerator, TInnerKeySelector, TKey, TResultSelector, TResult, TEqualityComparer>(
                outer ?? throw Error.ArgumentNull(nameof(outer)), inner ?? throw Error.ArgumentNull(nameof(inner)), (outerKeySelector ?? throw Error.ArgumentNull(nameof(outerKeySelector))).Unwrap(),
                (innerKeySelector ?? throw Error.ArgumentNull(nameof(innerKeySelector))).Unwrap(), (resultSelector ?? throw Error.ArgumentNull(nameof(resultSelector))).Unwrap(), comparer);
        }

        public static GroupJoinEnumerable<TOuter, TOuterEnumerator, FuncFunctor<TOuter, TKey>, TInner, TInnerEnumerator, FuncFunctor<TInner, TKey>, TKey, FuncFunctor<TOuter, InlinqArray<TInner>, TResult>, TResult, TEqualityComparer>
            GroupJoin<TOuter, TOuterEnumerator, TInner, TInnerEnumerator, TKey, TResult, TEqualityComparer>(this IEnumerable<TOuter, TOuterEnumerator> outer,
            IEnumerable<TInner, TInnerEnumerator> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector,
            Func<TOuter, InlinqArray<TInner>, TResult> resultSelector, TEqualityComparer comparer)
            where TOuterEnumerator : IEnumerator<TOuter>
            where TInnerEnumerator : IEnumerator<TInner>
            where TEqualityComparer : struct, IEqualityComparer<TKey>
        {
            return new GroupJoinEnumerable<TOuter, TOuterEnumerator, FuncFunctor<TOuter, TKey>, TInner, TInnerEnumerator, FuncFunctor<TInner, TKey>, TKey, FuncFunctor<TOuter, InlinqArray<TInner>, TResult>, TResult, TEqualityComparer>(
                outer ?? throw Error.ArgumentNull(nameof(outer)), inner ?? throw Error.ArgumentNull(nameof(inner)), new FuncFunctor<TOuter, TKey>(outerKeySelector ?? throw Error.ArgumentNull(nameof(outerKeySelector))),
                new FuncFunctor<TInner, TKey>(innerKeySelector ?? throw Error.ArgumentNull(nameof(innerKeySelector))),
                new FuncFunctor<TOuter, InlinqArray<TInner>, TResult>(resultSelector ?? throw Error.ArgumentNull(nameof(resultSelector))), comparer);
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
                first ?? throw Error.ArgumentNull(nameof(first)), second ?? throw Error.ArgumentNull(nameof(second)), EqualityComparers.Equatable<TSource>());
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
                first ?? throw Error.ArgumentNull(nameof(first)), second ?? throw Error.ArgumentNull(nameof(second)), EqualityComparers.Nullable<TSource>());
        }
        #endregion

        #region Reverse
        public static ReverseEnumerable<TSource, TEnumerator> Reverse<TSource, TEnumerator>(this IEnumerable<TSource, TEnumerator> source)
            where TEnumerator : IEnumerator<TSource>
        {
            return new ReverseEnumerable<TSource, TEnumerator>(source ?? throw Error.ArgumentNull(nameof(source)));
        }
        #endregion

        #region Union
        public static WhereEnumerable<TSource, ConcatEnumerator<TSource, TEnumerator1, TEnumerator2>, DistinctPredicate<TSource, EquatableEqualityComparer<TSource>>>
            Union<TSource, TEnumerator1, TEnumerator2>(this IEnumerable<TSource, TEnumerator1> first, IEnumerable<TSource, TEnumerator2> second)
            where TSource : IEquatable<TSource>
            where TEnumerator1 : IEnumerator<TSource>
            where TEnumerator2 : IEnumerator<TSource>
        {
            return new WhereEnumerable<TSource, ConcatEnumerator<TSource, TEnumerator1, TEnumerator2>, DistinctPredicate<TSource, EquatableEqualityComparer<TSource>>>(
                new ConcatEnumerable<TSource, TEnumerator1, TEnumerator2>(first ?? throw Error.ArgumentNull(nameof(first)), second ?? throw Error.ArgumentNull(nameof(second))),
                new DistinctPredicate<TSource, EquatableEqualityComparer<TSource>>(EqualityComparers.Equatable<TSource>()));
        }

        public static WhereEnumerable<TSource, ConcatEnumerator<TSource, TEnumerator1, TEnumerator2>, DistinctPredicate<TSource, IEqualityComparer<TSource>>>
            Union<TSource, TEnumerator1, TEnumerator2>(this IEnumerable<TSource, TEnumerator1> first, IEnumerable<TSource, TEnumerator2> second, IEqualityComparer<TSource> comparer)
            where TEnumerator1 : IEnumerator<TSource>
            where TEnumerator2 : IEnumerator<TSource>
        {
            return new WhereEnumerable<TSource, ConcatEnumerator<TSource, TEnumerator1, TEnumerator2>, DistinctPredicate<TSource, IEqualityComparer<TSource>>>(
                new ConcatEnumerable<TSource, TEnumerator1, TEnumerator2>(first ?? throw Error.ArgumentNull(nameof(first)), second ?? throw Error.ArgumentNull(nameof(second))),
                new DistinctPredicate<TSource, IEqualityComparer<TSource>>(comparer ?? EqualityComparer<TSource>.Default));
        }

        public static WhereEnumerable<TSource, ConcatEnumerator<TSource, TEnumerator1, TEnumerator2>, DistinctPredicate<TSource, TEqualityComparer>>
            Union<TSource, TEnumerator1, TEnumerator2, TEqualityComparer>(this IEnumerable<TSource, TEnumerator1> first, IEnumerable<TSource, TEnumerator2> second, TEqualityComparer comparer)
            where TEnumerator1 : IEnumerator<TSource>
            where TEnumerator2 : IEnumerator<TSource>
            where TEqualityComparer : struct, IEqualityComparer<TSource>
        {
            return new WhereEnumerable<TSource, ConcatEnumerator<TSource, TEnumerator1, TEnumerator2>, DistinctPredicate<TSource, TEqualityComparer>>(
                new ConcatEnumerable<TSource, TEnumerator1, TEnumerator2>(first ?? throw Error.ArgumentNull(nameof(first)), second ?? throw Error.ArgumentNull(nameof(second))),
                new DistinctPredicate<TSource, TEqualityComparer>(comparer));
        }

        public static WhereEnumerable<string, ConcatEnumerator<string, TEnumerator1, TEnumerator2>, DistinctPredicate<string, Cmp.StringComparer>>
            Union<TEnumerator1, TEnumerator2>(this IEnumerable<string, TEnumerator1> first, IEnumerable<string, TEnumerator2> second)
            where TEnumerator1 : IEnumerator<string>
            where TEnumerator2 : IEnumerator<string>
        {
            return new WhereEnumerable<string, ConcatEnumerator<string, TEnumerator1, TEnumerator2>, DistinctPredicate<string, Cmp.StringComparer>>(
                new ConcatEnumerable<string, TEnumerator1, TEnumerator2>(first ?? throw Error.ArgumentNull(nameof(first)), second ?? throw Error.ArgumentNull(nameof(second))),
                new DistinctPredicate<string, Cmp.StringComparer>(new Cmp.StringComparer()));
        }

        public static WhereEnumerable<TSource?, ConcatEnumerator<TSource?, TEnumerator1, TEnumerator2>, DistinctPredicate<TSource?, NullableEqualityComparer<TSource, EquatableEqualityComparer<TSource>>>>
            Union<TSource, TEnumerator1, TEnumerator2>(this IEnumerable<TSource?, TEnumerator1> first, IEnumerable<TSource?, TEnumerator2> second)
            where TSource : struct, IEquatable<TSource>
            where TEnumerator1 : IEnumerator<TSource?>
            where TEnumerator2 : IEnumerator<TSource?>
        {
            return new WhereEnumerable<TSource?, ConcatEnumerator<TSource?, TEnumerator1, TEnumerator2>, DistinctPredicate<TSource?, NullableEqualityComparer<TSource, EquatableEqualityComparer<TSource>>>>(
                new ConcatEnumerable<TSource?, TEnumerator1, TEnumerator2>(first ?? throw Error.ArgumentNull(nameof(first)), second ?? throw Error.ArgumentNull(nameof(second))),
                new DistinctPredicate<TSource?, NullableEqualityComparer<TSource, EquatableEqualityComparer<TSource>>>(EqualityComparers.Nullable<TSource>()));
        }
        #endregion
    }
}
