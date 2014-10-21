using System.Collections.Generic;
using Sharper.C;
using Fuchu;
using FC = Fuchu.FsCheck;

namespace Sharper.C.Laws
{
    public struct MonoidLaws<M, E, A>
        : ILaws
        where M : struct, IMonoid<M, A>
        where E : struct, IEqual<E, A>
    {
        public string Name() {  return "Monoid"; }

        public IEnumerable<ILaws> Subsumed()
        {
            yield return default(SemigroupLaws<M, E, A>);
        }

        public IEnumerable<Test> Properties()
        {
            var e = default(Equal<E, A>);
            var m = default(Monoid<M, A>);
            yield return FC.Property(
                    "Left identity",
                    (A a) => e.Eq(a, m.Op(m.Id, a)));
            yield return FC.Property(
                    "Right identity",
                    (A a) => e.Eq(a, m.Op(a, m.Id)));
        }
    }
}