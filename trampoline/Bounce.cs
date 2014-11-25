using System;

namespace sharper.c.control.trampoline
{
    public struct Bounce<A>
    {
        internal readonly IBounce<A> bounce;

        internal Bounce(IBounce<A> bounce)
        {
            this.bounce = bounce;
        }

        public Bounce<B> Map<B>(Func<A, B> f)
        {
            return new Bounce<B>(bounce.Bind(a => new Done<B>(f(a))));
        }

        public Bounce<B> Bind<B>(Func<A, Bounce<B>> f)
        {
            return new Bounce<B>(bounce.Bind(a => f(a).bounce));
        }

        public A Run()
        {
            var b = bounce;
            while (!(b is Done<A>))
            {
                b = b.Resume();
            }
            return ((Done<A>)b).Value;
        }
    }

    public static class Bounce
    {
        public static Bounce<A> Done<A>(A a)
        {
            return new Bounce<A>(new Done<A>(a));
        }

        public static Bounce<A> Next<A>(Func<Bounce<A>> f)
        {
            return new Bounce<A>(new Suspend<A>(() => f().bounce));
        }
    }

    internal interface IBounce<A>
    {
        IBounce<B> Bind<B>(Func<A, IBounce<B>> f);
        IBounce<A> Resume();
        IBounce<B> Resume<B>(Func<A, IBounce<B>> f);
    }

    internal struct Done<A>
        : IBounce<A>
    {
        public readonly A Value;

        public Done(A a)
        {
            Value = a;
        }

        public IBounce<B> Bind<B>(Func<A, IBounce<B>> f)
        {
            return new Gosub<A, B>(f, this);
        }

        public IBounce<A> Resume()
        {
            return this;
        }

        public IBounce<B> Resume<B>(Func<A, IBounce<B>> f)
        {
            return f(Value);
        }
    }

    internal struct Suspend<A>
        : IBounce<A>
    {
        private readonly Func<IBounce<A>> resume;

        public Suspend(Func<IBounce<A>> k)
        {
            resume = k;
        }

        public IBounce<B> Bind<B>(Func<A, IBounce<B>> f)
        {
            return new Gosub<A, B>(f, this);
        }

        public IBounce<A> Resume()
        {
            return resume();
        }

        public IBounce<B> Resume<B>(Func<A, IBounce<B>> f)
        {
            return resume().Bind(f);
        }
    }

    internal struct Gosub<A, B>
        : IBounce<B>
    {
        private readonly Func<A, IBounce<B>> bindf;
        private readonly IBounce<A> bounce;

        public Gosub(Func<A, IBounce<B>> f, IBounce<A> a)
        {
            bindf = f;
            bounce = a;
        }

        public IBounce<C> Bind<C>(Func<B, IBounce<C>> f)
        {
            var g = bindf;
            return new Gosub<A, C>(x => g(x).Bind(f), bounce);
        }

        public IBounce<B> Resume()
        {
            return bounce.Resume(bindf);
        }

        public IBounce<C> Resume<C>(Func<B, IBounce<C>> f)
        {
            return Bind(f);
        }
    }
}