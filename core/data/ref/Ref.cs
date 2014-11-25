using System;

namespace sharper.c.data.@ref
{
    using data.unit;

    public sealed class Ref<A>
    {
        private A value;
        private readonly object updateLock = new Object();

        internal Ref(A a)
        {
            value = a;
        }

        public A View
        {
            get { return value; }
        }

        public Unit Update(Func<A, A> f)
        {
            lock (updateLock)
            {
                value = f(value);
                return Unit.Value;
            }
        }
    }

    public static class Ref
    {
        public static Ref<A> Mk<A>(A a)
        {
            return new Ref<A>(a);
        }
    }
}