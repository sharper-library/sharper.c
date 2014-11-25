using System;
using System.Collections.Generic;
using System.Linq;

namespace sharper.c.data.semigroup
{
    public struct Monoid<S, A>
        where S : struct, IMonoid<A>
    {
        public A Plus(A x, A y)
        {
            return default(S).Plus(x, y);
        }

        public Func<A, A> Plus(A x)
        {
            return y => default(S).Plus(x, y);
        }

        public A Zero
        {
            get { return default(S).Zero; }
        }

        public A Sum(params A[] xs)
        {
            return xs.Aggregate(default(S).Plus);
        }
    }
}
