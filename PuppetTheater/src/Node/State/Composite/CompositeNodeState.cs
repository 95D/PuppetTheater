namespace Viento.PuppetTheater.Node
{

    public abstract class CompositeNodeState : NodeState
    {
        public CompositeNodeState(
            string nodeId,
            NodeLifeCycle lifeCycle
        ) : base(nodeId, lifeCycle)
        {
        }

        public abstract int GetIndex();
        public abstract bool isIterationFinished();
        public abstract CompositeNodeState ToNextIndex();
    }
}