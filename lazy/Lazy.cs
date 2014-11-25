using System;

namespace sharper.c.data.lazy
{
    public struct Lazy<A>
    {
        private readonly System.Lazy<A> lazy;

        internal Lazy(System.Lazy<A> lazy)
        {
            this.lazy = lazy;
        }

        public Lazy<B> Map<B>(System.Func<A, B> f)
        {
            var self = this;
            return new Lazy<B>(new System.Lazy<B>(() => f(self.lazy.Value)));
        }

        public Lazy<B> Bind<B>(System.Func<A, Lazy<B>> f)
        {
            var self = this;
            return new Lazy<B>(
                    new System.Lazy<B>(() => f(self.lazy.Value).lazy.Value));
        }

        public A Force
        {
            get { return lazy.Value; }
        }
    }

    public static class Lazy
    {
        public static Lazy<A> Now<A>(A a)
        {
            return new Lazy<A>(new System.Lazy<A>(() => a));
        }

        public static Lazy<A> Defer<A>(System.Func<A> a)
        {
            return new Lazy<A>(new System.Lazy<A>(a));
        }

        public static A Force<A>(Lazy<A> a)
        {
            return a.Force;
        }
    }
}