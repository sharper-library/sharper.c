namespace Sharper.C.HigherKinds.Wrapper.Unsafe
{
    public static class HKUnsafe<T>
        where T : struct
    {
        public static HK<T, A> Wrap<A>(object tb)
        {
            return new HK<T, A>(tb);
        }

        public static object Unwrap<A>(HK<T, A> hk)
        {
            return hk.Value;
        }
    }

    public static class HK2Unsafe<T>
        where T : struct
    {
        public static HK<T, A, B> Wrap<A, B>(object tb)
        {
            return new HK<T, A, B>(tb);
        }

        public static object Unwrap<A, B>(HK<T, A, B> hk)
        {
            return hk.Value;
        }
    }
}
