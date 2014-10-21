using System.Collections.Generic;
using Fuchu;
using Sharper.C.Laws;

namespace Sharper.C.Tests
{
    public struct AllTests
        : ILaws
    {
        public string Name()
        {
            return "Sharper.C.Tests";
        }

        public IEnumerable<ILaws> Subsumed()
        {
            yield return default(IntTests);
        }

        public IEnumerable<Test> Properties()
        {
            yield break;
        }
    }
}