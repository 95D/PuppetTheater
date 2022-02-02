using Viento.PuppetTheater.Puppet;

namespace Viento.PuppetTheater.Node
{
    /// <summary>
    /// A [BehaviorNode] which has children nodes operated by conditional branch
    /// </summary>
    public abstract class ConditionNode : BehaviorNode
    {
        public readonly string assertionId;

        internal ConditionNode(
            string nodeId,
            string assertionId) : base(nodeId)
        {
            this.assertionId = assertionId;
        }

        protected override NodeLifeCycle ExecuteInternal(
            string puppetId,
            IPuppetController puppetController,
            TraversalState traversalState,
            long currentMillis)
        {
            throw new System.NotImplementedException("Library error: Condition node couldn't be executed");
        }

        public override TraversalState TraverseUp(
            string puppetId,
            IPuppetController puppetController,
            TraversalState traversalState,
            NodeState childNodeState)
        {
            if (childNodeState.lifeCycle.isSucceeded())
                return traversalState.UpdateCurrentNodeLifeCycle(NodeLifeCycle.Success);
            else
                return traversalState.UpdateCurrentNodeLifeCycle(NodeLifeCycle.Failed);
        }
    }
}
