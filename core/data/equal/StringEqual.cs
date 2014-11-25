namespace sharper.c.data.equal
{
    public struct StringEqual
        : IEqual<string>
    {
        public bool Eq(string x, string y)
        {
            return x == y;
        }
    }
}
