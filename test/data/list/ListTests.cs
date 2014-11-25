namespace sharper.c.data.list
{
    using data.equal;
    using data.ordered;
    using data.laws;
    using data.semigroup;

    public static class ListTests
    {
        public static Laws Get()
        {
            return Laws.Mk("List")
                + EqualLaws.Get<ListEqual<int, ValueEqual<int>>, List<int>>()
                + OrderedLaws.Get<ListOrdered<int, IntOrdered>, List<int>>()
                + MonoidLaws.Get<
                        ListMonoid<int>,
                        ListEqual<int, IntOrdered>,
                        List<int>>();
        }
    }
}