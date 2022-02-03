using Viento.PuppetTheater.Puppet;

namespace Viento.PuppetTheater.Node
{
    /// <summary>
    /// A [ConditionBaseNode] for branching `true -> then` and `false -> else`
    /// </summary>
    public sealed class IfElseNode : ConditionNode
    {
        public readonly BehaviorNode thenChild;
        public readonly BehaviorNode elseChild;

        public IfElseNode(
            string nodeId,
            string assertionId,
            BehaviorNode thenChild,
            BehaviorNode elseChild) : base(nodeId, assertionId)
        {
            this.thenChild = thenChild;
            this.elseChild = elseChild;
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
                return traversalState.PopNode().PushNode(elseChild.CreateNodeStateAsReady());
        }
    }
}
