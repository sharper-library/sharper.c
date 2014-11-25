using Fuchu;
using FC = Fuchu.FsCheck;

namespace sharper.c.data.semigroup
{
    using control.trampoline;
    using data.equal;
    using data.laws;

    public static class LazyMonoidLaws
    {
        public static Laws Get<M, E, A>()
            where M : struct, ILazyMonoid<A>
            where E : struct, IEqual<A>
        {
            var E_ = default(E);
            var M_ = default(M);
            var zero = Bounce.Done(M_.Zero);
            return Laws.Mk(
                    "Monoid",
                    FC.Property(
                            "left identity",
                            (Bounce<A> a) => E_.Eq(
                                    a.Run(),
                                    M_.LazyPlus(zero, a).Run())),
                    FC.Property(
                            "right identity",
                            (Bounce<A> a) => E_.Eq(
                                    a.Run(),
                                    M_.LazyPlus(a, zero).Run())))
                + LazySemigroupLaws.Get<M, E, A>();
        }
    }
}
