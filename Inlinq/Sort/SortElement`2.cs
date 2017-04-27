namespace Inlinq.Sort
{
    internal class SortElement<T, TAux> : SortElement<T>
    {
        public SortElement() { }
        public SortElement(T element) : base(element) { }
        public SortElement(T element, TAux aux) : base(element) { this.aux = aux; }
        public TAux aux;
    }
}
