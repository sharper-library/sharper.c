namespace sharper.c.data.ordered
{
    using data.laws;
    using data.semigroup;

    public static class OrderingTests
    {
        public static Laws Get()
        {
            return Laws.Mk("Ordering")
                + OrderedLaws.Get<OrderingOrdered, Ordering>()
                + MonoidLaws.Get<
                        OrderingMonoid,
                        OrderingOrdered,
                        Ordering>();
        }
    }
}