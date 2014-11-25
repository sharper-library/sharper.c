using System;
using System.Collections.Generic;
using System.Linq;

namespace sharper.c.legacy.enumerable
{
    using data.list;
    using data.maybe;
    using data.tuple;

    public static class Enumerate
    {
        public static IEnumerable<A> Single<A>(A a)
        {
            yield return a;
        }

        public static Maybe<A> MaybeFirst<A>(this IEnumerable<A> e)
        {
            try
            {
                return Maybe.Just(e.First());
            }
            catch (InvalidOperationException)
            {
                return Maybe.Nothing<A>();
            }
        }

        public static List<A> ToList<A>(IEnumerable<A> e)
        {
            return List.UnfoldR<A, IEnumerator<A>>(
                    er => er.MoveNext()
                    ? Maybe.Just(Pair.Mk(er.Current, er))
                    : Maybe.Nothing<Pair<A, IEnumerator<A>>>(),
                    e.GetEnumerator());
        }

        public static IEnumerable<A> FromList<A>(List<A> l)
        {
            return l.ToEnumerable;
        }
    }
}