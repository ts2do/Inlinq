namespace Inlinq.Sort
{
    internal struct SortedArray<T>
    {
        public SortElement<T>[] items;
        public int count;

        public SortedArray(SortElement<T>[] items, int count)
        {
            this.items = items;
            this.count = count;
        }
    }
}
