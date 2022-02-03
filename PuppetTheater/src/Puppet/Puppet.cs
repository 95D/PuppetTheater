using Viento.PuppetTheater.Node;
using Viento.PuppetTheater.Tree;

namespace Viento.PuppetTheater.Puppet
{
    /// <summary>
    /// A product instance to manage Behavior tree for a entity
    /// </summary>
    public sealed class Puppet
    {
        private readonly BehaviorTree behaviorTree;
        private TraversalState traversalState;
        private readonly string puppetId;

        public Puppet(string puppetId, string rootId)
        {
            this.puppetId = puppetId;
            traversalState = new TraversalState(behaviorTree.GetBehaviorNode(rootId).CreateNodeStateAsReady());
        }

        /// <summary>
        /// Traverse puppet's behavior tree in order to request action for the puppet
        /// </summary>
        public RequestActionResult RequestAction(
            IPuppetController puppetController,
            long currentMillis)
        {
            traversalState = TraverseOneTick(puppetController, currentMillis);

            switch (traversalState.currentNodeState.lifeCycle)
            {
                case NodeLifeCycle.Start:
                    return RequestActionResult.NewAction;
                case NodeLifeCycle.Running:
                    return RequestActionResult.RunningAction;
                default:
                    return RequestActionResult.Thinking;
            }
        }

        private TraversalState TraverseOneTick(
            IPuppetController puppetController,
            long currentMillis)
        {
            var currentNodeState = traversalState.currentNodeState;
            if (currentNodeState.lifeCycle == NodeLifeCycle.Ready)
            {
                return behaviorTree.GetCurrentNode(traversalState).TraverseDown(
                    puppetId,
                    puppetController,
                    traversalState,
                    currentMillis);

            }
            else if (currentNodeState.lifeCycle.isFinished())
            {
                return behaviorTree.GetParentNode(traversalState).TraverseUp(
                    puppetId,
                    puppetController,
                    traversalState,
                    currentNodeState);

            }
            else
            {
                return behaviorTree.GetCurrentNode(traversalState).Execute(
                    puppetId,
                    puppetController,
                    traversalState,
                    currentMillis);
            }
        }
    }
}