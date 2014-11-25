namespace sharper.c.data.ordered
{
    public struct OrderingOrdered
        : IOrdered<Ordering>
    {
        public bool Eq(Ordering x, Ordering y)
        {
            return x == y;
        }

        public Ordering Compare(Ordering x, Ordering y)
        {
            if (x == y) return Ordering.EQ;
            if (x == Ordering.LT) return Ordering.LT;
            if (y == Ordering.LT) return Ordering.GT;
            if (x == Ordering.EQ) return Ordering.LT;
            return Ordering.GT;
        }
    }
}