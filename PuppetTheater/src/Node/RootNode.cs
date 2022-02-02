using Viento.PuppetTheater.Puppet;

namespace Viento.PuppetTheater.Node
{
    /// <summary>
    /// A [BehaviorNode] which is located in Root for each behavior tree.
    /// Triggers the starting point of a new tree
    /// </summary>
    public class RootNode : BehaviorNode
    {
        private readonly BehaviorNode child;
        public RootNode(string nodeId, BehaviorNode child) : base(nodeId)
        { }

        public override TraversalState TraverseDown(
            string puppetId,
            IPuppetController puppetController,
            TraversalState traversalState,
            long currentMillis) =>
            traversalState.PushNode(child.CreateNodeStateAsReady());

        public override TraversalState TraverseUp(
            string puppetId,
            IPuppetController puppetController,
            TraversalState traversalState,
            NodeState childNodeState) =>
            traversalState.PushNode(child.CreateNodeStateAsReady());

        protected override NodeLifeCycle ExecuteInternal(
            string puppetId,
            IPuppetController puppetController,
            TraversalState traversalState,
            long currentMillis)
        {
            throw new System.NotImplementedException(
                "Library error: Root node couldn't be executed");
        }
    }
}