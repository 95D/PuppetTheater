using System;
using Viento.PuppetTheater.Puppet;

namespace Viento.PuppetTheater.Node
{
    /// <summary>
    /// A [DecorateNode] for processing `child node` if random threshold is activated
    /// </summary>
    public sealed class StochasticNode : DecorateNode
    {
        public readonly float threshold;
        private readonly Random random;

        public StochasticNode(
            string nodeId,
            BehaviorNode child,
            float threshold) : this(nodeId, child, threshold, new Random())
        {
        }

        public StochasticNode(
            string nodeId,
            BehaviorNode child,
            float threshold,
            Random random) : base(nodeId, child)
        {
            this.threshold = threshold;
            this.random = random;
        }

        public override TraversalState TraverseDown(
            string puppetId,
            IPuppetController puppetController,
            TraversalState traversalState,
            long currentMillis)
        {
            if (random.NextDouble() > threshold)
            {
                return traversalState.UpdateCurrentNodeLifeCycle(NodeLifeCycle.Running)
                    .PushNode(child.CreateNodeStateAsReady());
            }
            else
                return traversalState.UpdateCurrentNodeLifeCycle(NodeLifeCycle.Failed);
        }

        public override TraversalState TraverseUp(
            string puppetId,
            IPuppetController puppetController,
            TraversalState traversalState,
            NodeState childNodeState) => traversalState
                .UpdateCurrentNodeLifeCycle(childNodeState.lifeCycle);
    }
}
