using System;

namespace sharper.c.control.resource
{
    using data.tuple;

    using State = sharper.c.data.tuple.Pair<Action, int>;

    internal static class S
    {
        public static readonly State Initial =
            Pair.Mk<Action, int>(() => {}, 1);

        public static Func<State, State> Register(Action r)
        {
            return s => s.MapFst<Action>(a => () => { a(); r(); });
        }

        public static Func<State, State> IncRefCount =
            s => s.MapSnd(i => i + 1);

        public static Func<State, State> ReleaseAll =
            s =>
            {
                if (s.Snd == 1)
                {
                    s.Fst();
                    return Initial;
                }
                return s.MapSnd(i => i - 1);
            };
    }
}