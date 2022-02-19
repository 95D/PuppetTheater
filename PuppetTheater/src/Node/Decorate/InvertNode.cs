using Viento.PuppetTheater.Puppet;

namespace Viento.PuppetTheater.Node
{
    /// <summary>
    /// A [DecorateNode] for returning inverted result to parent
    /// </summary>
    public sealed class InvertNode : DecorateNode
    {
        public InvertNode(
            string nodeId,
            BehaviorNode child
            ) : base(nodeId, child)
        {
        }

        public override TraversalState TraverseDown(
            string puppetId,
            IPuppetController puppetController,
            TraversalState traversalState,
            long currentMillis) => traversalState.UpdateCurrentNodeLifeCycle(NodeLifeCycle.Running)
                .PushNode(child.CreateNodeStateAsReady());

        public override TraversalState TraverseUp(
            string puppetId,
            IPuppetController puppetController,
            TraversalState traversalState,
            NodeState childNodeState)
        {
            if (childNodeState.lifeCycle.IsSucceeded())
            {
                return traversalState.UpdateCurrentNodeLifeCycle(NodeLifeCycle.Failed);
            }
            else
            {
                return traversalState.UpdateCurrentNodeLifeCycle(NodeLifeCycle.Success);
            }
        }
    }
}
