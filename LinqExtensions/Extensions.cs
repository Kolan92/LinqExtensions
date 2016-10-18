using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqExtensions
{
    public class GroupOfAdjacent<TSource, TKey> : IEnumerable<TSource>, IGrouping<TKey, TSource> {
        public TKey Key { get; private set; }
        private IList<TSource> GroupList { get; set; }
        public IEnumerator<TSource> GetEnumerator() {
            return GroupList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
        public GroupOfAdjacent(IList<TSource> source, TKey key) {
            GroupList = source;
            Key = key;
        }
    }

    public static class Extensions {

        public static IEnumerable<IGrouping<TKey, TSource>> GroupAdjecantBy<TKey, TSource>(
            this IEnumerable<TSource> collection, Func<TSource, TKey> selector) {

            var lastKey = default(TKey);
            var isFirst = true;
            var groupElements = new List<TSource>();

            foreach (TSource element in collection) {
                var key = selector(element);
                if (isFirst) {
                    isFirst = false;
                    lastKey = key;
                    groupElements.Add(element);
                }
                else {
                    if (lastKey.Equals(key)) {
                        groupElements.Add(element);
                    }
                    else {
                        yield return new GroupOfAdjacent<TSource, TKey>(groupElements, lastKey);
                        groupElements = new List<TSource>();
                        groupElements.Add(element);
                        lastKey = key;
                    }
                }
            }
            if (!isFirst)
                yield return new GroupOfAdjacent<TSource, TKey>(groupElements, lastKey);
        }
    }
}
