namespace Inlinq.Sort
{
    internal interface ISecondarySort<T, TAux> : ISecondarySort<T>, ISort<T, TAux>
    {
        int Compare(ref T elementX, ref TAux auxX, ref T elementY, ref TAux auxY);
    }
}
