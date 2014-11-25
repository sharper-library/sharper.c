using System;

namespace sharper.c.legacy
{
    using data.maybe;

    public static class Null
    {
        public static Maybe<A> ToMaybe<A>(this A a)
            where A : class
        {
            return ReferenceEquals(null, a)
                    ? Maybe.Nothing<A>()
                    : Maybe.Just(a);
        }

        public static Maybe<A> ToMaybe<A>(this A? a)
            where A : struct
        {
            return a.HasValue
                    ? Maybe.Just(a.Value)
                    : Maybe.Nothing<A>();
        }
    }
}