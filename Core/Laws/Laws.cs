using System.Collections.Generic;
using System.Linq;
using Fuchu;

namespace Sharper.C.Laws
{
    public interface ILaws
    {
        string Name();
        IEnumerable<ILaws> Subsumed();
        IEnumerable<Test> Properties();
    }

    public struct LawsComparer
        : IEqualityComparer<ILaws>
    {
        public bool Equals(ILaws l1, ILaws l2)
        {
            return l1.Name() == l2.Name();
        }

        public int GetHashCode(ILaws l)
        {
            return l.GetHashCode();
        }
    }

    public static class Law
    {
        public static int Run<L>()
            where L : struct, ILaws
        {
            return ToTest<L>().Run();
        }

        public static int RunParallel<L>()
            where L : struct, ILaws
        {
            return ToTest<L>().RunParallel();
        }

        public static Test ToTest<L>()
            where L : struct, ILaws
        {
            var distinct = Linearize(default(L)).Distinct(default(LawsComparer))
                .Select(l => Test.List(l.Name(), l.Properties().ToArray()));
            return Test.List(distinct);
        }

        private static IEnumerable<ILaws> Linearize(ILaws laws)
        {
            return Enumerable.Repeat(laws, 1)
                .Concat(laws.Subsumed().SelectMany(Linearize));
        }
    }
}