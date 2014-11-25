namespace sharper.c.data.ordered
{
    public struct IntOrdered
        : IOrdered<int>
    {
        public bool Eq(int x, int y)
        {
            return x == y;
        }

        public Ordering Compare(int x, int y)
        {
            return x == y ? Ordering.EQ : x < y ? Ordering.LT : Ordering.GT;
        }
    }
}