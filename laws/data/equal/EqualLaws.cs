using Fuchu;
using FC = Fuchu.FsCheck;

namespace sharper.c.data.equal
{
    using data.function;
    using data.laws;

    public static class EqualLaws
    {
        public static Laws Get<E, A>()
            where E : struct, IEqual<A>
        {
            var E_ = default(E);
            return Laws.Mk(
                    "Equal (equivalence relation)",
                    FC.Property(
                            "reflexivity",
                            (A a) => E_.Eq(a, a)),
                    FC.Property(
                            "symmetry",
                            (A a1, A a2) => E_.Eq(a1, a2) == E_.Eq(a2, a1)),
                    FC.Property(
                            "transitivity",
                            (A a1, A a2, A a3) =>
                            Pred.Implies(
                                    E_.Eq(a1, a2) && E_.Eq(a2, a3),
                                    E_.Eq(a1, a3))));
        }
    }
}