using System;
using System.Collections.Generic;
using System.Linq;

namespace sharper.c.data.list
{
    using control.trampoline;

    using data.box;
    using data.function;
    using data.@enum;
    using data.lazy;
    using data.maybe;
    using data.semigroup;
    using data.these;
    using data.tuple;

    public struct List<A>
    {
        internal readonly Lazy<Maybe<Pair<A, Box<List<A>>>>> value;

        internal List(Lazy<Maybe<Pair<A, Box<List<A>>>>> value)
        {
            this.value = value;
        }

        internal static List<A> Nil()
        {
            return new List<A>(
                    Lazy.Defer(Maybe.Nothing<Pair<A, Box<List<A>>>>));
        }

        internal static List<A> Cons(A head, List<A> tail)
        {
            return new List<A>(
                    Lazy.Defer(() => Maybe.Just(Pair.Mk(head, Box.Mk(tail)))));
        }

        internal static List<A> LazyCons(A head, Lazy<List<A>> tail)
        {
            return new List<A>(
                    tail.Map(t => Maybe.Just(Pair.Mk(head, Box.Mk(t)))));
        }

        public bool IsNil
        {
            get { return value.Force.IsNothing; }
        }

        public bool IsCons
        {
            get { return value.Force.IsJust; }
        }

        public Maybe<A> Head
        {
            get { return value.Force.Map(Pair.Fst); }
        }

        public Maybe<List<A>> Tail
        {
            get { return value.Force.Map(p => p.Snd.Unbox); }
        }

        public List<A> TailOrNil
        {
            get { return Tail.ValueOr(Nil); }
        }

        public Maybe<Cons<A>> ToCons
        {
            get { return value.Force.Map(v => new Cons<A>(v)); }
        }

        public IEnumerable<A> ToEnumerable
        {
            get { return new ListEnumerable<A>(this); }
        }

        public B LazyFoldRight<B>(Func<A, Lazy<B>, B> f, B b)
        {
            Func<List<A>, Lazy<B>> foldr = null;
            foldr = list =>
                list.value.Map(
                        m => m.Map(
                                p => p.With(
                                        (h, t) => f(h, foldr(t.Unbox))))
                        .ValueOr(Lazy.Now(b)));
            return foldr(this).Force;
        }

        public Bounce<B>
        BouncedFoldRight<B>(Func<A, Bounce<B>, Bounce<B>> f, B b)
        {
            return Fn.Recur<List<A>, B>(
                    foldr => list =>
                    Bounce.Next(() =>
                    list.ToCons.Match(
                            () => Bounce.Done(b),
                            c => f(c.Head, foldr(c.Tail)))))
                (this);
        }

        public B FoldRight<B>(Func<A, B, B> f, B b)
        {
            return ToEnumerable.Reverse().Aggregate(b, Fn.Flip(f));
        }

        public B FoldLeft<B>(Func<B, A, B> f, B b)
        {
            return ToEnumerable.Aggregate(b, f);
        }

        public Bounce<A> BouncedFold<MonoidA>()
            where MonoidA : struct, ILazyMonoid<A>
        {
            var M_ = default(MonoidA);
            return BouncedFoldRight<A>(
                    (x, y) => M_.LazyPlus(Bounce.Done(x), y),
                    M_.Zero);
        }

        public A Fold<MonoidA>()
            where MonoidA : struct, IMonoid<A>
        {
            var M_ = default(MonoidA);
            return FoldLeft<A>(M_.Plus, M_.Zero);
        }

        private static Func<List<A>, Maybe<Pair<A, List<A>>>>
        TakeWhileStep(Func<A, bool> f)
        {
            return list =>
                list.ToCons.Bind(cons => Maybe.When(cons.ToPair, f(cons.Head)));
        }

        public List<A> TakeWhile(Func<A, bool> f)
        {
            return List.UnfoldR(TakeWhileStep(f), this);
        }

        public List<A> Take(int count)
        {
            return ZipWith(List.FromToInt(0, count - 1), (a, _) => a);
        }

        public List<A> DropWhile(Func<A, bool> f)
        {
            return Fn.Recur<List<A>, List<A>>(
                    dropWhile => list =>
                    list.ToCons.Map(
                            cons => f(cons.Head)
                                ? dropWhile(cons.Tail)
                                : Bounce.Done(list))
                        .ValueOr(Lazy.Now(Bounce.Done(List.Nil<A>()))))
                (this).Run();
        }

        public List<A> Drop(int count)
        {
            return Fn.Recur<Pair<int, List<A>>,  List<A>>(
                    drop => arg => arg.With(
                            (n, list) => n == 0 || list.IsNil
                            ? Bounce.Done(list)
                            : drop(Pair.Mk(n - 1, list.TailOrNil))))
                (Pair.Mk(count, this)).Run();
        }

        public List<A> Append(List<A> other)
        {
            return LazyFoldRight(LazyCons, other);
        }

        public List<B> Map<B>(Func<A, B> f)
        {
            return LazyFoldRight<List<B>>(
                    (a, bs) => new List<B>(
                            bs.Map(
                                    bs1 => Maybe.Just(
                                            Pair.Mk(
                                                    f(a),
                                                    Box.Mk(bs1))))),
                    List<B>.Nil());
        }

