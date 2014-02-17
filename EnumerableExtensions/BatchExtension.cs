using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace EnumerableExtensions
{
    public static class BatchExtension
    {
        public static IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> source, int size)
        {
            Debug.Assert(source != null);
            Debug.Assert(size > 0);

            var batch = new T[size];
            var count = 0;

            foreach (var item in source)
            {
                batch[count++] = item;

                if (count != size) continue;

                yield return batch;
                batch = new T[size];
                count = 0;
            }

            if (count > 0)
            {
                yield return batch.Take(count).ToArray();
            }
        }
    }
}