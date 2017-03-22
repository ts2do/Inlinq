using System.Collections.Generic;

namespace Inlinq.Impl
{
    public sealed class InlinqSortedDictionary<TKey, TValue> : Collection<KeyValuePair<TKey, TValue>, SortedDictionary<TKey, TValue>.Enumerator>
    {
        private SortedDictionary<TKey, TValue> source;
        public InlinqSortedDictionary(SortedDictionary<TKey, TValue> source) => this.source = source;
        public override int Count => source.Count;
        public override bool Contains(KeyValuePair<TKey, TValue> item) => ((ICollection<KeyValuePair<TKey, TValue>>)source).Contains(item);
        public override void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => source.CopyTo(array, arrayIndex);
        public override SortedDictionary<TKey, TValue>.Enumerator GetEnumerator() => source.GetEnumerator();
    }
}
