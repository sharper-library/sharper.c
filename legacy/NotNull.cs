namespace sharper.c.legacy.@null
{
    using data.equal;
    using data.semigroup;

    public struct NotNull<A, MonoidA>
        where MonoidA : struct, IMonoid<A>
        where A : class
    {
        public readonly A Value;

        internal NotNull(A a)
        {
            Value = a ?? default(MonoidA).Zero;
        }
    }

    public static class NotNull
    {
        public static NotNull<A, MonoidA> Mk<A, MonoidA>(A a)
            where MonoidA : struct, IMonoid<A>
            where A : class
        {
            return new NotNull<A, MonoidA>(a);
        }
    }

    public struct NotNullEqual<A, EqA, MonoidA>
        : IEqual<NotNull<A, MonoidA>>
        where EqA : struct, IEqual<A>
        where MonoidA : struct, IMonoid<A>
        where A : class
    {
        public bool Eq(NotNull<A, MonoidA> x, NotNull<A, MonoidA> y)
        {
            return default(EqA).Eq(x.Value, y.Value);
        }
    }

    public struct NotNullMonoid<A, MonoidA>
        : IMonoid<NotNull<A, MonoidA>>
        where MonoidA : struct, IMonoid<A>
        where A : class
    {
        public NotNull<A, MonoidA>
        Plus(NotNull<A, MonoidA> x, NotNull<A, MonoidA> y)
        {
            var M_ = default(MonoidA);
            return new NotNull<A, MonoidA>(M_.Plus(x.Value, y.Value));
        }

        public NotNull<A, MonoidA> Zero
        {
            get { return new NotNull<A, MonoidA>(default(MonoidA).Zero); }
        }
    }
}