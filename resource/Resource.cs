using System;

namespace sharper.c.control.resource
{
    using data.@ref;
    using data.unit;

    using State = sharper.c.data.tuple.Pair<Action, int>;

    public struct Resource<A>
    {
        private readonly Func<Ref<State>, A> run;

        internal Resource(Func<Ref<State>, A> run)
        {
            this.run = run;
        }

        public Resource<B> Map<B>(Func<A, B> f)
        {
            var go = run;
            return new Resource<B>(s => f(go(s)));
        }

        public Resource<B> Bind<B>(Func<A, Resource<B>> f)
        {
            var go = run;
            return new Resource<B>(s => f(go(s)).run(s));
        }

        public A Run
        {
            get { return RunFork(Ref.Mk(S.Initial)); }
        }

        private A RunFork(Ref<State> s)
        {
            try
            {
                return run(s);
            }
            finally
            {
                s.Update(S.ReleaseAll);
            }
        }
    }

    public static class Resource
    {
        public static Resource<A> Pure<A>(A a)
        {
            return new Resource<A>(_ => a);
        }

        public static Resource<Unit> Register(Action release)
        {
            return new Resource<Unit>(s => s.Update(S.Register(release)));
        }

        public static Resource<A> Acquire<A>(Func<A> grab, Action<A> release)
        {
            return new Resource<A>(
                    s =>
                    {
                        var resource = grab();
                        s.Update(S.Register(() => release(resource)));
                        return resource;
                    });
        }

        public static Resource<A> Acquire<A>(Func<A> grab)
            where A : IDisposable
        {
            return Acquire(grab, a => a.Dispose());
        }
    }
}