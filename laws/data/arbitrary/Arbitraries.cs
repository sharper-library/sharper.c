using System;
using System.Linq;
using FsCheck;
using FsCheck.Fluent;

namespace sharper.c.data.arbitrary
{
    using control.trampoline;
    using data.lazy;
    using data.list;
    using data.maybe;
    using data.tuple;
    using data.semigroup;
    using legacy.enumerable;
    using legacy.@null;

    public sealed class Arbitraries
    {
        public static Arbitrary<Maybe<A>> MaybeArb<A>()
        {
            var nothing = Gen.constant(Maybe.Nothing<A>());
            var just = Arb.generate<A>().Select(Maybe.Just<A>);
            return Arbit.Mk(
                    Gen.frequency(new [] {
                            Tuple.Create(1, nothing),
                            Tuple.Create(9, just)
                        }),
                    m => m.IsNothing
                        ? Enumerable.Empty<Maybe<A>>()
                        : Enumerable.Repeat(Maybe.Nothing<A>(), 1));
        }

        public static Arbitrary<Lazy<A>> LazyArb<A>()
        {
            return Arbit.Mk(Arb.generate<A>().Select(Lazy.Now));
        }

        public static Arbitrary<Pair<A, B>> PairArb<A, B>()
        {
            return Arbit.Mk(
                    from a in Arb.generate<A>()
                    from b in Arb.generate<B>()
                    select Pair.Mk(a, b));
        }

        public static Arbitrary<List<A>> ListArb<A>()
        {
            return Arb.Default.Array<A>().Convert(
                    Enumerate.ToList<A>,
                    l => Enumerate.FromList<A>(l).ToArray());
        }

        public static Arbitrary<Bounce<A>> BounceArb<A>()
        {
            return Arbit.Mk(Arb.generate<A>().Select(Bounce.Done));
        }

        public static Arbitrary<NotNull<A, MonoidA>> NotNullArb<A, MonoidA>()
            where MonoidA : struct, IMonoid<A>
            where A : class
        {
            return Arbit.Mk(
                    Arb.generate<A>().Select(NotNull.Mk<A, MonoidA>),
                    na => Arb.shrink<A>(na.Value)
                        .Select(NotNull.Mk<A, MonoidA>));
        }
    }
}