using System;

namespace Inlinq
{
    public struct FuncFunctor<T1, T2, TResult> : ITFunctor<FuncFunctor<T1, T2, TResult>, T1, T2, TResult>
    {
        private Func<T1, T2, TResult> func;
        public FuncFunctor(Func<T1, T2, TResult> func) => this.func = func;
        public TResult Invoke(T1 arg1, T2 arg2) => func(arg1, arg2);
        public FuncFunctor<T1, T2, TResult> Unwrap() => this;
    }
}
