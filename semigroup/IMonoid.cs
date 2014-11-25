namespace sharper.c.data.semigroup
{
    public interface IMonoid<A>
        : ISemigroup<A>
    {
        A Zero { get; }
    }
}