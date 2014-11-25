namespace sharper.c.data.maybe
{
    using data.ordered;

    public struct MaybeOrdered<A, OrdA>
        : IOrdered<Maybe<A>>
        where OrdA : struct, IOrdered<A>
    {
        public bool Eq(Maybe<A> x, Maybe<A> y)
        {
            return default(MaybeEqual<A, OrdA>).Eq(x, y);
        }

        public Ordering Compare(Maybe<A> x, Maybe<A> y)
        {
            var O_ = default(OrdA);
            if (x.IsNothing && y.IsNothing) return Ordering.EQ;
            if (x.IsNothing) return Ordering.LT;
            if (y.IsNothing) return Ordering.GT;
            return O_.Compare(x.value, y.value);
        }
    }
}
