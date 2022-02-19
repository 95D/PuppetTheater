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
        public TraversalState traversalState { get; private set; }
        public readonly string puppetId;

        public Puppet(BehaviorTree tree, string puppetId, string rootId)
        {
            this.behaviorTree = tree;
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
            
            if(traversalState.isEnqueuedNewCommand) return RequestActionResult.NewAction;
            if(traversalState.isRunningCommand) return RequestActionResult.RunningAction;
            return RequestActionResult.Thinking;
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
            else if (currentNodeState.lifeCycle.IsFinished())
            {
                var childNodeState = traversalState.currentNodeState;
                var parentTraversalState = traversalState.PopNode();
                return behaviorTree.GetCurrentNode(parentTraversalState).TraverseUp(
                    puppetId,
                    puppetController,
                    parentTraversalState,
                    childNodeState);
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