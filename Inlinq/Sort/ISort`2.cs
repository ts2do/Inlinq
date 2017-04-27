namespace Inlinq.Sort
{
    internal interface ISort<T, TAux>
    {
        void GetAux(ref T element, out TAux aux);
    }
}
