using Fuchu;
using FC = Fuchu.FsCheck;

namespace sharper.c.data.semigroup
{
    using data.equal;
    using data.laws;

    public static class SemigroupLaws
    {
        public static Laws Get<S, E, A>()
            where S : struct, ISemigroup<A>
            where E : struct, IEqual<A>
        {
            var E_ = default(E);
            var S_ = default(S);
            return Laws.Mk(
                    "semigroup",
                    FC.Property(
                            "associativity",
                            (A a1, A a2, A a3) => E_.Eq(
                                    S_.Plus(S_.Plus(a1, a2), a3),
                                    S_.Plus(a1, S_.Plus(a2, a3)))));
        }
    }
}