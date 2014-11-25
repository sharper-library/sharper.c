using System.Collections.Generic;

namespace sharper.c.legacy.order
{
    using data.ordered;

    public struct OrderedStructComparer<A, OrdA>
        : IComparer<A>
        where OrdA : struct, IOrdered<A>
        where A : struct
    {
        public int Compare(A x, A y)
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