using System;
using System.Linq;

namespace sharper.c.data.semigroup
{
    using control.trampoline;

    public struct LazySemigroup<S, A>
        where S : struct, ILazySemigroup<A>
    {
        public Bounce<A> LazyPlus(Bounce<A> x, Bounce<A> y)
        {
            return default(S).LazyPlus(x, y);
        }

        public Func<Bounce<A>, Bounce<A>> LazyPlus(Bounce<A> x)
        {
            return y => default(S).LazyPlus(x, y);
        }

        public Bounce<A> Sum(Bounce<A> x, params Bounce<A>[] xs)
        {
            return default(S).LazyPlus(x, xs.Aggregate(default(S).LazyPlus));
        }
    }
}
