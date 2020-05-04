using System;
using Viento.PuppetTheater.Base;

namespace Viento.PuppetTheater.Node
{
    /// <summary>
    /// This class is BehaviorNode for processing child according to stochastic activation.
    /// </summary>
    public class StochasticNode : BehaviorNode
    {
        public readonly float threshold;
        public readonly IBehaviorNode child;

        public StochasticNode(
            string name,
            IBehaviorNode child,
            float threshold
            ) : base(name)
        {
            this.child = child;
            this.threshold = threshold;
        }

        protected override bool OnExecute(BehaviorContext context)
        {
            var randGen = new Random();
            if (randGen.NextDouble() > threshold)
                return child.Execute(context);
            else
                return false;
        }
    }
}
