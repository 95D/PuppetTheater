using Viento.PuppetTheater.Puppet;

namespace Viento.PuppetTheater.Node
{
    /// <summary>
    /// A [LeafNode] for acting puppet 
    /// </summary>
    public sealed class ActionNode : LeafNode
    {
        private readonly string actionId;
        private readonly long timeoutDurationMillis;
        public ActionNode(
            string nodeId,
            string actionId,
            long timeoutDurationMillis) : base(nodeId)
        {
            this.actionId = actionId;
            this.timeoutDurationMillis = timeoutDurationMillis;
        }

        public override NodeState CreateNodeStateAsReady()
        {
            return new TimeoutNodeState(nodeId, NodeLifeCycle.Ready);
        }

        public override TraversalState TraverseDown(
            string puppetId,
            IPuppetController puppetController,
            TraversalState traversalState,
            long currentMillis)
        {
            var resultLifeCycle = puppetController.RequestAction(puppetId, actionId);
            var nextNodeState = (traversalState.currentNodeState as TimeoutNodeState)
                .Start(currentMillis, resultLifeCycle.toNodeLifeCycle());
            return traversalState.UpdateCurrentNode(nextNodeState);
        }

        protected override NodeLifeCycle ExecuteInternal(
            string puppetId, 
            IPuppetController puppetController, 
            TraversalState traversalState, 
            long currentMillis)
        {
            bool isTimeout = (traversalState.currentNodeState as TimeoutNodeState)
                            .isTimeout(currentMillis, timeoutDurationMillis);
            if (isTimeout)
            {
                puppetController.CancelAction(puppetId, actionId);
                return NodeLifeCycle.Success;
            }
            else
                return puppetController.GetCurrentActionState(puppetId, actionId).toNodeLifeCycle();
        }
    }
}
