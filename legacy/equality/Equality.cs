using System.Collections.Generic;

namespace sharper.c.legacy.equality
{
    using data.equal;
    using data.hashable;

    public static class Equality
    {
        public static IEqualityComparer<A> Comparer<A, EqA, HashA>()
            where EqA : struct, IEqual<A>
            where HashA: struct, IHashable<A>
        {
            return default(EqualHashableEqualityComparer<A, EqA, HashA>);
        }

        public static IEqualityComparer<A> Comparer<A, EqHashA>()
            where EqHashA : struct, IEqual<A>, IHashable<A>
        {
            return default(EqualHashableEqualityComparer<A, EqHashA, EqHashA>);
        }
    }
}