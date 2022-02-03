using Viento.PuppetTheater.Puppet;

namespace Viento.PuppetTheater.Node
{
    /// <summary>
    /// A [ConditionBaseNode] for branching `true -> then` and `false -> do nothing`
    /// </summary>
    public sealed class IfNode : ConditionNode
    {
        public readonly BehaviorNode thenChild;

        public IfNode(
            string nodeId,
            string assertionId,
            BehaviorNode thenChild) : base(nodeId, assertionId)
        {
            this.thenChild = thenChild;
        }

        public override TraversalState TraverseDown(
            string puppetId,
            IPuppetController puppetController,
            TraversalState traversalState,
            long currentMillis)
        {
            if (puppetController.Assert(puppetId, assertionId))
                return traversalState.PopNode().PushNode(thenChild.CreateNodeStateAsReady());
            else
                return traversalState.UpdateCurrentNodeLifeCycle(NodeLifeCycle.Failed);
        }
    }
}
