namespace sharper.c.data.hashable
{
    public interface IHashable<A>
    {
        int Hash(A a);

        int HashWithSalt(int salt, A a);
    }
}