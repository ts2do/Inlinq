namespace Inlinq.Impl
{
    public sealed class EmptyEnumerable<T> : Enumerable<T, EmptyEnumerator<T>>
    {
        public static readonly EmptyEnumerable<T> Instance = new EmptyEnumerable<T>();

        private EmptyEnumerable() { }

        public override bool GetCount(out int count)
        {
            count = 0;
            return true;
        }

        public override EmptyEnumerator<T> GetEnumerator()
            => new EmptyEnumerator<T>();
    }
}
