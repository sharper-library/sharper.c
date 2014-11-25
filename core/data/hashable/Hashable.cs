using System;

namespace sharper.c.data.hashable
{
    public struct Hashable<H, A>
        where H : struct, IHashable<A>
    {
        public int Hash(A a)
        {
            return default(H).Hash(a);
        }

        public int HashWithSalt(int salt, A a)
        {
            return default(H).HashWithSalt(salt, a);
        }

        public Func<A, int> HashWithSalt(int salt)
        {
            return a => default(H).HashWithSalt(salt, a);
        }
    }
}