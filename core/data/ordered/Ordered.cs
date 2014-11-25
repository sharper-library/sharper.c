using System;

namespace sharper.c.data.ordered
{
    public struct Ordered<O, A>
        where O : struct, IOrdered<A>
    {
        public bool Eq(A x, A y)
        {
            return default(O).Eq(x, y);
        }

        public Func<A, bool> Eq(A x)
        {
            return y => default(O).Eq(x, y);
        }

        public bool Neq(A x, A y)
        {
            return !default(O).Eq(x, y);
        }

        public Func<A, bool> Neq(A x)
        {
            return y => !default(O).Eq(x, y);
        }

        public Ordering Compare(A x, A y)
        {
            return default(O).Compare(x, y);
        }

        public Func<A, Ordering> Compare(A x)
        {
            return y => default(O).Compare(x, y);
        }

        public bool Lt(A x, A y)
        {
            return default(O).Compare(x, y) == Ordering.LT;
        }

        public Func<A, bool> Lt(A x)
        {
            return y => default(O).Compare(x, y) == Ordering.LT;
        }

        public bool Gt(A x, A y)
        {
            return default(O).Compare(x, y) == Ordering.GT;
        }

        public Func<A, bool> Gt(A x)
        {
            return y => default(O).Compare(x, y) == Ordering.GT;
        }

        public bool LtEq(A x, A y)
        {
            return default(O).Compare(x, y) != Ordering.GT;
        }

        public Func<A, bool> LtEq(A x)
        {
            return y => default(O).Compare(x, y) != Ordering.GT;
        }

        public bool GtEq(A x, A y)
        {
            return default(O).Compare(x, y) != Ordering.LT;
        }

        public Func<A, bool> GtEq(A x)
        {
            return y => default(O).Compare(x, y) != Ordering.LT;
        }

        public A Max(A x, A y)
        {
            return LtEq(x, y) ? y : x;
        }

        public Func<A, A> Max(A x)
        {
            var self = this;
            return y => self.Max(x, y);
        }

        public A Min(A x, A y)
        {
            return LtEq(x, y) ? x : y;
        }

        public Func<A, A> Min(A x)
        {
            var self = this;
            return y => self.Min(x, y);
        }

        public Func<B, B, Ordering> Comparing<B>(Func<B, A> f)
        {
            return (x, y) => default(O).Compare(f(x), f(y));
        }
    }
}