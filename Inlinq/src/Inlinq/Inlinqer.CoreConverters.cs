using System;
using System.Collections.Generic;
using Inlinq.Impl;

namespace Inlinq
{
    public static partial class Inlinqer
    {
        public static IEnumerable<T, TEnumerator> AsInlinq<T, TEnumerable, TEnumerator>(this IEnumerable<T, TEnumerator> source)
            where TEnumerator : IEnumerator<T>
            => source;

        public static InlinqArray<T> AsInlinq<T>(this T[] source)
            => source != null ? new InlinqArray<T>(source, 0, source.Length) : throw Error.ArgumentNull(nameof(source));

        public static InlinqArray<T> AsInlinq<T>(this T[] source, int startIndex, int length)
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (startIndex.GeUn(source.Length)) throw Error.ArgumentOutOfRange(nameof(startIndex));
            if (length.GtUn(source.Length - startIndex)) throw Error.ArgumentOutOfRange(nameof(length));
            return new InlinqArray<T>(source, startIndex, length);
        }

        public static InlinqArray<T> AsInlinq<T>(this ArraySegment<T> source)
            => source.Array != null ? new InlinqArray<T>(source.Array, source.Offset, source.Count) : throw Error.ArgumentNull("source.Array");

        public static InlinqEnumerable1<T, IEnumerable<T>> AsInlinq<T>(this IEnumerable<T> source)
            => new InlinqEnumerable1<T, IEnumerable<T>>(source ?? throw Error.ArgumentNull(nameof(source)));

        public static InlinqEnumerable2<T, TEnumerator, FuncFunctor<TEnumerator>> AsInlinq<T, TEnumerator>(this IEnumerable<T> source, Func<TEnumerator> method)
            where TEnumerator : IEnumerator<T>
            => new InlinqEnumerable2<T, TEnumerator, FuncFunctor<TEnumerator>>(new FuncFunctor<TEnumerator>(method ?? throw Error.ArgumentNull(nameof(method))));

        public static InlinqEnumerable2<T, TEnumerator, TMethod> AsInlinq<T, TEnumerator, TMethod>(this IEnumerable<T> source, TMethod method)
            where TEnumerator : IEnumerator<T>
            where TMethod : IFunctor<TEnumerator>
            => method != null ? new InlinqEnumerable2<T, TEnumerator, TMethod>(method) : throw Error.ArgumentNull(nameof(method));

        public static InlinqEnumerable3<T, TEnumerator, IEnumerable<T>, FuncFunctor<IEnumerable<T>, TEnumerator>> AsInlinq<T, TEnumerator>(this IEnumerable<T> source, Func<IEnumerable<T>, TEnumerator> method)
            where TEnumerator : IEnumerator<T>
        {
            return new InlinqEnumerable3<T, TEnumerator, IEnumerable<T>, FuncFunctor<IEnumerable<T>, TEnumerator>>(
                source ?? throw Error.ArgumentNull(nameof(source)), new FuncFunctor<IEnumerable<T>, TEnumerator>(method ?? throw Error.ArgumentNull(nameof(method))));
        }

        public static InlinqCollection1<T> AsInlinq<T>(this ICollection<T> source)
            => new InlinqCollection1<T>(source ?? throw Error.ArgumentNull(nameof(source)));

        public static InlinqCollection2<T, TEnumerator, FuncFunctor<TEnumerator>> AsInlinq<T, TEnumerator>(this ICollection<T> source, Func<TEnumerator> method)
            where TEnumerator : IEnumerator<T>
        {
            return new InlinqCollection2<T, TEnumerator, FuncFunctor<TEnumerator>>(
                source ?? throw Error.ArgumentNull(nameof(source)), new FuncFunctor<TEnumerator>(method ?? throw Error.ArgumentNull(nameof(method))));
        }

        public static InlinqCollection2<T, TEnumerator, TMethod> AsInlinq<T, TEnumerator, TMethod>(this ICollection<T> source, TMethod method)
            where TEnumerator : IEnumerator<T>
            where TMethod : IFunctor<TEnumerator>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (method == null) throw Error.ArgumentNull(nameof(method));
            return new InlinqCollection2<T, TEnumerator, TMethod>(source, method);
        }

        public static InlinqCollection3<T, TEnumerator, ICollection<T>, FuncFunctor<ICollection<T>, TEnumerator>> AsInlinq<T, TEnumerator>(this ICollection<T> source, Func<ICollection<T>, TEnumerator> method)
            where TEnumerator : IEnumerator<T>
        {
            return new InlinqCollection3<T, TEnumerator, ICollection<T>, FuncFunctor<ICollection<T>, TEnumerator>>(
                source ?? throw Error.ArgumentNull(nameof(source)), new FuncFunctor<ICollection<T>, TEnumerator>(method ?? throw Error.ArgumentNull(nameof(method))));
        }

        public static InlinqList1<T> AsInlinq<T>(this IList<T> source)
            => new InlinqList1<T>(source ?? throw Error.ArgumentNull(nameof(source)));

        public static InlinqList2<T, TEnumerator, FuncFunctor<TEnumerator>> AsInlinq<T, TEnumerator>(this IList<T> source, Func<TEnumerator> method)
            where TEnumerator : IEnumerator<T>
        {
            return new InlinqList2<T, TEnumerator, FuncFunctor<TEnumerator>>(
                source ?? throw Error.ArgumentNull(nameof(source)), new FuncFunctor<TEnumerator>(method ?? throw Error.ArgumentNull(nameof(method))));
        }

        public static InlinqList2<T, TEnumerator, TMethod> AsInlinq<T, TEnumerator, TMethod>(this IList<T> source, TMethod method)
            where TEnumerator : IEnumerator<T>
            where TMethod : IFunctor<TEnumerator>
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (method == null) throw Error.ArgumentNull(nameof(method));
            return new InlinqList2<T, TEnumerator, TMethod>(source, method);
        }

        public static InlinqList3<T, TEnumerator, IList<T>, FuncFunctor<IList<T>, TEnumerator>> AsInlinq<T, TEnumerator>(this IList<T> source, Func<IList<T>, TEnumerator> method)
            where TEnumerator : IEnumerator<T>
        {
            return new InlinqList3<T, TEnumerator, IList<T>, FuncFunctor<IList<T>, TEnumerator>>(
                source ?? throw Error.ArgumentNull(nameof(source)), new FuncFunctor<IList<T>, TEnumerator>(method ?? throw Error.ArgumentNull(nameof(method))));
        }

        public static InlinqDictionary<TKey, TValue> AsInlinq<TKey, TValue>(this Dictionary<TKey, TValue> source)
            => new InlinqDictionary<TKey, TValue>(source ?? throw Error.ArgumentNull(nameof(source)));

        public static InlinqDictionaryKeyCollection<TKey, TValue> AsInlinq<TKey, TValue>(this Dictionary<TKey, TValue>.KeyCollection source)
            => new InlinqDictionaryKeyCollection<TKey, TValue>(source ?? throw Error.ArgumentNull(nameof(source)));

        public static InlinqDictionaryValueCollection<TKey, TValue> AsInlinq<TKey, TValue>(this Dictionary<TKey, TValue>.ValueCollection source)
            => new InlinqDictionaryValueCollection<TKey, TValue>(source ?? throw Error.ArgumentNull(nameof(source)));

        public static InlinqList<T> AsInlinq<T>(this List<T> source)
            => new InlinqList<T>(source ?? throw Error.ArgumentNull(nameof(source)));
    }
}
