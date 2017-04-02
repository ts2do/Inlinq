namespace Inlinq.Impl
{
    public sealed class Range32Enumerable : Enumerable<int, Range32Enumerator>
    {
        private int start, count;

        public Range32Enumerable(int start, int count)
        {
            this.start = start;
            this.count = count;
        }

        public override bool GetCount(out int count)
        {
            count = this.count;
            return true;
        }

        public override Range32Enumerator GetEnumerator()
            => new Range32Enumerator(start, count);
    }
}
