using System.Collections.Generic;
using Sharper.C;
using Fuchu;
using FC = Fuchu.FsCheck;

namespace Sharper.C.Laws
{
    public struct EqualLaws<E, A>
        : ILaws
        where E : struct, IEqual<E, A>
    {
        public string Name() { return "Equal"; }

        public IEnumerable<ILaws> Subsumed() { yield break; }

        public IEnumerable<Test> Properties()
        {
            var e = default(Equal<E, A>);
            yield return FC.Property(
                    "Symmetry",
                    (A a1, A a2) => e.Eq(a1, a2) == e.Eq(a2, a1));
        }
    }
}