using System;

namespace Sharper.C
{
    public interface IMonoid<M, A> : ISemigroup<M, A>
        where M : struct, IMonoid<M, A>
    {
        A Id { get; }
    }

    public struct Monoid<M, A>
        where M : struct, IMonoid<M, A>
    {
        public A Id
        {
            get { return default(M).Id; }
        }

        public Func<A, A> Op(A a1)
        {
            return a2 => default(M).Op(a1, a2);
        }

        public A Op(A a1, A a2)
        {
            return default(M).Op(a1, a2);
        }
    }
}