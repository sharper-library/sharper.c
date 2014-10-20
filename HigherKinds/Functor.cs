using System;

namespace Sharper.C.HigherKinds
{
    public interface IFunctor<T>
        where T : struct, IFunctor<T>
    {
        HK<T, B> Map<A, B>(Func<A, B> f, HK<T, A> a);
    }

    public static class Functor<T>
        where T : struct, IFunctor<T>
    {
        public static Func<HK<T, A>, HK<T, B>> Map<A, B>(Func<A, B> f)
        {
            return a => default(T).Map(f, a);
        }
    }

    public static class FunctorLinq
    {
        public static HK<T, B> Select<T, A, B>(this HK<T, A> a, Func<A, B> f)
            where T : struct, IFunctor<T>
        {
            return default(T).Map(f, a);
        }

        private static HK<T, B> LinqSelectCompileCheck<T, A, B>(
            HK<T, A> a,
            Func<A, B> f)
            where T : struct, IFunctor<T>
        {
            return from a1 in a select f(a1);
        }
    }
}
