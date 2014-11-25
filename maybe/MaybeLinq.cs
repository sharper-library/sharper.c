using System;

namespace sharper.c.data.maybe
{
    using data.function;

    public static class MaybeLinq
    {
        public static Maybe<B> Select<A, B>(this Maybe<A> m, Func<A, B> f)
        {
            return m.Map(f);
        }

        public static Maybe<C> SelectMany<A, B, C>(
                this Maybe<A> m,
                Func<A, Maybe<B>> f,
                Func<A, B, C> g)
        {
            return m.Bind(a => f(a).Map(b => g(a, b)));
        }

        public static Maybe<C> Join<K, A, B, C>(
                this Maybe<A> a,
                Maybe<B> b,
                Func<A, K> _,
                Func<B, K> __,
                Func<A, B, C> f)
        {
            return b.Apply(a.Map(Fn.Curry(f)));
        }

        public static Maybe<A> Where<A>(this Maybe<A> a, Func<A, bool> p)
        {
            return a.Filter(p);
        }
    }
}