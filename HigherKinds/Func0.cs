using System;
using Sharper.C.HigherKinds.Unsafe;

namespace Sharper.C.HigherKinds
{
    public struct Func0F
        : IFunctor<Func0F>
    {
        public static HK<Func0F, A> Wrap<A>(Func<A> f)
        {
            return HKUnsafe<Func0F>.Wrap<A>(f);
        }

        public static Func<A> Unwrap<A>(HK<Func0F, A> a)
        {
            return (Func<A>) HKUnsafe<Func0F>.Unwrap<A>(a);
        }

        public HK<Func0F, B> Map<A, B>(Func<A, B> f, HK<Func0F, A> a)
        {
            return Wrap<B>(() => f(Unwrap<A>(a)()));
        }
    }
}