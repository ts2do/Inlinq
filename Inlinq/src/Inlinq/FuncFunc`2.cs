using System;

namespace Inlinq
{
    public struct FuncFunc<T, TResult> : ITFunctor<FuncFunc<T, TResult>, T, TResult>
    {
        private Func<T, TResult> func;
        public FuncFunc(Func<T, TResult> func) => this.func = func;
        public TResult Invoke(T arg) => func(arg);
        public FuncFunc<T, TResult> Unwrap() => this;
    }
}
