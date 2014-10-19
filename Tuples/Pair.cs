using System;
using Sharper.C;
using Sharper.C.Lenses;

namespace Sharper.C.Tuples
{
    public struct Pair<A, B>
    {
        internal readonly A Fst;
        internal readonly B Snd;

        internal Pair(A a, B b)
        {
            Fst = a;
            Snd = b;
        }

        public Pair<C, B> MapFst<C>(Func<A, C> f)
        {
            return new Pair<C, B>(f(Fst), Snd);
        }

        public Pair<A, C> MapSnd<C>(Func<B, C> f)
        {
            return new Pair<A, C>(Fst, f(Snd));
        }
    }

    public static class Pair
    {
        public static Pair<A, B> Mk<A, B>(A a, B b)
        {
            return new Pair<A, B>(a, b);
        }

        public static ILens<Pair<A, R>, Pair<B, R>, A, B> Fst<F, R, A, B>()
        {
            return Lens.Mk<Pair<A, R>, Pair<B, R>, A, B>(
                p => p.Fst,
                p => b => Mk(b, p.Snd));
        }

        public static ILens<Pair<R, A>, Pair<R, B>, A, B> Snd<F, R, A, B>()
        {
            return Lens.Mk<Pair<R, A>, Pair<R, B>, A, B>(
                p => p.Snd,
                p => b => Mk(p.Fst, b));
        }
    }
}