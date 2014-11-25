namespace sharper.c.data.semigroup
{
    public interface ILazyMonoid<A>
        : ILazySemigroup<A>
        , IMonoid<A>
    {
    }
}
