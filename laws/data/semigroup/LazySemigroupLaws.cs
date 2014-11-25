using Fuchu;
using FC = Fuchu.FsCheck;

namespace sharper.c.data.semigroup
{
    using control.trampoline;
    using data.equal;
    using data.laws;

    public static class LazySemigroupLaws
    {
        public static Laws Get<S, E, A>()
            where S : struct, ILazySemigroup<A>
            where E : struct, IEqual<A>
        {
            var E_ = default(E);
            var S_ = default(S);
            return Laws.Mk(
                    "lazy semigroup",
                    FC.Property(
                            "associativity",
                            (Bounce<A> a1, Bounce<A> a2, Bounce<A> a3) =>
                            E_.Eq(
                                    S_.LazyPlus(S_.LazyPlus(a1, a2), a3)
                                        .Run(),
                                    S_.LazyPlus(a1, S_.LazyPlus(a2, a3))
                                        .Run())))
                + SemigroupLaws.Get<S, E, A>();
        }
    }
}
