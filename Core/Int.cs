namespace Sharper.C
{
    public struct IntEq
        : IEqual<IntEq, int>
    {
        public bool Eq(int a, int b)
        {
            return a == b;
        }
    }

    public struct IntSumMonoid
        : IMonoid<IntSumMonoid, int>
    {
        public int Op(int a, int b)
        {
            return a + b;
        }

        public int Id
        {
            get { return 0; }
        }
    }

    public struct IntProductMonoid
        : IMonoid<IntProductMonoid, int>
    {
        public int Op(int a, int b)
        {
            return a * b;
        }

        public int Id
        {
            get { return 1; }
        }
    }
}