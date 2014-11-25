namespace sharper.c.data.semigroup
{
    using control.trampoline;
    using data.ordered;

    public struct OrderingMonoid
        : ILazyMonoid<Ordering>
    {
        public Ordering Plus(Ordering x, Ordering y)
        {
            return x == Ordering.LT
                ? Ordering.LT
                : x == Ordering.GT
                    ? Ordering.GT
                    : y;
        }

        public Bounce<Ordering> LazyPlus(Bounce<Ordering> x, Bounce<Ordering> y)
        {
            return x.Bind(x1 => x1 != Ordering.EQ ? Bounce.Done(x1) : y);
        }

        public Ordering Zero
        {
            get { return Ordering.EQ; }
        }
    }
}
