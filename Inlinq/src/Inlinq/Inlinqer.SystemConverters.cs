using System.Collections.Generic;
using Inlinq.Impl;

namespace Inlinq
{
    public static partial class Inlinq
    {
        public static InlinqLinkedList<T> AsInlinq<T>(this LinkedList<T> source)
            => new InlinqLinkedList<T>(source ?? throw Error.ArgumentNull(nameof(source)));

        public static InlinqQueue<T> AsInlinq<T>(this Queue<T> source)
            => new InlinqQueue<T>(source ?? throw Error.ArgumentNull(nameof(source)));

        public static InlinqSortedDictionary<TKey, TValue> AsInlinq<TKey, TValue>(this SortedDictionary<TKey, TValue> source)
            => new InlinqSortedDictionary<TKey, TValue>(source ?? throw Error.ArgumentNull(nameof(source)));

        public static InlinqSortedDictionaryKeyCollection<TKey, TValue> AsInlinq<TKey, TValue>(this SortedDictionary<TKey, TValue>.KeyCollection source)
            => new InlinqSortedDictionaryKeyCollection<TKey, TValue>(source ?? throw Error.ArgumentNull(nameof(source)));

        public static InlinqSortedDictionaryValueCollection<TKey, TValue> AsInlinq<TKey, TValue>(this SortedDictionary<TKey, TValue>.ValueCollection source)
            => new InlinqSortedDictionaryValueCollection<TKey, TValue>(source ?? throw Error.ArgumentNull(nameof(source)));

        public static InlinqSortedSet<T> AsInlinq<T>(this SortedSet<T> source)
            => new InlinqSortedSet<T>(source ?? throw Error.ArgumentNull(nameof(source)));

        public static InlinqStack<T> AsInlinq<T>(this Stack<T> source)
            => new InlinqStack<T>(source ?? throw Error.ArgumentNull(nameof(source)));
    }
}
