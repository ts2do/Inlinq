namespace Inlinq.Impl
{
    public sealed class Repeat64Enumerable<T> : Enumerable<T, Repeat64Enumerator<T>>
    {
        private T element;
        private long count;

        public Repeat64Enumerable(T element, long count)
        {
            this.element = element;
            this.count = count;
        }

        public override bool GetCount(out int count)
        {
            if (this.count <= int.MaxValue)
            {
                count = (int)this.count;
                return true;
            }
            else
            {
                count = 0;
                return false;
            }
        }

        public override Repeat64Enumerator<T> GetEnumerator()
            => new Repeat64Enumerator<T>(element, count);
    }
}
