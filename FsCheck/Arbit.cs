using System;
using System.Collections.Generic;
using FsCheck;

namespace Sharper.C.FsCheck
{
    public static class Arbit
    {
        public static Arbitrary<A> Create<A>(
                Gen<A> generator,
                Func<A, IEnumerable<A>> shrinker)
        {
            return new AnonymousArbitrary<A>(generator, shrinker);
        }

        private sealed class AnonymousArbitrary<A>
            : Arbitrary<A>
        {
            private readonly Gen<A> generator;
            private readonly Func<A, IEnumerable<A>> shrinker;

            public AnonymousArbitrary(
                    Gen<A> generator,
                    Func<A, IEnumerable<A>> shrinker)
            {
                this.generator = generator;
                this.shrinker = shrinker;
            }

            public override Gen<A> Generator
            {
                get { return generator; }
            }

            public override IEnumerable<A> Shrinker(A a)
            {
                return shrinker(a);
            }
        }
    }
}