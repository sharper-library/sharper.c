using System;
using System.Collections;
using System.Collections.Generic;

namespace sharper.c.data.list
{
    using data.lazy;
    using data.maybe;
    using data.tuple;

    internal struct ListEnumerator<A>
        : IEnumerator<A>
    {
        private List<A> list;

        public ListEnumerator(List<A> list)
        {
            this.list = list;
        }

        public void Reset()
        {
            // Reset requires keeping a reference to the list head. This
            // prevents garbage collection of the list.
            throw new NotSupportedException();
        }

        void IDisposable.Dispose()
        {
        }

        public bool MoveNext()
        {
            list = list.Tail.ValueOr(List.Nil<A>);
            return list.IsCons;
        }

        public A Current
        {
            get
            {
                return list.Head.ValueOr(
                        () =>
                            {
                                throw new InvalidOperationException();
                            });
            }
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }
    }
}