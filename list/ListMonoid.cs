namespace sharper.c.data.list
{
    using data.lazy;
    using data.semigroup;

    public struct ListMonoid<A>
        : IMonoid<List<A>>
    {
        public List<A> Plus(List<A> x, List<A> y)
        {
            return x.Append(y);
        }

        public List<A> LazyPlus(List<A> x, Lazy<List<A>> y)
        {
            return x.Append(y.Force);
        }

        public List<A> Zero
        {
            get { return List.Nil<A>(); }
        }
    }
}