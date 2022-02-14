using Viento.PuppetTheater.Puppet;

namespace Viento.PuppetTheater.Node
{
    /// <summary>
    /// A [ConditionBaseNode] for iterating `until` until assertion becomes false
    /// </summary>
    public sealed class UntilNode : ConditionNode
    {
        public readonly BehaviorNode untilChild;

        public UntilNode(
            string nodeId,
            string assertionId,
            BehaviorNode untilChild
            ) : base(nodeId, assertionId)
        {
            this.untilChild = untilChild;
        }

        public override TraversalState TraverseDown(
            string puppetId,
            IPuppetController puppetController,
            TraversalState traversalState,
            long currentMillis)
        {
            if (puppetController.Assert(puppetId, assertionId))
                return traversalState
                    .UpdateCurrentNodeLifeCycle(NodeLifeCycle.Running)
                    .PushNode(untilChild.CreateNodeStateAsReady());
            else
                return traversalState.UpdateCurrentNodeLifeCycle(NodeLifeCycle.Failed);
        }

        public override TraversalState TraverseUp(
            string puppetId,
            IPuppetController puppetController,
            TraversalState traversalState,
            NodeState childNodeState)
        {
            if (puppetController.Assert(puppetId, assertionId))
                return traversalState.PushNode(untilChild.CreateNodeStateAsReady());
            else
                return traversalState.UpdateCurrentNodeLifeCycle(NodeLifeCycle.Success);
        }
    }
}
