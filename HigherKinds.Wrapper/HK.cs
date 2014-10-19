namespace Sharper.C.HigherKinds.Wrapper
{
    public struct HK<T, A>
        where T : struct
    {
        internal readonly object Value;

        internal HK(object ta)
        {
            Value = ta;
        }
    }

    public struct HK<T, A, B>
        where T : struct
    {
        internal readonly object Value;

        internal HK(object tab)
        {
            Value = tab;
        }
    }
}
