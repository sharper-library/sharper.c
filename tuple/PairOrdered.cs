namespace sharper.c.data.tuple
{
    using data.equal;
    using data.ordered;
    using data.semigroup;

    public struct PairOrdered<A, B, OrdA, OrdB>
        : IOrdered<Pair<A, B>>
        where OrdA : struct, IOrdered<A>
        where OrdB : struct, IOrdered<B>
    {
        public bool Eq(Pair<A, B> x, Pair<A, B> y)
        {
            return default(OrdA).Eq(x.Fst, y.Fst)
                && default(OrdB).Eq(x.Snd, y.Snd);
        }

        public Ordering Compare(Pair<A, B> x, Pair<A, B> y)
        {
            return default(OrderingMonoid).Plus(
                default(OrdA).Compare(x.Fst, y.Fst),
                default(OrdB).Compare(x.Snd, y.Snd));
        }
    }
}
