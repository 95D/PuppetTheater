using Viento.PuppetTheater.Puppet;

namespace Viento.PuppetTheater.Node
{
    /// <summary>
    /// A [DecorateNode] for Repeat [count]
    /// </summary>
    public class RepeatNode : DecorateNode
    {
        public readonly int count;

        public RepeatNode(
            string nodeId,
            BehaviorNode child,
            int count
            ) : base(nodeId, child)
        {
            this.count = count;
        }

        public override NodeState CreateNodeStateAsReady() =>
            new RepeatNodeState(count, nodeId, NodeLifeCycle.Ready);

        public override TraversalState TraverseDown(
            string puppetId,
            IPuppetController puppetController,
            TraversalState traversalState,
            long currentMillis)
        {
            var currentNodeState = traversalState.currentNodeState as RepeatNodeState;
            if (currentNodeState.isIterationFinished())
            {
                return traversalState.UpdateCurrentNodeLifeCycle(NodeLifeCycle.Failed);
            }
            else
            {
                return traversalState
                    .UpdateCurrentNodeLifeCycle(NodeLifeCycle.Running)
                    .PushNode(child.CreateNodeStateAsReady());
            }
        }

        public override TraversalState TraverseUp(
            string puppetId,
            IPuppetController puppetController,
            TraversalState traversalState,
            NodeState childNodeState)
        {
            var currentNodeState = traversalState.currentNodeState as RepeatNodeState;
            if (currentNodeState.isIterationFinished())
            {
                return traversalState.UpdateCurrentNodeLifeCycle(NodeLifeCycle.Success);
            }
            else
            {
                return traversalState.PushNode(child.CreateNodeStateAsReady());
            }
        }
    }
}
