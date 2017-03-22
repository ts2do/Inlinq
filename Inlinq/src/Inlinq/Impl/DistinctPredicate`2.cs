using System.Collections.Generic;

namespace Inlinq.Impl
{
    public struct DistinctPredicate<T, TEqualityComparer> : IFunctor<T, bool>
        where TEqualityComparer : IEqualityComparer<T>
    {
        private Set<T, TEqualityComparer> set;
        public DistinctPredicate(TEqualityComparer comparer)
            => set = new Set<T, TEqualityComparer>(comparer);
        public bool Invoke(T arg) => set.Add(arg);
    }
}
