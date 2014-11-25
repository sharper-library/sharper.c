namespace sharper.c.data.ordered
{
    using data.equal;

    public interface IOrdered<A>
        : IEqual<A>
    {
        Ordering Compare(A x, A y);
    }
}