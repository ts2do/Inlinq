using Inlinq.Impl;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Inlinq
{
    public sealed class Lookup<TKey, TElement, TEqualityComparer> : Enumerable<Grouping<TKey, TElement>, GroupingEnumerator<TKey, TElement>>, ILookup<TKey, TElement>
        where TEqualityComparer : IEqualityComparer<TKey>
    {
        private TEqualityComparer comparer;
        private Grouping<TKey, TElement>[] groupings;
        private Grouping<TKey, TElement> lastGrouping;
        private int count;
        private int capacity;
        private Grouping<TKey, TElement> emptyGrouping;

        internal static Lookup<TKey, TElement, TEqualityComparer> Create<TSource>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, TEqualityComparer comparer)
        {
            var lookup = new Lookup<TKey, TElement, TEqualityComparer>(comparer);
            foreach (TSource item in source)
                lookup.FindCreateGrouping(keySelector(item)).Add(elementSelector(item));
            return lookup;
        }

        internal static Lookup<TKey, TElement, TEqualityComparer> CreateForJoin(IEnumerable<TElement> source, Func<TElement, TKey> keySelector, TEqualityComparer comparer)
        {
            var lookup = new Lookup<TKey, TElement, TEqualityComparer>(comparer);
            foreach (TElement item in source)
            {
                TKey key = keySelector(item);
                if (key != null)
                    lookup.FindCreateGrouping(key).Add(item);
            }
            return lookup;
        }

        private Lookup(TEqualityComparer comparer)
        {
            this.comparer = comparer;
            groupings = new Grouping<TKey, TElement>[7];
            capacity = 7;
        }

        public int Count => count;

        public Grouping<TKey, TElement> this[TKey key]
        {
            get
            {
                int hashCode = GetHashCode(key);
                for (Grouping<TKey, TElement> g = groupings[hashCode % capacity]; g != null; g = g.hashNext)
                    if (g.hashCode == hashCode && comparer.Equals(g.key, key))
                        return g;
                return emptyGrouping ?? (emptyGrouping = new Grouping<TKey, TElement>());
            }
        }

        public bool Contains(TKey key)
        {
            int hashCode = GetHashCode(key);
            for (Grouping<TKey, TElement> g = groupings[hashCode % capacity]; g != null; g = g.hashNext)
                if (g.hashCode == hashCode && comparer.Equals(g.key, key))
                    return true;
            return false;
        }

        public override bool GetCount(out int count)
        {
            count = this.count;
            return true;
        }

        public override GroupingEnumerator<TKey, TElement> GetEnumerator() => new GroupingEnumerator<TKey, TElement>(lastGrouping);

        private int GetHashCode(TKey key)
            => key != null ? comparer.GetHashCode(key) & 0x7FFFFFFF : 0;

        private Grouping<TKey, TElement> FindCreateGrouping(TKey key)
        {
            int hashCode = GetHashCode(key);
            Grouping<TKey, TElement> g;
            for (g = groupings[hashCode % capacity]; g != null; g = g.hashNext)
                if (g.hashCode == hashCode && comparer.Equals(g.key, key))
                    return g;

            if (count == capacity) Resize();
            int index = hashCode % capacity;
            ref Grouping<TKey, TElement> listRef = ref groupings[index];
            g = new Grouping<TKey, TElement>
            {
                key = key,
                hashCode = hashCode,
                elements = new TElement[1],
                hashNext = listRef
            };
            listRef = g;
            if (lastGrouping == null)
            {
                g.next = g;
            }
            else
            {
                g.next = lastGrouping.next;
                lastGrouping.next = g;
            }
            lastGrouping = g;
            ++count;
            return g;
        }

        private void Resize()
        {
            int newCapacity = checked(count * 2 + 1);
            var newGroupings = new Grouping<TKey, TElement>[newCapacity];
            Grouping<TKey, TElement> g = lastGrouping;
            do
            {
                g = g.next;
                ref Grouping<TKey, TElement> listRef = ref newGroupings[g.hashCode % newCapacity];
                g.hashNext = listRef;
                listRef = g;
            } while (g != lastGrouping);
            groupings = newGroupings;
            capacity = newCapacity;
        }

        IEnumerable<TElement> ILookup<TKey, TElement>.this[TKey key] => this[key];

        IEnumerator<IGrouping<TKey, TElement>> IEnumerable<IGrouping<TKey, TElement>>.GetEnumerator() => GetEnumerator();
    }
}
