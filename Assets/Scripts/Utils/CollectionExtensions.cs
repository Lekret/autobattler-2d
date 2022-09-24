using System.Collections.Generic;

namespace Utils
{
    public static class CollectionExtensions
    {
        public static Queue<T> ToQueue<T>(this IEnumerable<T> enumerable)
        {
            return new Queue<T>(enumerable);
        }
    }
}