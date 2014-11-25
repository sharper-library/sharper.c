namespace sharper.c.data.@enum
{
    using data.maybe;

    public struct IntEnum
        : IEnum<int>
    {
        public Maybe<int> Succ(int x)
        {
            return Maybe.When(() => x + 1, x == int.MaxValue);
        }

        public Maybe<int> Pred(int x)
        {
            return Maybe.When(() => x - 1, x == int.MinValue);
        }

        public Maybe<int> ToEnum(int i)
        {
            return Maybe.Just(i);
        }

        public Maybe<int> FromEnum(int x)
        {
            return Maybe.Just(x);
        }
    }
}