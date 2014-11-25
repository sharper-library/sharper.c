using System.Collections.Generic;

namespace sharper.c.legacy.equality
{
    using data.equal;
    using data.hashable;

    internal struct EqualHashableEqualityComparer<A, EqA, HashA>
        : IEqualityComparer<A>
        where EqA : struct, IEqual<A>
        where HashA : struct, IHashable<A>
    {
        public bool Equals(A x, A y)
        {
            return default(EqA).Eq(x, y);
        }

        public int GetHashCode(A a)
        {
            return default(HashA).Hash(a);
        }
    }
}