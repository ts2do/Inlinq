using System;

namespace Inlinq
{
    internal static class Util
    {
        public static bool IsStarted(this EnumeratorState state)
            => state == EnumeratorState.Started;

        public static T[] ResizeArray<T>(T[] items, int count, int newSize)
        {
            Array.Resize(ref items, newSize);
            return items;
        }

        public static bool LtUn(this int a, int b)
            => (uint)a < (uint)b;

        public static bool LeUn(this int a, int b)
            => (uint)a <= (uint)b;

        public static bool GeUn(this int a, int b)
            => (uint)a >= (uint)b;

        public static bool GtUn(this int a, int b)
            => (uint)a > (uint)b;

        public static bool LtUn(this long a, long b)
            => (ulong)a < (ulong)b;

        public static bool LeUn(this long a, long b)
            => (ulong)a <= (ulong)b;

        public static bool GeUn(this long a, long b)
            => (ulong)a >= (ulong)b;

        public static bool GtUn(this long a, long b)
            => (ulong)a > (ulong)b;

        public static void Swap<T>(ref T left, ref T right)
        {
            T temp = left;
            left = right;
            right = temp;
        }

        public static Func<T, T> Identity<T>() => IdentityCache<T>.Instance;

        private static class IdentityCache<T>
        {
            public static readonly Func<T, T> Instance = x => x;
        }
    }
}
