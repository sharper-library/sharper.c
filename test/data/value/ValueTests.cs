namespace sharper.c.data.value
{
    using data.laws;
    using data.equal;

    public struct ValueTests
    {
        public static Laws Get()
        {
            return Laws.Mk("value type")
                + EqualLaws.Get<ValueEqual<int>, int>();
        }
    }
}
