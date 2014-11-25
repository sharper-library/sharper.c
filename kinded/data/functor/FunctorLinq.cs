using System;

namespace sharper.c.data.functor
{
    using data.kinded;

    public static class FunctorLinq
    {
        public static HK<F, B> Select<F, A, B>(this HK<F, A> a, Func<A, B> f)
            where F : struct, IFunctor<F>
        {
            return default(F).Map(f, a);
        }
    }
}