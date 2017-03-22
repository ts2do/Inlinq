using System.Collections.Generic;

namespace Inlinq.Impl
{
    public sealed class ConcatEnumerable<T, TEnumerator1, TEnumerator2> : Enumerable<T, ConcatEnumerator<T, TEnumerator1, TEnumerator2>>
        where TEnumerator1 : IEnumerator<T>
        where TEnumerator2 : IEnumerator<T>
    {
        private IEnumerable<T, TEnumerator1> first;
        private IEnumerable<T, TEnumerator2> second;

        public ConcatEnumerable(IEnumerable<T, TEnumerator1> first, IEnumerable<T, TEnumerator2> second)
        {
            this.first = first;
            this.second = second;
        }

        public override bool GetCount(out int count)
        {
            if (!first.GetCount(out int firstCount) || !second.GetCount(out int secondCount))
            {
                count = 0;
                return false;
            }
            count = firstCount + secondCount;
            return true;
        }

        public override ConcatEnumerator<T, TEnumerator1, TEnumerator2> GetEnumerator()
        {
            TEnumerator1 firstEnumerator = first.GetEnumerator();
            try
            {
                return new ConcatEnumerator<T, TEnumerator1, TEnumerator2>(firstEnumerator, second.GetEnumerator());
            }
            catch
            {
                firstEnumerator.Dispose();
                throw;
            }
        }
    }
}
