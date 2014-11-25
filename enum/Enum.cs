namespace sharper.c.data.@enum
{
    using data.maybe;

    public struct Enum<E, A>
        where E : struct, IEnum<A>
    {
        public Maybe<A> ToEnum(int i)
        {
            return default(E).ToEnum(i);
        }

        public Maybe<int> FromEnum(A a)
        {
            return default(E).FromEnum(a);
        }

        public Maybe<A> Succ(A a)
        {
            return default(E).Succ(a);
        }

        public Maybe<A> Pred(A a)
        {
            return default(E).Pred(a);
        }
    }
}