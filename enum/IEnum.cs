namespace sharper.c.data.@enum
{
    using data.maybe;

    public interface IEnum<A>
    {
        Maybe<A> Succ(A a);

        Maybe<A> Pred(A a);

        Maybe<A> ToEnum(int i);

        Maybe<int> FromEnum(A a);
    }
}