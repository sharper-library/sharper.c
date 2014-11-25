using System;

namespace sharper.c.data.function
{
    using control.trampoline;

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

        public static Func<B, A, C> Flip<A, B, C>(Func<A, B, C> f)
        {
            return (b, a) => f(a, b);
        }

        public static Func<A, C> Comp<A, B, C>(Func<B, C> f, Func<A, B> g)
        {
            return a => f(g(a));
        }

        public static Func<Func<A, B>, Func<A, C>> Comp<A, B, C>(Func<B, C> f)
        {
            return g => a => f(g(a));
        }

        public static Func<A, Func<B, C>> Curry<A, B, C>(Func<A, B, C> f)
        {
            return a => b => f(a, b);
        }

        public static Func<A, B, C> Uncurry<A, B, C>(Func<A, Func<B, C>> f)
        {
            return (a, b) => f(a)(b);
        }

        public static Func<A, Bounce<B>>
        Fix<A, B>(
                Func<Func<A, Bounce<B>>, Func<A, Bounce<B>>> f,
                uint max = DefaultRecurMaxFrames)
        {
            return Fix(f, max, max);
        }

        public static Func<A, Bounce<B>>
        Fix<A, B>(
                Func<Func<A, Bounce<B>>, Func<A, Bounce<B>>> f,
                uint n,
                uint max)
        {
            return x =>
                f(n == 0
                  ? a => Bounce.Next(() => Fix(f, max, max)(a))
                  : Fix(f, n - 1, max))
                (x);
        }

        public static Func<A, Bounce<B>> Recur<A, B>(
                Func<Func<A, Bounce<B>>, Func<A, Bounce<B>>> f,
                uint max = DefaultRecurMaxFrames)
        {
            return a => Fix<A, B>(f, max)(a);
        }

        public const uint DefaultRecurMaxFrames = 128;
    }
}
