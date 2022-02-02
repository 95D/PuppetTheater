using Viento.PuppetTheater.Puppet;

namespace Viento.PuppetTheater.Node
{
    /// <summary>
    /// A [DecorateNode] for returning result as `Success` to parent
    /// </summary>
    public sealed class ForceSuccessNode : DecorateNode
    {
        public ForceSuccessNode(
            string nodeId,
            BehaviorNode child
            ) : base(nodeId, child)
        {
        }

        public override TraversalState TraverseDown(
            string puppetId,
            IPuppetController puppetController,
            TraversalState traversalState,
            long currentMillis) => traversalState.UpdateCurrentNodeLifeCycle(NodeLifeCycle.Success)
                .PushNode(child.CreateNodeStateAsReady());

        public override TraversalState TraverseUp(
            string puppetId,
            IPuppetController puppetController,
            TraversalState traversalState,
            NodeState childNodeState) => traversalState;
    }
}
