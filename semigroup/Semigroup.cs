using System;
using System.Linq;

namespace sharper.c.data.semigroup
{
    public struct Semigroup<S, A>
        where S : struct, ISemigroup<A>
    {
        public A Plus(A x, A y)
        {
            return default(S).Plus(x, y);
        }

        public Func<A, A> Plus(A x)
        {
            return y => default(S).Plus(x, y);
        }

        public A Sum(A x, params A[] xs)
        {
            return default(S).Plus(x, xs.Aggregate(default(S).Plus));
        }
    }
}