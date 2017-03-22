namespace Inlinq.Impl
{
    public sealed class Repeat32Enumerable<T> : Enumerable<T, Repeat32Enumerator<T>>
    {
        private T element;
        private int count;

        public Repeat32Enumerable(T element, int count)
        {
            this.element = element;
            this.count = count;
        }

        public override bool GetCount(out int count)
        {
            count = this.count;
            return true;
        }

        public override Repeat32Enumerator<T> GetEnumerator()
            => new Repeat32Enumerator<T>(element, count);
    }
}
