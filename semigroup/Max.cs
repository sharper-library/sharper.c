namespace sharper.c.data.semigroup
{
    using data.bounded;
    using data.ordered;

    public struct Max<A>
    {
        public readonly A UnMax;

        internal Max(A a)
        {
            UnMax = a;
        }
    }

    public static class Max
    {
        public static Max<A> Wrap<A>(A a)
        {
            return new Max<A>(a);
        }

        public static A Unwrap<A>(Max<A> a)
        {
            return a.UnMax;
        }
    }

    public struct MaxBounded<A, BoundA>
        : IBounded<Max<A>>
        where BoundA : struct, IBounded<A>
    {
        public Max<A> MinBound
        {
            get { return Max.Wrap(default(BoundA).MinBound); }
        }

        public Max<A> MaxBound
        {
            get { return Max.Wrap(default(BoundA).MaxBound); }
        }
    }

    public struct MaxSemigroup<A, OrdA>
        : ISemigroup<Max<A>>
        where OrdA : struct, IOrdered<A>
    {
        public Max<A> Plus(Max<A> x, Max<A> y)
        {
            var O_ = default(Ordered<OrdA, A>);
            return Max.Wrap(O_.Max(x.UnMax, y.UnMax));
        }
    }

    public struct MaxMonoid<A, InstA>
        : IMonoid<Max<A>>
        where InstA : struct, IOrdered<A>, IBounded<A>
    {
        public Max<A> Plus(Max<A> x, Max<A> y)
        {
            return default(MaxSemigroup<A, InstA>).Plus(x, y);
        }

        public Max<A> Zero
        {
            get { return default(MaxBounded<A, InstA>).MinBound; }
        }
    }
}