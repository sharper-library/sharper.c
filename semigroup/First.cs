namespace sharper.c.data.semigroup
{
    using control.trampoline;

    public struct First<A>
        : ILazySemigroup<A>
    {
        public A Plus(A x, A _)
        {
            return x;
        }

        public Bounce<A> LazyPlus(Bounce<A> x, Bounce<A> _)
        {
            return x;
        }
    }
}