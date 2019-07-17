using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ywxt.Novel.Utils
{
    static class LinqExtensions
    {
        public static IEnumerable<IEnumerable<(int index, T element)>> Split<T>(this IEnumerable<T> list, int parts)
        {
            var collection = list.Select((element, index) => (index, element));
            var splits = from item in collection
                group item by item.index % parts into part
                select part.AsEnumerable();
            return splits;
        }
    }
}
