namespace sharper.c.data.bounded
{
    public struct Bounded<T, A>
        where T : struct, IBounded<A>
    {
        public A MinBound
        {
            get { return default(T).MinBound; }
        }

        public A MaxBound
        {
            get { return default(T).MaxBound; }
        }
    }
}