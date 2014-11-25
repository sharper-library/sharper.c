namespace sharper.c.data.either
{
    using data.equal;

    public struct EitherEqual<A, B, EqA, EqB>
        : IEqual<Either<A, B>>
        where EqA : struct, IEqual<A>
        where EqB : struct, IEqual<B>
    {
        public bool Eq(Either<A, B> x, Either<A, B> y)
        {
            var EA_ = default(EqA);
            var EB_ = default(EqB);
            return x.Match(
                    a1 => y.Match(
                            a2 => EA_.Eq(a1, a2),
                            _ => false),
                    b1 => y.Match(
                            _ => false,
                            b2 => EB_.Eq(b1, b2)));
        }
    }
}