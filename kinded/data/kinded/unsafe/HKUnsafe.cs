namespace sharper.c.data.kinded.@unsafe
{
    public static class HkUnsafe<T>
        where T : struct
    {
        public static HK<T, A> Wrap<A>(object a)
        {
            return new HK<T, A>(a);
        }

        public static object Unwrap<A>(HK<T, A> a)
        {
            return a.UnKind;
        }
    }

    public static class Hk2Unsafe<T>
        where T : struct
    {
        public static HK<T, A, B> Wrap<A, B>(object ab)
        {
            return new HK<T, A, B>(ab);
        }

        public static object Unwrap<A, B>(HK<T, A, B> ab)
        {
            return ab.UnKind;
        }
    }
}