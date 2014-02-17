using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class Stub : IEnumerable<int>
    {
        public int CallsToGetEnumerator = 0;
        public int IndexOfYield = 0;

        public IEnumerator<int> GetEnumerator()
        {
            CallsToGetEnumerator++;
            while (true)
            {
                yield return IndexOfYield++;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
