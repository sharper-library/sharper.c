using Fuchu;
using FC = Fuchu.FsCheck;

namespace sharper.c.data.ordered
{
    using data.equal;
    using data.function;
    using data.laws;

    public static class OrderedLaws
    {
        public static Laws Get<O, A>()
            where O : struct, IOrdered<A>
        {
            var O_ = default(Ordered<O, A>);
            return Laws.Mk(
                    "Ordered (a total order)",
                    FC.Property(
                            "transitivity",
                            (A a1, A a2, A a3) => Pred.Implies(
                                    O_.LtEq(a1, a2) && O_.LtEq(a2, a3),
                                    O_.LtEq(a1, a3))),
                    FC.Property(
                            "antisymmetry",
                            (A a1, A a2) => Pred.Implies(
                                    O_.LtEq(a1, a2) && O_.LtEq(a2, a1),
                                    O_.Eq(a1, a2))),
                    FC.Property(
                            "reflexivity",
                            (A a) => O_.LtEq(a, a)))
                + EqualLaws.Get<O, A>();
        }
    }
}