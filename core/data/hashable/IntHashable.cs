namespace sharper.c.data.hashable
{
    public struct IntHashable
        : IHashable<int>
    {
        public int Hash(int x)
        {
            return x;
        }

        public int HashWithSalt(int salt, int x)
        {
            return HashableDefaults<IntHashable, int>.HashWithSalt(salt, x);
        }
    }
}