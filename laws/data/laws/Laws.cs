using System.Collections.Generic;
using System.Linq;
using Fuchu;

namespace sharper.c.data.laws
{
    public struct Laws
    {
        private readonly string name;
        private readonly IEnumerable<Test> properties;

        private Laws(string name, IEnumerable<Test> properties)
        {
            this.name = name;
            this.properties = properties;
        }

        public static Laws Mk(string name, params Test[] properties)
        {
            return new Laws(name, properties);
        }

        public Laws Append(Laws l)
        {
            return new Laws(
                    name,
                    properties.Concat(Enumerable.Repeat(l.ToTest, 1)));
        }

        public Test ToTest
        {
            get { return Test.List(name, properties.ToArray()); }
        }

        public static Laws operator +(Laws l1, Laws l2)
        {
            return l1.Append(l2);
        }
    }
}