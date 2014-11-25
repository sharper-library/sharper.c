using Fuchu;
using FC = Fuchu.FsCheck;

namespace sharper.c.data.semigroup
{
    using data.equal;
    using data.laws;

    public static class MonoidLaws
    {
        public static Laws Get<M, E, A>()
            where M : struct, IMonoid<A>
            where E : struct, IEqual<A>
        {
            var E_ = default(E);
            var M_ = default(M);
            return Laws.Mk(
                    "Monoid",
                    FC.Property(
                            "left identity",
                            (A a) => E_.Eq(a, M_.Plus(M_.Zero, a))),
                    FC.Property(
                            "right identity",
                            (A a) => E_.Eq(a, M_.Plus(a, M_.Zero))))
                + SemigroupLaws.Get<M, E, A>();
        }
    }
}