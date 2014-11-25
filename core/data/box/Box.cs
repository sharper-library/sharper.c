namespace sharper.c.data.box
{
    public sealed class Box<A>
        where A : struct
    {
        public readonly A Unbox;

        internal Box(A a)
        {
            Unbox = a;
        }
    }

    public static class Box
    {
        public static Box<A> Mk<A>(A a)
            where A : struct
        {
            return new Box<A>(a);
        }
    }
}