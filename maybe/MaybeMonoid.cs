namespace sharper.c.data.maybe
{
    using data.lazy;
    using data.semigroup;

    public struct MaybeMonoid<A, SgA>
        : IMonoid<Maybe<A>>
        where SgA : struct, ISemigroup<A>
    {
        public Maybe<A> Plus(Maybe<A> m1, Maybe<A> m2)
        {
            var s = default(SgA);
            return m1.Match(
                    () => m2,
                    a1 => m2.Match(
                            () => m1,
                            a2 => Maybe.Just(s.Plus(a1, a2))));
        }

        public Maybe<A> LazyPlus(Maybe<A> m1, Lazy<Maybe<A>> m2)
        {
            return Plus(m1, m2.Force);
        }

        public Maybe<A> Zero
        {
            get { return Maybe.Nothing<A>(); }
        }
    }
}