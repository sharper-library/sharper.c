namespace sharper.c.data.bounded
{
    public struct IntBounded
        : IBounded<int>
    {
        public int MaxBound
        {
            get { return int.MaxValue; }
        }

        public int MinBound
        {
            get { return int.MinValue; }
        }
    }
}