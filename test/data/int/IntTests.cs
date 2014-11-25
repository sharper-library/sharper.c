namespace sharper.c.data.@int
{
    using data.laws;
    using data.ordered;
    using data.semigroup;

    public struct IntTests
    {
        public static Laws Get()
        {
            return Laws.Mk("int")
                + OrderedLaws.Get<IntOrdered, int>()
                + MonoidLaws.Get<IntSumMonoid, IntOrdered, int>()
                + MonoidLaws.Get<IntMultMonoid, IntOrdered, int>();
        }
    }
}