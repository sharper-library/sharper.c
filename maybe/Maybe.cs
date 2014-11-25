using System;

namespace sharper.c.data.maybe
{
    using data.function;
    using data.lazy;
    using data.tuple;

    public struct Maybe<A>
    {
        internal readonly bool isJust;
        internal readonly A value;

        internal Maybe(A value)
        {
            isJust = true;
            this.value = value;
        }

        public bool IsJust
        {
            get { return isJust; }
        }

        public bool IsNothing
        {
            get { return !isJust; }
        }

        public B Match<B>(Func<B> nothing, Func<A, B> some)
        {
            return isJust ? some(value) : nothing();
        }

        public Maybe<B> Map<B>(Func<A, B> f)
        {
            return isJust ? new Maybe<B>(f(value)) : default(Maybe<B>);
        }

        public Maybe<B> Bind<B>(Func<A, Maybe<B>> f)
        {
            return isJust ? f(value) : default(Maybe<B>);
        }

        public Maybe<B> Apply<B>(Maybe<Func<A, B>> f)
        {
            return f.isJust ? Map(f.value) : default(Maybe<B>);
        }

        public Maybe<Pair<A, B>> Zip<B>(Maybe<B> other)
        {
            return ZipWith(other, Pair.Mk);
        }

        public Maybe<C> ZipWith<B, C>(Maybe<B> other, Func<A, B, C> f)
        {
            return other.Apply(Map(Fn.Curry(f)));
        }

        public Maybe<A> Filter(Func<A, bool> p)
        {
            return isJust && p(value) ? this : default(Maybe<A>);
        }

        public A ValueOr(Lazy<A> or)
        {
            return isJust ? value : or.Force;
        }

        public A ValueOr(Func<A> or)
        {
            return isJust ? value : or();
        }

        public Maybe<A> JustOr(Lazy<Maybe<A>> or)
        {
            return isJust ? this : or.Force;
        }

        public Maybe<A> JustOr(Func<Maybe<A>> or)
        {
            return isJust ? this : or();
        }
    }

    public static class Maybe
    {
        public static Maybe<A> Nothing<A>()
        {
            return default(Maybe<A>);
        }

        public static Maybe<A> Just<A>(A value)
        {
            return new Maybe<A>(value);
        }

        public static Maybe<A> When<A>(Lazy<A> a, bool b)
        {
            return b ? Maybe.Just(a.Force) : Maybe.Nothing<A>();
        }

        public static Maybe<A> When<A>(Func<A> a, bool b)
        {
            return b ? Maybe.Just(a()) : Maybe.Nothing<A>();
        }

        public static Maybe<A> When<A>(A a, bool b)
        {
            return b ? Maybe.Just(a) : Maybe.Nothing<A>();
        }
    }
}