using System;

namespace sharper.c.data.either
{
    using data.lazy;

    public struct Either<A, B>
    {
        private readonly bool isRight;
        private readonly A a;
        private readonly B b;

        private Either(A a, B b, bool isRight)
        {
            this.a = a;
            this.b = b;
            this.isRight = isRight;
        }

        internal static Either<A, B> Left(A a)
        {
            return new Either<A, B>(a, default(B), false);
        }

        internal static Either<A, B> Right(B b)
        {
            return new Either<A, B>(default(A), b, true);
        }

        public bool IsLeft
        {
            get { return !isRight; }
        }

        public bool IsRight
        {
            get { return isRight; }
        }

        public C Match<C>(Func<A, C> left, Func<B, C> right)
        {
            return isRight ? right(b) : left(a);
        }

        public Either<C, B> MapLeft<C>(Func<A, C> f)
        {
            return isRight ? Either<C, B>.Right(b) : Either<C, B>.Left(f(a));
        }

        public Either<A, C> MapRight<C>(Func<B, C> f)
        {
            return isRight ? Either<A, C>.Right(f(b)) : Either<A, C>.Left(a);
        }

        public Either<C, B> BindLeft<C>(Func<A, Either<C, B>> f)
        {
            return isRight ? Either<C, B>.Right(b) : f(a);
        }

        public Either<A, C> BindLeft<C>(Func<B, Either<A, C>> f)
        {
            return isRight ? f(b) : Either<A, C>.Left(a);
        }

        public A LeftOr(Lazy<A> or)
        {
            return isRight ? or.Force : a;
        }

        public A LeftOr(Func<A> or)
        {
            return isRight ? or() : a;
        }

        public B RightOr(Lazy<B> or)
        {
            return isRight ? b : or.Force;
        }

        public B RightOr(Func<B> or)
        {
            return isRight ? b : or();
        }
    }

    public static class Either
    {
        public static Either<A, B> Left<A, B>(A a)
        {
            return Either<A, B>.Left(a);
        }

        public static Either<A, B> Right<A, B>(B b)
        {
            return Either<A, B>.Right(b);
        }
    }
}