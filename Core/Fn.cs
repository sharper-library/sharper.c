using System;

namespace Sharper.C
{
    public static class Fn
    {
        public static A Id<A>(A a)
        {
            return a;
        }

        public static Func<B, A> Const<A, B>(A a)
        {
            return b => a;
        }

        public static Func<A, C> Comp<A, B, C>(Func<B, C> f, Func<A, B> g)
        {
            return a => f(g(a));
        }

        public static Func<A, Func<B, C>> Curry<A, B, C>(Func<A, B, C> f)
        {
            return a => b => f(a, b);
        }

        public static Func<A, B, C> Uncurry<A, B, C>(Func<A, Func<B, C>> f)
        {
            return (a, b) => f(a)(b);
        }
    }
}
