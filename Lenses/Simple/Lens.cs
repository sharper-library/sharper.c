using System;
using Sharper.C;

namespace Sharper.C.Lenses.Simple
{
    public delegate Func<A, Tuple<A, B>> Lens<A, B>(Func<B, B> f);

    public static class L
    {
        public static Lens<A, B> Mk<A, B>(Func<A, B> get, Func<A, B, A> set)
        {
            return f => a =>
                {
                    var b = f(get(a));
                    return Tuple.Create(set(a, b), b);
                };
        }

        public static Lens<A, B> Mk<A, B>(
            Func<A, B> get,
            Func<Func<B, B>, A, A> update)
        {
            return f => a =>
                {
                    var a1 = update(f, a);
                    return Tuple.Create(a1, get(a1));
                };
        }

        public static Func<A, B> View<A, B>(Lens<A, B> l)
        {
            return a => l(Fn.Id)(a).Item2;
        }

        public static Func<B, Func<A, A>> Set<A, B>(Lens<A, B> l)
        {
            return b => a => l(Fn.Const<B, B>(b))(a).Item1;
        }

        public static Func<Func<B, B>, Func<A, A>> Update<A, B>(Lens<A, B> l)
        {
            return f => a => l(f)(a).Item1;
        }

        public static Lens<A, C> Comp<A, B, C>(
            Lens<B, C> lbc,
            Lens<A, B> lab)
        {
            return fc => a => MapTuple(View(lbc), lab(Update(lbc)(fc))(a));
        }

        private static Tuple<A, C> MapTuple<A, B, C>(
            Func<B, C> f,
            Tuple<A, B> t)
        {
            return Tuple.Create(t.Item1, f(t.Item2));
        }
    }
}
