namespace sharper.c.data.semigroup
{
    public struct StringMonoid
        : IMonoid<string>
    {
        public string Plus(string x, string y)
        {
            return string.Concat(x, y);
        }

        public string Zero
        {
            get { return string.Empty; }
        }
    }
}