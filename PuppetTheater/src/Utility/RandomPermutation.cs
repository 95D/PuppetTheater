using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Viento.PuppetTheater.Utiltiy
{
    /// <summary>
    /// This class is used for making permutation collection.
    /// </summary>
    public class RandomPermutation
    {
        private readonly Random random;

        public RandomPermutation(Random random)
        {
            this.random = random;
        }

        /// <summary>
        /// <para>Get Random permutated list.</para>
        /// <para>List elements is minimum ~ (maximum - 1)</para>
        /// <para>How do it: Choice random index and separate space without chosen index. and repeat process for two space.</para>
        /// </summary>
        /// <param name="minimum">start</param>
        /// <param name="maximum">end + 1</param>
        /// <returns></returns>
        public virtual ImmutableList<int> Next(int minimum, int maximum)
        {
            List<int> list = new List<int>();
            if (minimum == maximum)
                list.Add(minimum);
            else
                AddIndexRecursive(minimum, maximum-1, list);

            return ImmutableList.Create(list.ToArray());
        }

        private void AddIndexRecursive(int minimum, int maximum, List<int> list)
        {
            int next = random.Next(minimum, maximum);
            list.Add(next);
            if(next + 1 <= maximum)
            {
                AddIndexRecursive(next + 1, maximum, list);
            }
            if(next - 1 >= minimum)
            {
                AddIndexRecursive(minimum, next - 1, list);
            }
        }
    }
}
