namespace sharper.c.data.bounded
{
    public interface IBounded<A>
    {
        A MinBound { get; }

        A MaxBound { get; }
    }
}