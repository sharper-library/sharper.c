using System;
using Sharper.C.HigherKinds.Wrapper;

namespace Sharper.C.HigherKinds
{
    public interface IApplicative<T>
        : IApply<T>
        where T : struct, IApplicative<T>
    {
        HK<T, A> Unit<A>(A a);
    }

    public static class Applicative<T>
        where T : struct, IApplicative<T>
    {
        public static HK<T, A> Unit<A>(A a)
        {
            return default(T).Unit(a);
        }
    }
}
