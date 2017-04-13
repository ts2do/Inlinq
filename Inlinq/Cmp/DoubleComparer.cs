using System.Collections.Generic;

#pragma warning disable CS1718 // Comparison made to same variable

namespace Inlinq.Cmp
{
    public struct DoubleComparer : IComparer<double>, IEqualityComparer<double>
    {
        public int Compare(double x, double y)
        {
            if (x >= y) return x > y ? 1 : 0;
            if (x < y) return -1;
            if (IsNaN(y)) return !IsNaN(x) ? 1 : 0;
            return -1;
        }

        public bool Equals(double x, double y) => x == y || (IsNaN(x) && IsNaN(y));
        public int GetHashCode(double obj) => obj.GetHashCode();

        private static bool IsNaN(double f) => f != f;
    }
}
