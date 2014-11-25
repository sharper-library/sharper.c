namespace sharper.c.data.kinded
{
    public struct HK<T, A>
        where T : struct
    {
        internal readonly object UnKind;

        internal HK(object a)
        {
            UnKind = a;
        }
    }

    public struct HK<T, A, B>
        where T : struct
    {
        internal readonly object UnKind;

        internal HK(object ab)
        {
            UnKind = ab;
        }
    }
}