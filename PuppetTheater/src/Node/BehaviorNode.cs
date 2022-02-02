using Viento.PuppetTheater.Puppet;
namespace Viento.PuppetTheater.Node
{
    /// <summary>
    /// A Model class for constructing `Node` in `Behavior tree`
    /// - This node is stateless model class, State is holded in [NodeState]
    /// </summary>
    public abstract class BehaviorNode
    {
        public readonly string nodeId;

        public BehaviorNode(string nodeId)
        {
            this.nodeId = nodeId;
        }

        public virtual NodeState CreateNodeStateAsReady() =>
            new BasicNodeState(nodeId, NodeLifeCycle.Ready);

        public abstract TraversalState TraverseUp(
            string puppetId,
            IPuppetController puppetController,
            TraversalState traversalState,
            NodeState childNodeState
        );
        
        public abstract TraversalState TraverseDown(
            string puppetId,
            IPuppetController puppetController,
            TraversalState traversalState,
            long currentMillis
        );

        public TraversalState Execute(
            string puppetId, 
            IPuppetController puppetController, 
            TraversalState traversalState, 
            long currentMillis)
        {
            var currentStep = ExecuteInternal(puppetId, puppetController, traversalState, currentMillis);
            return traversalState.UpdateCurrentNodeLifeCycle(currentStep);        
        }

        protected abstract NodeLifeCycle ExecuteInternal(
            string puppetId, 
            IPuppetController puppetController,
            TraversalState traversalState, 
            long currentMillis);

        public override int GetHashCode()
        {
            return nodeId.GetHashCode();
        }
    }
}
