namespace sharper.c.data.semigroup
{
    using control.trampoline;

    public struct Last<A>
        : ISemigroup<A>
    {
        public A Plus(A _, A y)
        {
            return y;
        }

        public Bounce<A> LazyPlus(Bounce<A> _, Bounce<A> y)
        {
            return y;
        }
    }
}
