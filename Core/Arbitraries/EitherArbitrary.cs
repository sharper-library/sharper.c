using FsCheck;
using FsCheck.Fluent;
using Sharper.C;

namespace Sharper.C.Arbitraries
{
    public sealed class CoreArbitraries
    {
        public static Arbitrary<Either<L, R>> ArbitraryEither<L, R>()
        {
            return Gen.oneof(new[] {
                    Arb.generate<L>().Select(Either.Left<L, R>),
                    Arb.generate<R>().Select(Either.Right<L, R>)
                }).ToArbitrary();
        }
    }
}