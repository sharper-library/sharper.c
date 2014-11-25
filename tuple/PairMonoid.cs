namespace sharper.c.data.tuple
{
    using data.semigroup;

    public struct PairMonoid<A, B, MonoidA, MonoidB>
        : IMonoid<Pair<A, B>>
        where MonoidA : struct, IMonoid<A>
        where MonoidB : struct, IMonoid<B>
    {
        public Pair<A, B> Plus(Pair<A, B> x, Pair<A, B> y)
        {
            return Pair.Mk(
                default(MonoidA).Plus(x.Fst, y.Fst),
                default(MonoidB).Plus(x.Snd, y.Snd));
        }

        public Pair<A, B> Zero
        {
            get
            {
                return Pair.Mk(default(MonoidA).Zero, default(MonoidB).Zero);
            }
        }
    }
}
