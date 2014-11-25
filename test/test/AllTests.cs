using FsCheck;
using Fuchu;

namespace sharper.c.test
{
    using data.arbitrary;
    using data.@int;
    using data.laws;
    using data.list;
    using data.maybe;
    using data.ordered;
    using data.tuple;

    public static class AllTests
    {
        public static Laws Get()
        {
            Arb.register<Arbitraries>();
            return Laws.Mk("sharper.c")
                + IntTests.Get()
                + OrderingTests.Get()
                + MaybeTests.Get()
                + ListTests.Get()
                + PairTests.Get();
        }
    }
}