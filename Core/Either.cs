using System;

namespace Sharper.C
{
    public struct Either<L, R>
    {
        private readonly bool isLeft;
        private readonly object value;

        internal Either(L left)
        {
            value = left;
            isLeft = true;
        }

        internal Either(R right)
        {
            value = right;
            isLeft = false;
        }

        public A Match<A>(Func<L, A> f, Func<R, A> g)
        {
            return isLeft ? f((L)value) : g((R)value);
        }

        public Either<L1, R> Lmap<L1>(Func<L, L1> f)
        {
            return isLeft
                ? new Either<L1, R>(f((L)value))
                : new Either<L1, R>((R)value);
        }

        public Either<L, R1> Rmap<R1>(Func<R, R1> f)
        {
            return isLeft
                ? new Either<L, R1>((L)value)
                : new Either<L, R1>(f((R)value));
        }

        public Either<L1, R1> Dimap<L1, R1>(Func<L, L1> f, Func<R, R1> g)
        {
            return isLeft
                ? new Either<L1, R1>(f((L)value))
                : new Either<L1, R1>(g((R)value));
        }

        public Either<L1, R> Lbind<L1>(Func<L, Either<L1, R>> f)
        {
            return isLeft ? f((L)value) : new Either<L1, R>((R)value);
        }

        public Either<L, R1> Rbind<R1>(Func<R, Either<L, R1>> f)
        {
            return isLeft ? new Either<L, R1>((L)value) : f((R)value);
        }

        public Either<L1, R> Lapply<L1>(Either<Func<L, L1>, R> f)
        {
            return f.isLeft
                ? Lmap((Func<L, L1>)f.value)
                : new Either<L1, R>((R)f.value);
        }

        public Either<L, R1> Rapply<R1>(Either<L, Func<R, R1>> f)
        {
            return f.isLeft
                ? new Either<L, R1>((L)f.value)
                : Rmap((Func<R, R1>)f.value);
        }

        public bool Eq<EqL, EqR>(Either<L, R> e)
            where EqL : struct, IEqual<EqL, L>
            where EqR : struct, IEqual<EqR, R>
        {
            var el = default(Equal<EqL, L>);
            var er = default(Equal<EqR, R>);
            return
                (isLeft && e.isLeft && el.Eq((L)value, (L)e.value))
                ||
                (!isLeft && !isLeft && er.Eq((R)value, (R)e.value));
        }
    }

    public static class Either
    {
        public static Either<L, R> Left<L, R>(L left)
        {
            return new Either<L, R>(left);
        }

        public static Either<L, R> Right<L, R>(R right)
        {
            return new Either<L, R>(right);
        }
    }

    public static class EitherLinq
    {
        public static Either<L, R1> Select<L, R, R1>(
                this Either<L, R> e,
                Func<R, R1> f)
        {
            return e.Rmap(f);
        }

        public static Either<L, R2> Select<L, R, R1, R2>(
                this Either<L, R> e,
                Func<R, Either<L, R1>> f,
                Func<R, R1, R2> g)
        {
            return e.Rbind(r => f(r).Rmap(r1 => g(r, r1)));
        }

        public static Either<L, R2> Join<L, K, R, R1, R2>(
                this Either<L, R> e1,
                Either<L, R1> e2,
                Func<R, K> _,
                Func<R1, K> __,
                Func<R, R1, R2> f)
        {
            return e2.Rapply(e1.Rmap(Fn.Curry(f)));
        }
    }

    public struct EitherEq<L, R, EqL, EqR>
        : IEqual<EitherEq<L, R, EqL, EqR>, Either<L, R>>
        where EqL : struct, IEqual<EqL, L>
        where EqR : struct, IEqual<EqR, R>
    {
        public bool Eq(Either<L, R> e1, Either<L, R> e2)
        {
            return e1.Eq<EqL, EqR>(e2);
        }
    }
}