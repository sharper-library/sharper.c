namespace sharper.c.data.tuple
{
    using data.semigroup;

    public struct PairSemigroup<A, B, SemiA, SemiB>
        : ISemigroup<Pair<A, B>>
        where SemiA : struct, ISemigroup<A>
        where SemiB : struct, ISemigroup<B>
    {
        public Pair<A, B> Plus(Pair<A, B> x, Pair<A, B> y)
        {
            return Pair.Mk(
                default(SemiA).Plus(x.Fst, y.Fst),
                default(SemiB).Plus(x.Snd, y.Snd));
        }
    }
}