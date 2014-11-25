namespace sharper.c.data.tuple
{
    using data.equal;

    public struct PairEqual<A, B, EqA, EqB>
        : IEqual<Pair<A, B>>
        where EqA : struct, IEqual<A>
        where EqB : struct, IEqual<B>
    {
        public bool Eq(Pair<A, B> x, Pair<A, B> y)
        {
            return default(EqA).Eq(x.Fst, y.Fst)
                && default(EqB).Eq(x.Snd, y.Snd);
        }
    }
}