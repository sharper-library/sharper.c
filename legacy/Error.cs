using System;

namespace sharper.c.legacy
{
    using data.maybe;

    public static class Error
    {
        public static Maybe<A> CatchNull<A>(this Func<A> a)
        {
            try
            {
                return Maybe.Just(a());
            }
            catch (NullReferenceException)
            {
                return Maybe.Nothing<A>();
            }
        }
    }
}