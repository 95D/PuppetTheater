using Viento.PuppetTheater.Puppet;

namespace Viento.PuppetTheater.Node
{
    /// <summary>
    /// A [DecorateNode] for returning result as `Failed` to parent
    /// </summary>
    public sealed class ForceFailureNode : DecorateNode
    {
        public ForceFailureNode(
            string nodeId,
            BehaviorNode child
            ) : base(nodeId, child)
        {
        }

        public override TraversalState TraverseDown(
            string puppetId,
            IPuppetController puppetController,
            TraversalState traversalState,
            long currentMillis) => traversalState.UpdateCurrentNodeLifeCycle(NodeLifeCycle.Failed)
                .PushNode(child.CreateNodeStateAsReady());

        public override TraversalState TraverseUp(
            string puppetId,
            IPuppetController puppetController,
            TraversalState traversalState,
            NodeState childNodeState) => traversalState;
    }
}
