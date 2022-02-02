namespace Viento.PuppetTheater.Node
{
    /// <summary>
    /// A basic [NodeState]
    /// </summary>
    public sealed class BasicNodeState : NodeState
    {
        public BasicNodeState(string nodeId, NodeLifeCycle lifeCycle) : base(nodeId, lifeCycle)
        {
        }

        public override NodeState UpdateCurrentLifeCycle(NodeLifeCycle nextLifeCycle) =>
            new BasicNodeState(nodeId, nextLifeCycle);
    }
}