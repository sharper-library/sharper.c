using System;
using System.Collections;
using System.Collections.Generic;

namespace sharper.c.data.list
{
    internal struct ListEnumerable<A>
        : IEnumerable<A>
    {
        private readonly List<A> list;

        public ListEnumerable(List<A> list)
        {
            this.list = list;
        }

        public IEnumerator<A> GetEnumerator()
        {
            return new ListEnumerator<A>(list);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}