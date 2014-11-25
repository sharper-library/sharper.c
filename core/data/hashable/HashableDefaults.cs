namespace sharper.c.data.hashable
{
    public static class HashableDefaults<H, A>
        where H : IHashable<A>
    {
        public static int Hash(A a)
        {
            return default(H).HashWithSalt(DefaultSalt, a);
        }

        public static int HashWithSalt(int salt, A a)
        {
            return Combine(salt, default(H).Hash(a));
        }

        private static int DefaultSalt = 0x087fc72c;

        // Combine two hash values. Left identity is 0.
        private static int Combine(int x, int y)
        {
            return (x * 16777619) ^ y;
        }
    }
}