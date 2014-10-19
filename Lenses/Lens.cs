using System;
using Sharper.C;
using Sharper.C.HigherKinds;
using Sharper.C.HigherKinds.Wrapper;

namespace Sharper.C.Lenses
{
    public interface ILens<S, T, A, B>
    {
        Func<S, HK<F, T>> Apply<F>(Func<A, HK<F, B>> f)
            where F : struct, IFunctor<F>;
    }

    public static class Lens
    {
        public static ILens<S, T, A, B> Mk<S, T, A, B>(
            Func<S, A> get,
            Func<S, Func<B, T>> set)
        {
            return new GetSetLens<S, T, A, B>(get, set);
        }

        public static Func<S, A> View<S, A>(ILens<S, S, A, A> l)
        {
            return Fn.Comp(ConstF<A>.Value<S>, l.Apply(ConstF<A>.Mk<A>));
        }

        public static Func<B, Func<S, T>> Set<S, T, A, B>(ILens<S, T, A, B> l)
        {
            return d => Fn.Comp(
                IdA.Value<T>,
                l.Apply(
                    Fn.Const<HK<IdA, B>, A>(Applicative<IdA>.Unit(d))));
        }

        public static Func<Func<A, B>, Func<S, T>> Over<S, T, A, B>(
            ILens<S, T, A, B> l)
        {
            return fab => Fn.Comp(
                IdA.Value<T>,
                l.Apply(Fn.Comp(Applicative<IdA>.Unit, fab)));
        }

        private struct GetSetLens<S, T, A, B> : ILens<S, T, A, B>
        {
            private readonly Func<S, A> getter;
            private readonly Func<S, Func<B, T>> setter;

            public GetSetLens(
                Func<S, A> get,
                Func<S, Func<B, T>> set)
            {
                getter = get;
                setter = set;
            }

            public Func<S, HK<F, T>> Apply<F>(Func<A, HK<F, B>> f)
                where F : struct, IFunctor<F>
            {
                var set = setter;
                var get = getter;
                return a => Functor<F>.Map(set(a)) (f(get(a)));
            }
        }
    }
}
