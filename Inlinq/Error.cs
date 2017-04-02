using System;
using System.Runtime.CompilerServices;

namespace Inlinq
{
    internal static class Error
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static Exception ArgumentNull(string s) => new ArgumentNullException(s);

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static Exception ArgumentOutOfRange(string s) => new ArgumentOutOfRangeException(s);

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static Exception MoreThanOneElement() => new InvalidOperationException("Sequence contains more than one element");

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static Exception MoreThanOneMatch() => new InvalidOperationException("Sequence contains more than one matching element");

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static Exception NoElements() => new InvalidOperationException("Sequence contains no elements");

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static Exception NoMatch() => new InvalidOperationException("Sequence contains no matching element");

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static Exception NotSupported() => new NotSupportedException();

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static Exception EnumerableStateException(bool before) => new InvalidOperationException(before ? "Enumeration not started" : "Enumeration ended");

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static Exception EnumerableStateException(EnumeratorState state) => EnumerableStateException(state == EnumeratorState.Initial);
    }
}