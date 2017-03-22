using System;

namespace Inlinq
{
    public struct FuncFunctor<TResult> : ITFunctor<FuncFunctor<TResult>, TResult>
    {
        private Func<TResult> func;
        public FuncFunctor(Func<TResult> func) => this.func = func;
        public TResult Invoke() => func();
        public FuncFunctor<TResult> Unwrap() => this;
    }
}
