namespace Inlinq.Impl
{
    public sealed class Range64Enumerable : Enumerable<long, Range64Enumerator>
    {
        private long start, count;

        public Range64Enumerable(long start, long count)
        {
            this.start = start;
            this.count = count;
        }

        public override bool GetCount(out int count)
        {
            if (this.count <= int.MaxValue)
            {
                count = (int)this.count;
                return true;
            }
            count = 0;
            return false;
        }

        public override Range64Enumerator GetEnumerator()
            => new Range64Enumerator(start, count);
    }
}
