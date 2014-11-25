using System;

namespace sharper.c.data.tuple
{
    public struct Pair<A, B>
    {
        public readonly A Fst;
        public readonly B Snd;

        internal Pair(A a, B b)
        {
            Fst = a;
            Snd = b;
        }

        public C With<C>(Func<A, B, C> f)
        {
            return f(Fst, Snd);
        }

        public Pair<C, B> MapFst<C>(Func<A, C> f)
        {
            return new Pair<C, B>(f(Fst), Snd);
        }

        public Pair<A, C> MapSnd<C>(Func<B, C> f)
        {
            return new Pair<A, C>(Fst, f(Snd));
        }

        public override string ToString()
        {
            return "Pair(" + Fst + ", " + (Snd == null ? "null" : Snd.ToString()) + ")";
        }
    }

    public static class Pair
    {
        public static Pair<A, B> Mk<A, B>(A a, B b)
        {
            return new Pair<A, B>(a, b);
        }

        public static Func<Pair<A, B>, C> With<A, B, C>(Func<A, B, C> f)
        {
            return p => f(p.Fst, p.Snd);
        }

        public static A Fst<A, B>(Pair<A, B> p)
        {
            return p.Fst;
        }

        public static B Snd<A, B>(Pair<A, B> p)
        {
            return p.Snd;
        }
    }
}