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

        public StochasticNode(
            string name,
            BehaviorNode child,
            float threshold
            ) : base(name, child)
        {
            this.threshold = threshold;
        }

        private TraversalState InvokeByThreshold(TraversalState traversalState, bool isSucceeded)
        {
            var randGen = new Random();
            if (randGen.NextDouble() > threshold)
            {
                return traversalState.UpdateCurrentNodeLifeCycle(NodeLifeCycle.Running)
                    .PushNode(child.CreateNodeStateAsReady());
            }
            else if (isSucceeded)
                return traversalState.UpdateCurrentNodeLifeCycle(NodeLifeCycle.Success);
            else
                return traversalState.UpdateCurrentNodeLifeCycle(NodeLifeCycle.Failed);
        }

        public override TraversalState TraverseDown(
            string puppetId,
            IPuppetController puppetController,
            TraversalState traversalState,
            long currentMillis) => InvokeByThreshold(traversalState, false);

        public override TraversalState TraverseUp(
            string puppetId,
            IPuppetController puppetController,
            TraversalState traversalState,
            NodeState childNodeState) => InvokeByThreshold(
                traversalState,
                childNodeState.lifeCycle.isSucceeded());
    }
}
