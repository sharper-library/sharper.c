using Fuchu;
using Sharper.C.Laws;

namespace Sharper.C.Tests
{
    public sealed class Run
    {
        public static int Main(string[] args)
        {
            return Law.RunParallel<AllTests>();
        }
    }
}