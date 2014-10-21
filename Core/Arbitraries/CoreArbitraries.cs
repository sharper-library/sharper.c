using System;
using System.Linq;
using FsCheck;
using FsCheck.Fluent;
using Sharper.C;
using Sharper.C.FsCheck;

namespace Sharper.C.Arbitraries
{
    public sealed class CoreArbitraries
    {
        public static Arbitrary<Maybe<A>> ArbitraryMaybe<A>()
        {
            var nothing = Gen.constant(Maybe.Nothing<A>());
            var just = Arb.generate<A>().Select(Maybe.Just<A>);
            return Arbit.Create(
                    Gen.frequency(new [] {
                            Tuple.Create(1, nothing),
                            Tuple.Create(9, just)
                        }),
                    _ => Enumerable.Repeat(Maybe.Nothing<A>(), 1));
        }

        public static Arbitrary<Either<L, R>> ArbitraryEither<L, R>()
        {
            return Gen.oneof(new[] {
                    Arb.generate<L>().Select(Either.Left<L, R>),
                    Arb.generate<R>().Select(Either.Right<L, R>)
                }).ToArbitrary();
        }
    }
}