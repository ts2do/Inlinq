using System.Collections.Generic;

namespace Inlinq.Impl
{
    public sealed class InlinqDictionaryValueCollection<TKey, TValue> : Collection<TValue, Dictionary<TKey, TValue>.ValueCollection.Enumerator>
    {
        private Dictionary<TKey, TValue>.ValueCollection source;
        public InlinqDictionaryValueCollection(Dictionary<TKey, TValue>.ValueCollection source) => this.source = source;
        public override int Count => source.Count;
        public override bool Contains(TValue item) => ((ICollection<TValue>)source).Contains(item);
        public override void CopyTo(TValue[] array, int arrayIndex) => source.CopyTo(array, arrayIndex);
        public override Dictionary<TKey, TValue>.ValueCollection.Enumerator GetEnumerator() => source.GetEnumerator();
    }
}
