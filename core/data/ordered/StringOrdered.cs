using System;

namespace sharper.c.data.ordered
{
    public struct StringOrdered
        : IOrdered<string>
    {
        public bool Eq(string x, string y)
        {
            return string.Equals(x, y, StringComparison.InvariantCulture);
        }

        public Ordering Compare(string x, string y)
        {
            return StringOrderedDefaults
                .Compare(x, y, StringComparison.InvariantCulture);
        }
    }

    public struct StringNoCaseOrdered
        : IOrdered<string>
    {
        public bool Eq(string x, string y)
        {
            return string.Equals(
                    x,
                    y,
                    StringComparison.InvariantCultureIgnoreCase);
        }

        public Ordering Compare(string x, string y)
        {
            return StringOrderedDefaults
                .Compare(x, y, StringComparison.InvariantCultureIgnoreCase);
        }
    }

    public struct StringOrdinalOrdered
        : IOrdered<string>
    {
        public bool Eq(string x, string y)
        {
            return string.Equals(x, y, StringComparison.Ordinal);
        }

        public Ordering Compare(string x, string y)
        {
            return StringOrderedDefaults
                .Compare(x, y, StringComparison.Ordinal);
        }
    }

    public struct StringOrdinalNoCaseOrdered
        : IOrdered<string>
    {
        public bool Eq(string x, string y)
        {
            return string.Equals(x, y, StringComparison.OrdinalIgnoreCase);
        }

        public Ordering Compare(string x, string y)
        {
            return StringOrderedDefaults
                .Compare(x, y, StringComparison.OrdinalIgnoreCase);
        }
    }

    internal static class StringOrderedDefaults
    {
        public static Ordering Compare(string x, string y, StringComparison c)
        {
            var n = string.Compare(x, y, c);
            return n < 0 ? Ordering.LT : (n > 0 ? Ordering.GT : Ordering.EQ);
        }
    }
}