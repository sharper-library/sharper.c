using System;
using System.Collections.Generic;

namespace sharper.c.data.list
{
    using control.trampoline;
    using data.box;
    using data.function;
    using data.lazy;
    using data.maybe;
    using data.semigroup;
    using data.tuple;

    public struct Cons<A>
    {
        internal readonly Pair<A, Box<List<A>>> value;

        internal Cons(Pair<A, Box<List<A>>> value)
        {
            this.value = value;
        }

        internal static Cons<A> Mk(A head, List<A> tail)
        {
            return new Cons<A>(Pair.Mk(head, Box.Mk(tail)));
        }

        public A Head
        {
            get { return value.Fst; }
        }

        public List<A> Tail
        {
            get { return value.Snd.Unbox; }
        }

        public B With<B>(Func<A, List<A>, B> f)
        {
            return f(Head, Tail);
        }

        public Pair<A, List<A>> ToPair
        {
            get { return Pair.Mk(Head, Tail); }
        }

        public List<A> ToList
        {
            get
            {
                var val = value;
                return new List<A>(Lazy.Now(Maybe.Just(val)));
            }
        }

        public IEnumerable<A> ToEnumerable
        {
            get { return ToList.ToEnumerable; }
        }

        public B LazyFoldRight<B>(Func<A, Lazy<B>, B> f, B b)
        {
            return ToList.LazyFoldRight(f, b);
        }

        public Bounce<B>
        BouncedFoldRight<B>(Func<A, Bounce<B>, Bounce<B>> f, B b)
        {
            return ToList.BouncedFoldRight(f, b);
        }

        public B FoldRight<B>(Func<A, B, B> f, B b)
        {
            return ToList.FoldRight(f, b);
        }

        public B FoldLeft<B>(Func<B, A, B> f, B b)
        {
            return ToList.FoldLeft(f, b);
        }

        public A Fold<SemiA>()
            where SemiA : struct, ISemigroup<A>
        {
            var S_ = default(SemiA);
            return Tail.FoldLeft(S_.Plus, Head);
        }

        public Bounce<A> BouncedFold<SemiA>()
            where SemiA : struct, ILazySemigroup<A>
        {
            var S_ = default(SemiA);
            return Fn.Recur<Cons<A>, A>(
                    foldr => cons =>
                    Bounce.Next(() =>
                    cons.Tail.ToCons.Match(
                            () => Bounce.Done(cons.Head),
                            c => S_.LazyPlus(
                                    Bounce.Done(cons.Head), foldr(c)))))
                (this);
        }

        public Cons<A> Append(List<A> list)
        {
            return Mk(Head, Tail.Append(list));
        }

        public Cons<A> Append(Cons<A> cons)
        {
            return Append(cons.ToList);
        }

        public Cons<B> Map<B>(Func<A, B> f)
        {
            return Cons<B>.Mk(f(Head), Tail.Map(f));
        }

        public Cons<B> Bind<B>(Func<A, Cons<B>> f)
        {
            return f(Head).Append(Tail.Bind(a => f(a).ToList));
        }

        public Cons<B> Apply<B>(Cons<Func<A, B>> f)
        {
            return Map(f.Head).Append(Tail.Apply(f.ToList));
        }

        public List<A> Filter(Func<A, bool> f)
        {
            return ToList.Filter(f);
        }

        public Cons<Pair<A, B>> Zip<B>(Cons<B> other)
        {
            return ZipWith(other, Pair.Mk);
        }

        public Cons<C> ZipWith<B, C>(Cons<B> other, Func<A, B, C> f)
        {
            return other.Apply(Map(Fn.Curry(f)));
        }
    }

    public static class Cons
    {
        public static Cons<A> Mk<A>(A head, List<A> tail)
        {
            return Cons<A>.Mk(head, tail);
        }
    }
}