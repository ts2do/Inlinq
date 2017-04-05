using System.Collections.Generic;

namespace Inlinq.Cmp
{
    public struct Int32Comparer : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            if (x < y) return -1;
            if (x > y) return 1;
            return 0;
        }
    }
}
