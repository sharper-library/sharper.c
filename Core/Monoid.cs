using Sharper.C.Semigroups;

namespace Sharper.C
{
    public interface IMonoid<M, A> : ISemigroup<M, A>
        where M : struct, IMonoid<M, A>
    {
        A Id { get; }
    }
}