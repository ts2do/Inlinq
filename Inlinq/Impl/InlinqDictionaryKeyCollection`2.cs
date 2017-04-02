using System.Collections.Generic;

namespace Inlinq.Impl
{
    public sealed class InlinqDictionaryKeyCollection<TKey, TValue> : Collection<TKey, Dictionary<TKey, TValue>.KeyCollection.Enumerator>
    {
        private Dictionary<TKey, TValue>.KeyCollection source;
        public InlinqDictionaryKeyCollection(Dictionary<TKey, TValue>.KeyCollection source) => this.source = source;
        public override int Count => source.Count;
        public override bool Contains(TKey item) => ((ICollection<TKey>)source).Contains(item);
        public override void CopyTo(TKey[] array, int arrayIndex) => source.CopyTo(array, arrayIndex);
        public override Dictionary<TKey, TValue>.KeyCollection.Enumerator GetEnumerator() => source.GetEnumerator();
    }
}
