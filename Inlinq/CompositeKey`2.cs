namespace Inlinq
{
    public struct CompositeKey<TKey1, TKey2>
    {
        internal TKey1 first;
        internal TKey2 second;
        public TKey1 First { get => first; set => first = value; }
        public TKey2 Second { get => second; set => second = value; }

        public CompositeKey(TKey1 first, TKey2 second)
        {
            this.first = first;
            this.second = second;
        }
    }
}
