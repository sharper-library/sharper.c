using System;

namespace Sharper.C.HigherKinds
{
    public interface IProfunctor<T>
        where T : struct, IProfunctor<T>
    {
        HK<T, A, D>
        Dimap<A, B, C, D>(Func<A, B> f, Func<C, D> g, HK<T, B, C> bc);

        HK<T, A, C> Lmap<A, B, C>(Func<A, B> f, HK<T, B, C> bc);

        HK<T, B, D> Rmap<B, C, D>(Func<C, D> g, HK<T, B, C> bc);
    }

    public static class Profunctor<T>
        where T : struct, IProfunctor<T>
    {
        public static Func<HK<T, B, C>, HK<T, A, D>>
        Dimap<A, B, C, D>(Func<A, B> f, Func<C, D> g)
        {
            return bc => default(T).Dimap(f, g, bc);
        }

        public static Func<HK<T, B, C>, HK<T, A, C>> Lmap<A, B, C>(Func<A, B> f)
        {
            return bc => default(T).Lmap(f, bc);
        }

        public static Func<HK<T, B, C>, HK<T, B, D>> Rmap<B, C, D>(Func<C, D> g)
        {
            return bc => default(T).Rmap(g, bc);
        }
    }

    public static class ProfunctorMethods
    {
        public static HK<T, A, D>
        Dimap<T, A, B, C, D>(this HK<T, B, C> bc, Func<A, B> f, Func<C, D> g)
            where T : struct, IProfunctor<T>
        {
            return default(T).Dimap(f, g, bc);
        }

        public static HK<T, A, C>
        Lmap<T, A, B, C>(this HK<T, B, C> bc, Func<A, B> f)
            where T : struct, IProfunctor<T>
        {
            return default(T).Lmap(f, bc);
        }

        public static HK<T, B, D>
        Lmap<T, B, C, D>(this HK<T, B, C> bc, Func<C, D> g)
            where T : struct, IProfunctor<T>
        {
            return default(T).Rmap(g, bc);
        }
    }
}