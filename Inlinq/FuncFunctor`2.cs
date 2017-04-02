using System;

namespace Inlinq
{
    public struct FuncFunctor<T, TResult> : ITFunctor<FuncFunctor<T, TResult>, T, TResult>
    {
        private Func<T, TResult> func;
        public FuncFunctor(Func<T, TResult> func) => this.func = func;
        public TResult Invoke(T arg) => func(arg);
        public FuncFunctor<T, TResult> Unwrap() => this;
    }
}
