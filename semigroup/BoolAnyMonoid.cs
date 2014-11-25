namespace sharper.c.data.semigroup
{
    using control.trampoline;

    public struct BoolAnyMonoid
        : ILazyMonoid<bool>
    {
        public bool Plus(bool x, bool y)
        {
            return x || y;
        }

        public Bounce<bool> LazyPlus(Bounce<bool> x, Bounce<bool> y)
        {
            return x.Bind(x1 => x1 ? Bounce.Done(x1) : y);
        }

        public bool Zero
        {
            get { return false; }
        }
    }
}
