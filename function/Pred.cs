using System;

namespace sharper.c.data.function
{
    public static class Pred
    {
        public static bool Implies(bool p, bool q)
        {
            return !p || q;
        }
    }
}