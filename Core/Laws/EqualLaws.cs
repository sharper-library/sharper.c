using Sharper.C;

namespace Sharper.C.Laws
{
    public abstract class EqualLaws<E, A>
        where E : struct, IEqual<E, A>
    {
        public bool Symmetry(A a1, A a2)
        {
            return Equal<E, A>.Eq(a1, a2) == Equal<E, A>.Eq(a2, a1);
        }
    }
}