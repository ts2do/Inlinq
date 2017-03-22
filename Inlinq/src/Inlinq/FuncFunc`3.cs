using System;

namespace Inlinq
{
    public struct FuncFunc<T1, T2, TResult> : ITFunctor<FuncFunc<T1, T2, TResult>, T1, T2, TResult>
    {
        private Func<T1, T2, TResult> func;
        public FuncFunc(Func<T1, T2, TResult> func) => this.func = func;
        public TResult Invoke(T1 arg1, T2 arg2) => func(arg1, arg2);
        public FuncFunc<T1, T2, TResult> Unwrap() => this;
    }
}
