using System;

namespace Sharper.C
{
    public struct Maybe<A>
    {
        private readonly bool isJust;
        private readonly A value;

        internal Maybe(A value)
        {
            isJust = true;
            this.value = value;
        }

        public B Match<B>(Func<B> nothing, Func<A, B> some)
        {
            return isJust ? some(value) : nothing();
        }

        public Maybe<B> Map<B>(Func<A, B> f)
        {
            return isJust ? new Maybe<B>(f(value)) : new Maybe<B>();
        }

        public Maybe<B> Bind<B>(Func<A, Maybe<B>> f)
        {
            return isJust ? f(value) : new Maybe<B>();
        }

        public Maybe<B> Apply<B>(Maybe<Func<A, B>> f)
        {
            return f.isJust ? Map(f.value) : new Maybe<B>();
        }
    }

    public static class Maybe
    {
        public static Maybe<A> Nothing<A>()
        {
            return new Maybe<A>();
        }

        public static Maybe<A> Just<A>(A value)
        {
            return new Maybe<A>(value);
        }
    }

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
    }
}