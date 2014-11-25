namespace sharper.c.data.semigroup
{
    public interface ISemigroup<A>
    {
        A Plus(A x, A y);
    }
}