using System.Collections.Generic;

namespace Inlinq.Impl
{
    public sealed class InlinqSortedDictionaryKeyCollection<TKey, TValue> : Collection<TKey, SortedDictionary<TKey, TValue>.KeyCollection.Enumerator>
    {
        private SortedDictionary<TKey, TValue>.KeyCollection source;
        public InlinqSortedDictionaryKeyCollection(SortedDictionary<TKey, TValue>.KeyCollection source) => this.source = source;
        public override int Count => source.Count;
        public override bool Contains(TKey item) => ((ICollection<TKey>)source).Contains(item);
        public override void CopyTo(TKey[] array, int arrayIndex) => source.CopyTo(array, arrayIndex);
        public override SortedDictionary<TKey, TValue>.KeyCollection.Enumerator GetEnumerator() => source.GetEnumerator();
    }
}
