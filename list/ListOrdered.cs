namespace sharper.c.data.list
{
    using data.ordered;
    using data.semigroup;

    public struct ListOrdered<A, OrdA>
        : IOrdered<List<A>>
        where OrdA : struct, IOrdered<A>
    {
        public bool Eq(List<A> x, List<A> y)
        {
            return default(ListEqual<A, OrdA>).Eq(x, y);
        }

        public Ordering Compare(List<A> x, List<A> y)
        {
            var O_ = default(OrdA);
            return x.AlignWith(
                    y,
                    t => t.Match(
                            _ => Ordering.GT,
                            _ => Ordering.LT,
                            O_.Compare))
                .BouncedFold<OrderingMonoid>()
                .Run();
        }
    }
}