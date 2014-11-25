using System;

namespace sharper.c.data.functor
{
    using data.kinded;

    public struct Functor<F>
        where F : struct, IFunctor<F>
    {
        public Func<HK<F, A>, HK<F, B>> Map<A, B>(Func<A, B> f)
        {
            return a => default(F).Map(f, a);
        }

        public HK<F, B> Map<A, B>(Func<A, B> f, HK<F, A> a)
        {
            return default(F).Map(f, a);
        }
    }
}