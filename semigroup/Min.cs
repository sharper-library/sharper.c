namespace sharper.c.data.semigroup
{
    using data.bounded;
    using data.ordered;

    public struct Min<A>
    {
        public readonly A UnMin;

        internal Min(A a)
        {
            UnMin = a;
        }
    }

    public static class Min
    {
        public static Min<A> Wrap<A>(A a)
        {
            return new Min<A>(a);
        }

        public static A Unwrap<A>(Min<A> a)
        {
            return a.UnMin;
        }
    }

    public struct MinBounded<A, BoundA>
        : IBounded<Min<A>>
        where BoundA : struct, IBounded<A>
    {
        public Min<A> MinBound
        {
            get { return Min.Wrap(default(BoundA).MinBound); }
        }

        public Min<A> MaxBound
        {
            get { return Min.Wrap(default(BoundA).MaxBound); }
        }
    }

    public struct MinSemigroup<A, OrdA>
        : ISemigroup<Min<A>>
        where OrdA : struct, IOrdered<A>
    {
        public Min<A> Plus(Min<A> x, Min<A> y)
        {
            var O_ = default(Ordered<OrdA, A>);
            return Min.Wrap(O_.Min(x.UnMin, y.UnMin));
        }
    }

    public struct MinMonoid<A, InstA>
        : IMonoid<Min<A>>
        where InstA : struct, IOrdered<A>, IBounded<A>
    {
        public Min<A> Plus(Min<A> x, Min<A> y)
        {
            return default(MinSemigroup<A, InstA>).Plus(x, y);
        }

        public Min<A> Zero
        {
            get { return default(MinBounded<A, InstA>).MaxBound; }
        }
    }
}
