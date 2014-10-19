using System;
using Sharper.C;
using Sharper.C.HigherKinds;
using Sharper.C.HigherKinds.Wrapper;

namespace Sharper.C.Lenses
{
    public interface IIso<S, T, A, B>
    {
        HK<P, S, HK<F, T>> Apply<P, F>(HK<P, A, HK<F, B>> ab)
            where P : struct, IProfunctor<P>
            where F : struct, IFunctor<F>;
    }

    public static class Iso
    {
        public static IIso<S, T, A, B>
        Mk<S, T, A, B>(Func<S, A> sa, Func<B, T> bt)
        {
            return new ToFromIso<S, T, A, B>(sa, bt);
        }

        private struct ToFromIso<S, T, A, B>
            : IIso<S, T, A, B>
        {
            private readonly Func<S, A> sa;
            private readonly Func<B, T> bt;

            public ToFromIso(Func<S, A> sa, Func<B, T> bt)
            {
                this.sa = sa;
                this.bt = bt;
            }

            public HK<P, S, HK<F, T>> Apply<P, F>(HK<P, A, HK<F, B>> ab)
                where P : struct, IProfunctor<P>
                where F : struct, IFunctor<F>
            {
                return Profunctor<P>.Dimap(sa, Functor<F>.Map(bt)) (ab);
            }
        }
    }
}
