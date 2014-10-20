using System;

namespace Sharper.C
{
    public struct Bounce<A>
    {
        private readonly bool isDone;
        private readonly object value;

        private Bounce(object value, bool isDone)
        {
            this.value = value;
            this.isDone = isDone;
        }

        internal static Bounce<A> Done(A value)
        {
            return new Bounce<A>(value, true);
        }

        internal static Bounce<A> Next(Func<Bounce<A>> value)
        {
            return new Bounce<A>(value, false);
        }

        public A Run()
        {
            var bounce = this;
            while (!bounce.isDone)
            {
                bounce = ((Func<Bounce<A>>)bounce.value)();
            }
            return (A)bounce.value;
        }
    }

    public static class Bounce
    {
        public static Bounce<A> Done<A>(A value)
        {
            return Bounce<A>.Done(value);
        }

        public static Bounce<A> Next<A>(Func<Bounce<A>> next)
        {
            return Bounce<A>.Next(next);
        }
    }
}