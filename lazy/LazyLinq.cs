namespace sharper.c.data.lazy
{
    public static class LazyLinq
    {
        public static Lazy<B> Select<A, B>(this Lazy<A> a, System.Func<A, B> f)
        {
            return a.Map(f);
        }

        public static Lazy<C> SelectMany<A, B, C>(
                this Lazy<A> a,
                System.Func<A, Lazy<B>> f,
                System.Func<A, B, C> g)
        {
            return new Lazy<C>(
                    new System.Lazy<C>(() =>
                            {
                                var a1 = a.Force;
                                var b = f(a1).Force;
                                return g(a1, b);
                            }));
        }
    }
}