        public List<B> Bind<B>(Func<A, List<B>> f)
        {
            return LazyFoldRight<List<B>>(
                    (a, bs) => new List<B>(
                            bs.Map(
                                    bs1 => f(a).Append(bs1).ToCons
                                        .Map(c => c.value))),
                    List<B>.Nil());
        }

        public List<B> Apply<B>(List<Func<A, B>> f)
        {
            return f.Bind(Map);
        }

        public List<A> Filter(Func<A, bool> f)
        {
            return LazyFoldRight<List<A>>(
                    (a, bs) => f(a) ? LazyCons(a, bs) : bs.Force,
                    List<A>.Nil());
        }

        public List<Pair<A, B>> Zip<B>(List<B> other)
        {
            return ZipWith(other, Pair.Mk);
        }

        public List<C> ZipWith<B, C>(List<B> other, Func<A, B, C> f)
        {
            return other.Apply(Map(Fn.Curry(f)));
        }

        private static
        Func<List<A>, List<B>, Maybe<Pair<C, Pair<List<A>, List<B>>>>>
        AlignStep<B, C>(Func<These<A, B>, C> f)
        {
            return (aa, bb) => These.FromMaybes(aa.Head, bb.Head)
                .Map(th => Pair.Mk(f(th), Pair.Mk(aa.TailOrNil, bb.TailOrNil)));
        }

        public List<C> AlignWith<B, C>(List<B> other, Func<These<A, B>, C> f)
        {
            return List.UnfoldR(
                    p => p.With(AlignStep(f)),
                    Pair.Mk(this, other));
        }

        public List<These<A, B>> Align<B>(List<B> other)
        {
            return AlignWith(other, Fn.Id);
        }

        private static
        Func<List<A>, List<B>, Maybe<Pair<C, Pair<List<A>, List<B>>>>>
        PairwiseZipStep<B, C>(Func<A, B, C> f)
        {
            return (aa, bb) => aa.Head.ZipWith(
                    bb.Head,
                    (a, b) => Pair.Mk(
                            f(a, b),
                            Pair.Mk(aa.TailOrNil, bb.TailOrNil)));
        }

        public List<C> PairwiseZipWith<B, C>(List<B> other, Func<A, B, C> f)
        {
            return List.UnfoldR(
                    p => p.With(PairwiseZipStep(f)),
                    Pair.Mk(this, other));
        }

        public List<Pair<A, B>> PairwiseZip<B>(List<B> other)
        {
            return PairwiseZipWith(other, Pair.Mk);
        }
    }

    public static class List
    {
        public static List<A> Nil<A>()
        {
            return List<A>.Nil();
        }

        public static List<A> Cons<A>(A head, List<A> tail)
        {
            return List<A>.Cons(head, tail);
        }

        public static List<A> LazyCons<A>(A head, Lazy<List<A>> tail)
        {
            return List<A>.LazyCons(head, tail);
        }

        public static List<A> UnfoldR<A, B>(Func<B, Maybe<Pair<A, B>>> f, B b)
        {
            return f(b)
                .Map(
                        p => p.With(
                                (a, b1) => LazyCons(
                                        a,
                                        Lazy.Defer(() => UnfoldR(f, b1)))))
                .ValueOr(List.Nil<A>);
        }

        public static Func<A, List<A>> JustIterate<A>(Func<A, Maybe<A>> f)
        {
            return a => f(a)
                .Map(
                        a1 => LazyCons(
                                a1,
                                Lazy.Defer(
                                        () => f(a1).Match(
                                                List.Nil<A>,
                                                JustIterate(f)))))
                .ValueOr(List.Nil<A>);
        }

        public static List<A> JustIterate<A>(Func<A, Maybe<A>> f, A a)
        {
            return JustIterate<A>(f)(a);
        }

        public static List<A> Iterate<A>(Func<A, A> f, A a)
        {
            return LazyCons(a, Lazy.Defer(() => Iterate(f, f(a))));
        }

        public static List<A> From<A, EnumA>(A a)
            where EnumA : struct, IEnum<A>
        {
            return JustIterate(default(EnumA).Succ, a);
        }

        public static List<A> FromTo<A, EnumA>(A a, A to)
            where EnumA : struct, IEnum<A>
        {
            var E_ = default(EnumA);
            var toN = E_.FromEnum(to);
            return JustIterate(
                    a1 => E_.FromEnum(a1).ZipWith(toN, (x, y) => x <= y)
                        .Bind(b => b ? E_.Succ(a1) : Maybe.Nothing<A>()),
                    a);
        }

        public static List<int> FromInt(int x)
        {
            return Iterate(i => i + 1, x);
        }

        public static List<int> FromToInt(int a, int b)
        {
            return JustIterate(i => Maybe.When(i + 1, i <= b), a);
        }

        public static List<int> Naturals
        {
            get { return UnfoldR(n => Maybe.Just(Pair.Mk(n, n + 1)), 1); }
        }

        public static List<int> Naturals1
        {
            get { return Naturals.Map(n => n + 1); }
        }

        public static List<bool> Bools
        {
            get { return Naturals.Map(n => { System.Console.Write("XX " + n); return n <= 10; }); }
        }

        public static int Sum()
        {
            return Naturals.FoldRight((a, b) => a + b, 0);
        }
    }
}