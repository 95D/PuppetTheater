using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Viento.PuppetTheater.Utiltiy
{
    public class RandomPermutation
    {
        private readonly Random random;

        public RandomPermutation(Random random)
        {
            this.random = random;
        }

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
