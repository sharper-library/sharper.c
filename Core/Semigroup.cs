namespace Sharper.C.Semigroups
{
    public interface ISemigroup<S, A>
        where S : struct, ISemigroup<S, A>
    {
        A Op(A x, A y);
    }
}