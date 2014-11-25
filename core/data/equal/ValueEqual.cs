namespace sharper.c.data.equal
{
    public struct ValueEqual<A>
        : IEqual<A>
        where A : struct
    {
        public bool Eq(A x, A y)
        {
            return x.Equals(y);
        }
    }
}
