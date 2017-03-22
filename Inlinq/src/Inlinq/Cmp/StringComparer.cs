using System.Collections.Generic;

namespace Inlinq.Cmp
{
    public struct StringComparer : IEqualityComparer<string>, IComparer<string>
    {
        public bool Equals(string x, string y)
            => x?.Equals(y) ?? y == null;
        public int GetHashCode(string obj)
            => obj?.GetHashCode() ?? 0;
        public override bool Equals(object obj)
            => obj is StringComparer;
        public override int GetHashCode()
            => typeof(StringComparer).Name.GetHashCode();
        public int Compare(string x, string y)
            => x?.CompareTo(y) ?? (y != null ? -1 : 0);
    }
}
