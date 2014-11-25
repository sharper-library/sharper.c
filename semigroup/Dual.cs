namespace sharper.c.data.semigroup
{
    using control.trampoline;

    public struct Dual<A>
    {
        public readonly A UnDual;

        internal Dual(A a)
        {
            UnDual = a;
        }
    }

    public static class Dual
    {
        public static Dual<A> Wrap<A>(A a)
        {
            return new Dual<A>(a);
        }

        public static A Unwrap<A>(Dual<A> a)
        {
            return a.UnDual;
        }
    }

    public struct DualSemigroup<A, SemiA>
        : ISemigroup<Dual<A>>
        where SemiA : struct, ISemigroup<A>
    {
        public Dual<A> Plus(Dual<A> x, Dual<A> y)
        {
            return Dual.Wrap(default(SemiA).Plus(y.UnDual, x.UnDual));
        }
    }

    public struct DualLazySemigroup<A, SemiA>
        : ILazySemigroup<Dual<A>>
        where SemiA : struct, ILazySemigroup<A>
    {
        public Dual<A> Plus(Dual<A> x, Dual<A> y)
        {
            return Dual.Wrap(default(SemiA).Plus(y.UnDual, x.UnDual));
        }

        public Bounce<Dual<A>> LazyPlus(Bounce<Dual<A>> x, Bounce<Dual<A>> y)
        {
            return default(SemiA).LazyPlus(
                    y.Map(Dual.Unwrap),
                    x.Map(Dual.Unwrap))
                .Map(Dual.Wrap);
        }
    }

    public struct DualMonoid<A, MonoidA>
        : IMonoid<Dual<A>>
        where MonoidA : struct, IMonoid<A>
    {
        public Dual<A> Plus(Dual<A> x, Dual<A> y)
        {
            return Dual.Wrap(default(MonoidA).Plus(y.UnDual, x.UnDual));
        }

        public Dual<A> Zero
        {
            get { return Dual.Wrap(default(MonoidA).Zero); }
        }
    }

    public struct DualLazyMonoid<A, MonoidA>
        : IMonoid<Dual<A>>
        where MonoidA : struct, ILazyMonoid<A>
    {
        public Dual<A> Plus(Dual<A> x, Dual<A> y)
        {
            return Dual.Wrap(default(MonoidA).Plus(y.UnDual, x.UnDual));
        }

        public Bounce<Dual<A>> LazyPlus(Bounce<Dual<A>> x, Bounce<Dual<A>> y)
        {
            return default(MonoidA).LazyPlus(
                    y.Map(Dual.Unwrap),
                    x.Map(Dual.Unwrap))
                .Map(Dual.Wrap);
        }

        public Dual<A> Zero
        {
            get { return Dual.Wrap(default(MonoidA).Zero); }
        }
    }
}