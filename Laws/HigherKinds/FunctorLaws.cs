using System;
using Sharper.C;
using Sharper.C.HigherKinds;
using Sharper.C.HigherKinds.Wrapper;

namespace Sharper.C.Laws.HigherKinds
{
    public abstract class FunctorLaws<F, A, B, C, Eq>
        where F : struct, IFunctor<F>
        where Eq : struct, IEqual<Eq, HK<F, A>>
    {
        public bool Identity(HK<F, A> a)
        {
            return Equal<Eq, HK<F, A>>.Eq
                (Functor<F>.Map<A, A>(Fn.Id)(a))
                (Fn.Id(a));
        }

        public bool
        Distributivity(HK<F, B> a, Func<B, C> f, Func<C, A> g)
        {
            return Equal<Eq, HK<F, A>>.Eq
                (Functor<F>.Map(Fn.Comp(g, f))(a))
                (Functor<F>.Map(g)(Functor<F>.Map(f)(a)));
        }
    }
}