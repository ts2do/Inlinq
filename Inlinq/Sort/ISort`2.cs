namespace Inlinq.Sort
{
    internal interface ISort<T, TData>
    {
        void GetData(ref T element, out TData data);
        int Compare(ref T elementX, ref TData dataX, ref T elementY, ref TData dataY);
    }
}
