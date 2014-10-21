using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using Fuchu;
using Sharper.C.Laws;

namespace Sharper.C.Tests
{
    public class Run
        : Task
    {
        public static int Main(string[] args)
        {
            return new Run().Go();
        }

        public override bool Execute()
        {
            return Go() == 0;
        }

        private int Go()
        {
            return Law.RunParallel<AllTests>();
        }
    }
}