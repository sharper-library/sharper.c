using System;

namespace Sharper.C.HigherKinds
{
    internal enum FreeCtor { Return, Suspend, Gosub }

    public struct Free<F, A>
        where F : struct, IFunctor<F>
    {
        private readonly FreeCtor ctor;
        private readonly object value;

        private Free(object value, FreeCtor ctor)
        {
            this.value = value;
            this.ctor = ctor;
        }

        internal static Free<F, A> Return(A a)
        {
            return new Free<F, A>(a, FreeCtor.Return);
        }

        internal static Free<F, A> Suspend(HK<F, Free<F, A>> a)
        {
            return new Free<F, A>(a, FreeCtor.Suspend);
        }

        internal static Free<F, A>
        Gosub<A0>(Free<F, A0> a, Func<A0, Free<F, A>> f)
        {
            return new Free<F, A>(Tuple.Create(a, f), FreeCtor.Gosub);
        }

        public Free<F, B> Map<B>(Func<A, B> f)
        {
            return Bind(a => Free<F, B>.Return(f(a)));
        }

        public Free<F, B> Bind<B>(Func<A, Free<F, B>> f)
        {
            if (ctor == FreeCtor.Gosub)
            {
                var a =
                    (Tuple<Free<F, object>, Func<object, Free<F, A>>>) value;
                return Free<F, B>.Gosub(
                        a.Item1,
                        x => Free<F, B>.Gosub(a.Item2(x), f));
            }
            return Free<F, B>.Gosub(this, f);
        }
    }

    public static class Free<F>
        where F : struct, IFunctor<F>
    {
        public static Free<F, A> Return<A>(A a)
        {
            return Free<F, A>.Return(a);
        }

        public static Free<F, A> Suspend<A>(HK<F, Free<F, A>> a)
        {
            return Free<F, A>.Suspend(a);
        }
    }
}