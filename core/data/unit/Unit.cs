using System;

namespace sharper.c.data.unit
{
    public struct Unit
    {
        public static Unit Value = default(Unit);

        public static Func<Unit> Fn(Action action)
        {
            return () => { action(); return Value; };
        }

        public static Func<A, Unit> Fn<A>(Action<A> action)
        {
            return a => { action(a); return Value; };
        }
    }
}