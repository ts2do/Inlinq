namespace Inlinq.Sort
{
    public struct PrimaryChainedKey<TKey, TNextAux>
    {
        public TKey key;
        public TNextAux next;
    }
}
