namespace Inlinq.Sort
{
    internal interface ISort<T, TAux>
    {
        void GetAux(ref T element, out TAux aux);
        int Compare(ref T elementX, ref TAux auxX, ref T elementY, ref TAux auxY);
    }
}
