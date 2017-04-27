namespace Inlinq.Sort
{
    public struct SecondaryChainedKey<TKey, TNextAux>
    {
        public TKey key;
        public bool keySet;
        public TNextAux next;
    }
}
