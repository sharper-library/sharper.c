using System.Collections.Generic;
using Sharper.C;
using Fuchu;
using FC = Fuchu.FsCheck;

namespace Sharper.C.Laws
{
    public struct SemigroupLaws<S, E, A>
        : ILaws
        where S : struct, ISemigroup<S, A>
        where E : struct, IEqual<E, A>
    {
        public string Name() { return "Semigroup"; }

        public IEnumerable<ILaws> Subsumed() { yield break; }

        public IEnumerable<Test> Properties()
        {
            var e = default(Equal<E, A>);
            var s = default(Semigroup<S, A>);
            yield return FC.Property(
                    "Associativity",
                    (A a1, A a2, A a3) => e.Eq(
                            s.Op(s.Op(a1, a2), a3),
                            s.Op(a1, s.Op(a2, a3))));
        }
    }
}