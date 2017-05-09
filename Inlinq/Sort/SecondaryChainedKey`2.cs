namespace Inlinq.Sort
{
    public struct SecondaryChainedKey<TKey, TNextData>
    {
        public TKey key;
        public bool keySet;
        public TNextData next;
    }
}
