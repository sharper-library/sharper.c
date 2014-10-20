using System;
using Sharper.C.HigherKinds.Unsafe;

namespace Sharper.C.HigherKinds
{
    public struct Id<A>
    {
        public readonly A Value;

        internal Id(A a)
        {
            Value = a;
        }

        public Id<B> Map<B>(Func<A, B> f)
        {
            return new Id<B>(f(Value));
        }

        public Id<B> Apply<B>(Id<Func<A, B>> f)
        {
            return new Id<B>(f.Value(Value));
        }
    }

    public static class Id
    {
        public static Id<A> Unit<A>(A a)
        {
            return new Id<A>(a);
        }

        public static A Value<A>(Id<A> i)
        {
            return i.Value;
        }
    }

    public struct IdA
        : IApplicative<IdA>
    {
        public static HK<IdA, A> Wrap<A>(Id<A> ta)
        {
            return HKUnsafe<IdA>.Wrap<A>(ta);
        }

        public static Id<A> Unwrap<A>(HK<IdA, A> hk)
        {
            return (Id<A>) HKUnsafe<IdA>.Unwrap<A>(hk);
        }

        public static A Value<A>(HK<IdA, A> hk)
        {
            return Unwrap(hk).Value;
        }

        public HK<IdA, B> Map<A, B>(Func<A, B> f, HK<IdA, A> a)
        {
            return Wrap<B>(Unwrap<A>(a).Map(f));
        }

        public HK<IdA, B> Ap<A, B>(HK<IdA, Func<A, B>> f, HK<IdA, A> a)
        {
            return Wrap<B>(Unwrap<A>(a).Apply(Unwrap<Func<A, B>>(f)));
        }

        public HK<IdA, A> Unit<A>(A a)
        {
            return Wrap<A>(Id.Unit(a));
        }
    }
}
