using System;
using Fuchu;
using FC = Fuchu.FsCheck;

namespace sharper.c.data.functor
{
    using data.equal;
    using data.function;
    using data.kinded;
    using data.laws;

    public static class FunctorLaws
    {
        public static Laws Get<F, A, B, C, EqC>()
            where F : struct, IFunctor<F>
            where EqC : struct, IEqual<HK<F, C>>
        {
            var F_ = default(F);
            var E_ = default(EqC);
            return Laws.Mk(
                    "Functor",
                    FC.Property(
                            "identity",
                            (HK<F, C> c) =>
                            E_.Eq(F_.Map<C, C>(Fn.Id, c), Fn.Id(c))),
                    FC.Property(
                            "distributivity",
                            (HK<F, A> a, Func<A, B> f, Func<B, C> g) => E_.Eq(
                                    F_.Map(Fn.Comp(g, f), a),
                                    F_.Map(g, F_.Map(f, a)))));
        }
    }
}