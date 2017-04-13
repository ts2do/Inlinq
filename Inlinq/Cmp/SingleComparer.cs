using System.Collections.Generic;

#pragma warning disable CS1718 // Comparison made to same variable

namespace Inlinq.Cmp
{
    public struct SingleComparer : IComparer<float>, IEqualityComparer<float>
    {
        public int Compare(float x, float y)
        {
            if (x >= y) return x > y ? 1 : 0;
            if (x < y) return -1;
            if (IsNaN(y)) return !IsNaN(x) ? 1 : 0;
            return -1;
        }

        public bool Equals(float x, float y) => x == y || (IsNaN(x) && IsNaN(y));
        public int GetHashCode(float obj) => obj.GetHashCode();

        private static bool IsNaN(float f) => f != f;
    }
}
