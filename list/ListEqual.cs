namespace sharper.c.data.list
{
    using data.equal;
    using data.semigroup;

    public struct ListEqual<A, EqA>
        : IEqual<List<A>>
        where EqA : struct, IEqual<A>
    {
        public bool Eq(List<A> x, List<A> y)
        {
            var E_ = default(EqA);
            return x.AlignWith(
                    y,
                    t => t.Match(
                            _ => false,
                            __ => false,
                            E_.Eq))
                .BouncedFold<BoolAllMonoid>()
                .Run();
        }
    }
}