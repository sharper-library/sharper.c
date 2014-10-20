using System;
using Sharper.C;
using Sharper.C.HigherKinds.Unsafe;

namespace Sharper.C.HigherKinds
{
    public struct Const<A, B>
    {
        public readonly A Value;

        internal Const(A a)
        {
            Value = a;
        }

        public Const<A, C> Cast<C>()
        {
            return new Const<A, C>(Value);
        }
    }

    public static class Const
    {
        public static Const<A, B> Mk<A, B>(A a)
        {
            return new Const<A, B>(a);
        }

        public static Const<A, B> Unit<M, A, B>(B _)
            where M : struct, IMonoid<M, A>
        {
            return new Const<A, B>(default(M).Id);
        }

        public static A Value<A, B>(Const<A, B> c)
        {
            return c.Value;
        }
    }

    public struct ConstF<R>
        : IApply<ConstF<R>>
    {
        public static HK<ConstF<R>, A> Wrap<A>(Const<R, A> ta)
        {
            return HKUnsafe<ConstF<R>>.Wrap<A>(ta);
        }

        public static Const<R, A> Unwrap<A>(HK<ConstF<R>, A> hk)
        {
            return (Const<R, A>) HKUnsafe<ConstF<R>>.Unwrap<A>(hk);
        }

        public static HK<ConstF<R>, A> Mk<A>(R r)
        {
            return Wrap<A>(Const.Mk<R, A>(r));
        }

        public static R Value<A>(HK<ConstF<R>, A> hk)
        {
            return Unwrap<A>(hk).Value;
        }

        public HK<ConstF<R>, B> Map<A, B>(Func<A, B> _, HK<ConstF<R>, A> ta)
        {
            return Wrap<B>(Unwrap<A>(ta).Cast<B>());
        }

        public HK<ConstF<R>, B>
        Ap<A, B>(HK<ConstF<R>, Func<A, B>> _, HK<ConstF<R>, A> a)
        {
            return Wrap<B>(Unwrap<A>(a).Cast<B>());
        }
    }

    public struct ConstA<M, R>
        : IApplicative<ConstA<M, R>>
        where M : struct, IMonoid<M, R>
    {
        public static HK<ConstA<M, R>, A> Wrap<A>(Const<R, A> ta)
        {
            return HKUnsafe<ConstA<M, R>>.Wrap<A>(ta);
        }

        public static Const<R, A> Unwrap<A>(HK<ConstA<M, R>, A> hk)
        {
            return (Const<R, A>) HKUnsafe<ConstA<M, R>>.Unwrap<A>(hk);
        }

        public static HK<ConstA<M, R>, A> Mk<A>(R r)
        {
            return Wrap<A>(Const.Mk<R, A>(r));
        }

        public static R Value<A>(HK<ConstA<M, R>, A> hk)
        {
            return Unwrap<A>(hk).Value;
        }

        public HK<ConstA<M, R>, A> Unit<A>(A _)
        {
            return Wrap<A>(Const.Unit<M, R, A>(_));
        }

        public HK<ConstA<M, R>, B>
        Map<A, B>(Func<A, B> _, HK<ConstA<M, R>, A> a)
        {
            return Wrap<B>(Unwrap<A>(a).Cast<B>());
        }

        public HK<ConstA<M, R>, B>
        Ap<A, B>(HK<ConstA<M, R>, Func<A, B>> _, HK<ConstA<M, R>, A> a)
        {
            return Wrap<B>(Unwrap<A>(a).Cast<B>());
        }
    }
}
