namespace sharper.c.data.semigroup
{
    public struct IntMultMonoid
        : IMonoid<int>
    {
        public int Plus(int x, int y)
        {
            return x * y;
        }

        public int Zero
        {
            get { return 1; }
        }
    }
}
