namespace Viento.PuppetTheater.Node
{
    /// <summary>
    /// A state model class for managing run-time state of [BehaviorNode]
    /// - This class is immutable
    /// </summary>
    public abstract class NodeState
    {
        public readonly string nodeId;
        public readonly NodeLifeCycle lifeCycle;

        public NodeState(string nodeId, NodeLifeCycle lifeCycle)
        {
            this.nodeId = nodeId;
            this.lifeCycle = lifeCycle;
        }

        public abstract NodeState UpdateCurrentLifeCycle(NodeLifeCycle nextLifeCycle);
    }
}