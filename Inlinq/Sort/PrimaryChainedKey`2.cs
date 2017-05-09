namespace Inlinq.Sort
{
    public struct PrimaryChainedKey<TKey, TNextData>
    {
        public TKey key;
        public TNextData next;
    }
}
