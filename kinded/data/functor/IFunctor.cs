using System;

namespace sharper.c.data.functor
{
    using data.kinded;

    public interface IFunctor<F>
        where F : struct
    {
        HK<F, B> Map<A, B>(Func<A, B> f, HK<F, A> a);
    }
}