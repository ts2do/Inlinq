using System.Collections.Generic;

namespace Inlinq.Impl
{
    public sealed class InlinqDictionary<TKey, TValue> : Collection<KeyValuePair<TKey, TValue>, Dictionary<TKey, TValue>.Enumerator>
    {
        private Dictionary<TKey, TValue> source;
        public InlinqDictionary(Dictionary<TKey, TValue> source) => this.source = source;
        public override int Count => source.Count;
        public override bool Contains(KeyValuePair<TKey, TValue> item) => ((ICollection<KeyValuePair<TKey, TValue>>)source).Contains(item);
        public override void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => ((ICollection<KeyValuePair<TKey, TValue>>)source).CopyTo(array, arrayIndex);
        public override Dictionary<TKey, TValue>.Enumerator GetEnumerator() => source.GetEnumerator();
    }
}
