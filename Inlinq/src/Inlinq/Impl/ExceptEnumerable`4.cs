using System.Collections.Generic;

namespace Inlinq.Impl
{
    public sealed class ExceptEnumerable<T, TEnumerator1, TEnumerator2, TEqualityComparer> : Enumerable<T, ExceptEnumerator<T, TEnumerator1, TEnumerator2, TEqualityComparer>>
        where TEnumerator1 : IEnumerator<T>
        where TEnumerator2 : IEnumerator<T>
        where TEqualityComparer : IEqualityComparer<T>
    {
        private IEnumerable<T, TEnumerator1> first;
        private IEnumerable<T, TEnumerator2> second;
        private TEqualityComparer comparer;

        public ExceptEnumerable(IEnumerable<T, TEnumerator1> first, IEnumerable<T, TEnumerator2> second, TEqualityComparer comparer)
        {
            this.first = first;
            this.second = second;
            this.comparer = comparer;
        }

        public override ExceptEnumerator<T, TEnumerator1, TEnumerator2, TEqualityComparer> GetEnumerator()
        {
            TEnumerator1 firstEnumerator = first.GetEnumerator();
            try
            {
                return new ExceptEnumerator<T, TEnumerator1, TEnumerator2, TEqualityComparer>(firstEnumerator, second.GetEnumerator(), comparer);
            }
            catch
            {
                firstEnumerator.Dispose();
                throw;
            }
        }
    }
}
