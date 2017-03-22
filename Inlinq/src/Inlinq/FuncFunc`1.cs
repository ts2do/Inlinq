using System;

namespace Inlinq
{
    public struct FuncFunc<TResult> : ITFunctor<FuncFunc<TResult>, TResult>
    {
        private Func<TResult> func;
        public FuncFunc(Func<TResult> func) => this.func = func;
        public TResult Invoke() => func();
        public FuncFunc<TResult> Unwrap() => this;
    }
}
