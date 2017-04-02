using System.Collections.Generic;

namespace Inlinq.Impl
{
    public sealed class InlinqSortedDictionaryValueCollection<TKey, TValue> : Collection<TValue, SortedDictionary<TKey, TValue>.ValueCollection.Enumerator>
    {
        private SortedDictionary<TKey, TValue>.ValueCollection source;
        public InlinqSortedDictionaryValueCollection(SortedDictionary<TKey, TValue>.ValueCollection source) => this.source = source;
        public override int Count => source.Count;
        public override bool Contains(TValue item) => ((ICollection<TValue>)source).Contains(item);
        public override void CopyTo(TValue[] array, int arrayIndex) => source.CopyTo(array, arrayIndex);
        public override SortedDictionary<TKey, TValue>.ValueCollection.Enumerator GetEnumerator() => source.GetEnumerator();
    }
}
