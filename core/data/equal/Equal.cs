using System;

namespace sharper.c.data.equal
{
    public struct Equal<E, A>
        where E : struct, IEqual<A>
    {
        public bool Eq(A x, A y)
        {
            return default(E).Eq(x, y);
        }

        public Func<A, bool> Eq(A x)
        {
            return y => default(E).Eq(x, y);
        }

        public bool Neq(A x, A y)
        {
            return !default(E).Eq(x, y);
        }

        public Func<A, bool> Neq(A x)
        {
            return y => !default(E).Eq(x, y);
        }
    }
}