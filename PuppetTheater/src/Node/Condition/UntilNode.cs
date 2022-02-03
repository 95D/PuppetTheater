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
            BehaviorNode thenChild
            ) : base(nodeId, assertionId)
        {
            this.untilChild = thenChild;
        }

        public override TraversalState TraverseDown(
            string puppetId,
            IPuppetController puppetController,
            TraversalState traversalState,
            long currentMillis)
        {
            if (puppetController.Assert(puppetId, assertionId))
                return traversalState.PopNode().PushNode(untilChild.CreateNodeStateAsReady());
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
                return traversalState.PopNode().PushNode(untilChild.CreateNodeStateAsReady());
            else if (childNodeState.lifeCycle.isSucceeded())
                return traversalState.UpdateCurrentNodeLifeCycle(NodeLifeCycle.Success);
            else
                return traversalState.UpdateCurrentNodeLifeCycle(NodeLifeCycle.Failed);
        }
    }
}
