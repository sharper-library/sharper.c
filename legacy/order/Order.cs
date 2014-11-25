using System.Collections.Generic;

namespace sharper.c.legacy.order
{
    using data.ordered;

    public static class Order
    {
        public static IComparer<A> StructComparer<A, OrdA>()
            where OrdA : struct, IOrdered<A>
            where A : struct
        {

            return default(OrderedStructComparer<A, OrdA>);
        }

        public static IComparer<A> ClassComparer<A, OrdA>()
            where OrdA : struct, IOrdered<A>
            where A : class
        {
            return default(OrderedClassComparer<A, OrdA>);
        }
    }
}