using System;

namespace Sharper.C
{
    public interface ISemigroup<S, A>
        where S : struct, ISemigroup<S, A>
    {
        A Op(A x, A y);
    }

    public struct Semigroup<S, A>
        where S : struct, ISemigroup<S, A>
    {
        public Func<A, A> Op(A a1)
        {
            return a2 => default(S).Op(a1, a2);
        }

        public A Op(A a1, A a2)
        {
            return default(S).Op(a1, a2);
        }
    }

    public static class SemigroupMethods
    {
        public static A SemiOp<S, A>(this A a1, A a2)
            where S : struct, ISemigroup<S, A>
        {
            return default(S).Op(a1, a2);
        }
    }
}