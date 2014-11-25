namespace sharper.c.data.maybe
{
    using data.equal;
    using data.laws;
    using data.ordered;
    using data.semigroup;

    public static class MaybeTests
    {
        public static Laws Get()
        {
            return Laws.Mk("Maybe")
                + EqualLaws.Get<MaybeEqual<int, ValueEqual<int>>, Maybe<int>>()
                + OrderedLaws.Get<MaybeOrdered<int, IntOrdered>, Maybe<int>>()
                + MonoidLaws.Get<
                        MaybeMonoid<int, IntSumMonoid>,
                        MaybeEqual<int, ValueEqual<int>>,
                        Maybe<int>>();
        }
    }
}