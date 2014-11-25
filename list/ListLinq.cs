using System;

namespace sharper.c.data.list
{
    using data.function;

    public static class ListLinq
    {
        public static List<B> Select<A, B>(this List<A> l, Func<A, B> f)
        {
            return l.Map(f);
        }

        public static List<C> SelectMany<A, B, C>(
                this List<A> l,
                Func<A, List<B>> f,
                Func<A, B, C> g)
        {
            return l.Bind(a => f(a).Map(b => g(a, b)));
        }

        public static List<C> Join<A, B, C, K>(
                this List<A> la,
                List<B> lb,
                Func<A, K> _,
                Func<B, K> __,
                Func<A, B, C> f)
        {
            return lb.Apply(la.Map(Fn.Curry(f)));
        }

        public static List<A> Where<A>(this List<A> l, Func<A, bool> f)
        {
            return l.Filter(f);
        }
    }
}