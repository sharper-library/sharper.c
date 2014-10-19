using System;
using Sharper.C.HigherKinds.Wrapper;

namespace Sharper.C.HigherKinds
{
    public interface ISemimonad<T>
        : IApply<T>
        where T : struct, ISemimonad<T>
    {
        HK<T, B> Bind<A, B>(Func<A, HK<T, B>> f, HK<T, A> a);
    }

    public static class Semimonad<T>
        where T : struct, ISemimonad<T>
    {
        public static Func<HK<T, A>, HK<T, B>> Bind<A, B>(Func<A, HK<T, B>> f)
        {
            return a => default(T).Bind(f, a);
        }
    }

    public static class SemimonadMethods
    {
        public static HK<T, B>
        Bind<T, A, B>(this HK<T, A> a, Func<A, HK<T, B>> f)
            where T : struct, ISemimonad<T>
        {
            return default(T).Bind(f, a);
        }
    }

    public static class SemimonadLinq
    {
        public static HK<T, C>
        SelectMany<T, A, B, C>(
                this HK<T, A> ta,
                Func<A, HK<T, B>> f,
                Func<A, B, C> g)
            where T : struct, ISemimonad<T>
        {
            return default(T).Bind(a => default(T).Map(b => g(a, b), f(a)), ta);
        }

        private static HK<T, B>
        LinqSelectManyCompileCheck<T, A, B>(
                this HK<T, A> ta,
                Func<A, HK<T, B>> f)
            where T : struct, ISemimonad<T>
        {
            return
                from a in ta
                from b in f(a)
                select b;
        }
    }
}