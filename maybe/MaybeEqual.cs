namespace sharper.c.data.maybe
{
    using data.equal;

    public struct MaybeEqual<A, EqA>
        : IEqual<Maybe<A>>
        where EqA : struct, IEqual<A>
    {
        public bool Eq(Maybe<A> m1, Maybe<A> m2)
        {
            var E_ = default(EqA);
            return m1.Match(
                    () => m2.IsNothing,
                    a1 => m2.Match(
                            () => false,
                            a2 => E_.Eq(a1, a2)));
        }
    }
}