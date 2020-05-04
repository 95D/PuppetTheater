using System;
using System.Collections.Generic;
using Viento.PuppetTheater.Base;
using Viento.PuppetTheater.Utility;

namespace Viento.PuppetTheater.Node
{
    /// <summary>
    /// <para>This class is Behavior Node which composite children.</para> 
    /// <para>This class iterates executing child with random until child return true.</para>
    /// </summary>
    public class RandomSelectNode : CompositeBaseNode
    {
        private readonly RandomPermutation randomPermutation;

        public RandomSelectNode(
            string name,
            List<IBehaviorNode> children
            ) : base(name, children)
        {
            this.randomPermutation = new RandomPermutation(new Random());
        }

        public RandomSelectNode(
            string name,
            List<IBehaviorNode> children,
            RandomPermutation randomPermutation
            ) : base(name, children)
        {
            this.randomPermutation = randomPermutation;
        }

        protected override bool OnExecute(BehaviorContext context)
        {
            var indices = randomPermutation.Next(0, children.Count);
            
            foreach(int index in indices)
            {
                if (children[index].Execute(context))
                    return true;
            }
            return false;
        }
    }
}
