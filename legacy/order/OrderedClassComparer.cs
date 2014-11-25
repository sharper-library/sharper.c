using System.Collections.Generic;

namespace sharper.c.legacy.order
{
    using data.ordered;

    public struct OrderedClassComparer<A, OrdA>
        : IComparer<A>
        where OrdA : struct, IOrdered<A>
        where A : class
    {
        public int Compare(A x, A y)
        {
            if (ReferenceEquals(null, x) && ReferenceEquals(null, y)) return 0;
            if (ReferenceEquals(null, x)) return -1;
            if (ReferenceEquals(null, y)) return 1;
            return SafeCompare(x, y);
        }

        private int SafeCompare(A x, A y)
        {
            switch (default(OrdA).Compare(x, y))
            {
            case Ordering.LT: return -1;
            case Ordering.GT: return 1;
            default: return 0;
            }
        }
    }
}