using System;

namespace sharper.c.data.these
{
    using data.either;
    using data.function;
    using data.maybe;
    using data.semigroup;
    using data.tuple;

    public struct These<A, B>
    {
        private enum State {_This, _That, _Both }

        private readonly State state;
        private readonly A a;
        private readonly B b;

        private These(A a, B b, State state)
        {
            this.a = a;
            this.b = b;
            this.state = state;
        }

        internal static These<A, B> Both(A a, B b)
        {
            return new These<A, B>(a, b, State._Both);
        }

        internal static These<A, B> This(A a)
        {
            return new These<A, B>(a, default(B), State._This);
        }

        internal static These<A, B> That(B b)
        {
            return new These<A, B>(default(A), b, State._That);
        }

        public bool IsThis
        {
            get { return state == State._This; }
        }

        public bool IsThat
        {
            get { return state == State._That; }
        }

        public bool IsBoth
        {
            get { return state == State._Both; }
        }

        public C Match<C>(Func<A, C> _this, Func<B, C> that, Func<A, B, C> both)
        {
            switch (state)
            {
            case State._This: return _this(a);
            case State._That: return that(b);
            default: return both(a, b);
            }
        }

        public Maybe<A> OnlyThis
        {
            get { return Maybe.When(a, state == State._This); }
        }

        public Maybe<B> OnlyThat
        {
            get { return Maybe.When(b, state == State._That); }
        }

        public Maybe<Either<A, B>> OnlyThisOrThat
        {
            get
            {
                switch (state)
                {
                case State._This: return Maybe.Just(Either.Left<A, B>(a));
                case State._That: return Maybe.Just(Either.Right<A, B>(b));
                default: return Maybe.Nothing<Either<A, B>>();
                }
            }
        }

        public Maybe<Pair<A, B>> OnlyBoth
        {
            get { return Maybe.When(Pair.Mk(a, b), state == State._Both); }
        }

        public These<C, D> Bimap<C, D>(Func<A, C> f, Func<B, D> g)
        {
            switch (state)
            {
            case State._This: return These<C, D>.This(f(a));
            case State._That: return These<C, D>.That(g(b));
            default: return These<C, D>.Both(f(a), g(b));
            }
        }
    }

    public static class These
    {
        public static These<A, B> This<A, B>(A a)
        {
            return These<A, B>.This(a);
        }

        public static These<A, B> That<A, B>(B b)
        {
            return These<A, B>.That(b);
        }

        public static These<A, B> Both<A, B>(A a, B b)
        {
            return These<A, B>.Both(a, b);
        }

        public static Maybe<These<A, B>>
        FromMaybes<A, B>(Maybe<A> a, Maybe<B> b)
        {
            return a.ZipWith(b, Both)
                .JustOr(() => a.Map(This<A, B>))
                .JustOr(() => b.Map(That<A, B>));
        }

        public static A Magma<A>(These<A, A> aa, Func<A, A, A> f)
        {
            return aa.Match(Fn.Id, Fn.Id, f);
        }

        //public static C Plus<A, B, C, SemiC>(
        //        These<A, B> ab,
        //        Func<A, C> ac,
        //        Func<B, C> bc)
        //    where SemiC : struct, ISemigroup<SemiC, C>
        //{
        //    var S_ = default(SemiC);
        //    return ab.Match(ac, bc, (a, b) => S_.Plus(ac(a), bc(b)));
        //}
    }
}