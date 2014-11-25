namespace sharper.c.data.semigroup
{
    using control.trampoline;

    public interface ILazySemigroup<A>
        : ISemigroup<A>
    {
        Bounce<A> LazyPlus(Bounce<A> x, Bounce<A> y);
    }
}
