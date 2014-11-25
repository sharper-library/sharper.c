namespace sharper.c.data.@enum
{
    using data.maybe;

    public static class EnumDefaults<E, A>
        where E : struct, IEnum<A>
    {
        public static Maybe<A> Succ(A a)
        {
            var E_ = default(E);
            return E_.FromEnum(a).Bind(
                    i => i == int.MaxValue
                        ? Maybe.Nothing<A>()
                        : E_.ToEnum(i + 1));
        }

        public static Maybe<A> Pred(A a)
        {
            var E_ = default(E);
            return E_.FromEnum(a).Bind(
                    i => i == int.MinValue
                        ? Maybe.Nothing<A>()
                        : E_.ToEnum(i - 1));
        }
    }
}