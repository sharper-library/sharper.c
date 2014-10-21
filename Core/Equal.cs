using System;

namespace Sharper.C
{
    public interface IEqual<E, A>
        where E : struct, IEqual<E, A>
    {
        bool Eq(A a1, A a2);
    }

    public struct Equal<E, A>
        where E : struct, IEqual<E, A>
    {
        public Func<A, bool> Eq(A a1)
        {
            return a2 => default(E).Eq(a1, a2);
        }

        public bool Eq(A a1, A a2)
        {
            return default(E).Eq(a1, a2);
        }
    }

    public static class EqualMethods
    {
        public static bool Eq<A, EqA>(this A a1, A a2)
            where EqA : struct, IEqual<EqA, A>
        {
            return default(EqA).Eq(a1, a2);
        }
    }
}