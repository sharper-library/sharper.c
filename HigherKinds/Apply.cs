using System;
using Sharper.C.HigherKinds.Wrapper;

namespace Sharper.C.HigherKinds
{
    public interface IApply<T>
        : IFunctor<T>
        where T : struct, IApply<T>
    {
        HK<T, B> Ap<A, B>(HK<T, Func<A, B>> f, HK<T, A> a);
    }

    public static class Apply<T>
        where T : struct, IApply<T>
    {
        public static Func<HK<T, A>, HK<T, B>> Ap<A, B>(HK<T, Func<A, B>> f)
        {
            return a => default(T).Ap(f, a);
        }
    }

    public static class ApplyMethods
    {
        public static HK<T, B> Ap<T, A, B>(this HK<T, Func<A, B>> f, HK<T, A> a)
            where T : struct, IApply<T>
        {
            return default(T).Ap(f, a);
        }
    }

    public static class ApplyLinq
    {
        public static HK<T, C> Join<T, K, A, B, C>(
            this HK<T, A> fa,
            HK<T, B> fb,
            Func<A, K> _,
            Func<B, K> __,
            Func<A, B, C> f)
            where T : struct, IApply<T>
        {
            var af = default(T);
            var f1 = af.Map<A, Func<B, C>>(a => b => f(a, b), fa);
            return af.Ap(f1, fb);
        }

        private static HK<T, C> LinqJoinCompileCheck<T, A, B, C>(
            HK<T, A> fa,
            HK<T, B> fb,
            Func<A, B, C> f)
            where T : struct, IApply<T>
        {
            return
                from a in fa
                join b in fb on 1 equals 1
                select f(a, b);
        }
    }
}