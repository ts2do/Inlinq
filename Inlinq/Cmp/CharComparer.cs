using System.Collections.Generic;

namespace Inlinq.Cmp
{
    public struct CharComparer : IComparer<char>, IEqualityComparer<char>
    {
        public int Compare(char x, char y) => x - y;
        public bool Equals(char x, char y) => x == y;
        public int GetHashCode(char obj) => obj;
    }
}
