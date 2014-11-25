namespace sharper.c.data.tuple
{
    using data.equal;
    using data.laws;
    using data.ordered;
    using data.semigroup;
    using legacy.@null;

    public static class PairTests
    {
        public static Laws Get()
        {
            return Laws.Mk("Pair")
                + EqualLaws.Get<
                        PairEqual<int, string, ValueEqual<int>, StringEqual>,
                        Pair<int, string>>()
                + OrderedLaws.Get<
                        PairOrdered<int, string, IntOrdered, StringOrdered>,
                        Pair<int, string>>()
                + SemigroupLaws.Get<
                        PairSemigroup<int, string, IntSumMonoid, StringMonoid>,
                        PairEqual<int, string, ValueEqual<int>, StringEqual>,
                        Pair<int, string>>()
                + MonoidLaws.Get<
                        PairMonoid<
                                int,
                                NotNull<string, StringMonoid>,
                                IntSumMonoid,
                                NotNullMonoid<string, StringMonoid>>,
                        PairEqual<
                                int,
                                NotNull<string, StringMonoid>,
                                ValueEqual<int>,
                                NotNullEqual<
                                        string,
                                        StringEqual,
                                        StringMonoid>>,
                        Pair<int, NotNull<string, StringMonoid>>>();
        }
    }
}