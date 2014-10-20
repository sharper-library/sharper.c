using System;

namespace Sharper.C
{
    public interface IEqual<E, A>
        where E : struct, IEqual<E, A>
    {
        bool Eq(A a1, A a2);
    }

    public static class Equal<E, A>
        where E : struct, IEqual<E, A>
    {
        public static Func<A, bool> Eq(A a1)
        {
            return a2 => default(E).Eq(a1, a2);
        }

        public static bool Eq(A a1, A a2)
        {
            return default(E).Eq(a1, a2);
        }
    }
}