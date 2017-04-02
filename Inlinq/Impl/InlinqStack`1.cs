using System.Collections.Generic;

namespace Inlinq.Impl
{
    public sealed class InlinqStack<T> : Collection<T, Stack<T>.Enumerator>
    {
        private Stack<T> source;
        public InlinqStack(Stack<T> source) => this.source = source;
        public override int Count => source.Count;
        public override bool Contains(T item) => source.Contains(item);
        public override void CopyTo(T[] array, int arrayIndex) => source.CopyTo(array, arrayIndex);
        public override Stack<T>.Enumerator GetEnumerator() => source.GetEnumerator();
    }
}
