using System.Collections.Generic;
using Fuchu;
using Sharper.C;
using Sharper.C.Laws;

namespace Sharper.C.Tests
{
    public struct IntTests
        : ILaws
    {
        public string Name() { return "Int"; }

        public IEnumerable<ILaws> Subsumed()
        {
            yield return default(EqualLaws<IntEq, int>);
            yield return default(MonoidLaws<IntSumMonoid, IntEq, int>);
            yield return default(MonoidLaws<IntProductMonoid, IntEq, int>);
        }

        public IEnumerable<Test> Properties() { yield break; }
    }
}