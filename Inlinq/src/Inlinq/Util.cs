using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Inlinq
{
    internal static class Util
    {
        public static void AssertState(this EnumeratorState state)
        {
            if (state != EnumeratorState.Started)
                ThrowStateException(state);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void ThrowStateException(EnumeratorState state)
        {
            throw Error.EnumerableStateException(state == EnumeratorState.Initial);
        }

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
    }
}
