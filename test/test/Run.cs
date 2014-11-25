using System.Text.RegularExpressions;
using Microsoft.Build.Utilities;
using Fuchu;

namespace sharper.c.test
{
    using data.laws;

    public sealed class Run
        : Task
    {
        public static int Main(string[] args)
        {
            var sequential = args.Length > 0 && args[0] == "-seq";
            var filter = sequential && args.Length > 1
                ? args[1]
                : args.Length > 0 ? args[0] : string.Empty;
            return new Run().Go(sequential, filter);
        }

        public override bool Execute()
        {
            return Go() == 0;
        }

        private int Go(bool sequential = false, string filter = "")
        {
            var allTests = AllTests.Get().ToTest;
            var toTest = filter == ""
                ? allTests
                : Test.Where(allTests, s => Regex.Match(s, filter).Success);
            return sequential ? toTest.Run() : toTest.RunParallel();
        }
    }
}