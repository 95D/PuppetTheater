namespace Viento.PuppetTheater.Node
{
    /// <summary>
    /// A [CompositeNodeState] which is iterated by ascending-order
    /// </summary>
    public sealed class AscentLoopNodeState : CompositeNodeState
    {
        public readonly int size;
        private readonly int index;
        public override int GetIndex() => index;

        public AscentLoopNodeState(
            int size,
            string nodeId,
            NodeLifeCycle lifeCycle) : base(nodeId, lifeCycle)
        {
            this.size = size;
            index = 0;
        }

        private AscentLoopNodeState(
            string nodeId,
            NodeLifeCycle lifeCycle,
            int size,
            int index) : base(nodeId, lifeCycle)
        {
            this.index = index;
            this.size = size;
        }

        public override NodeState UpdateCurrentLifeCycle(NodeLifeCycle nextLifeCycle) =>
            new AscentLoopNodeState(nodeId, nextLifeCycle, size, index);

        public override bool isIterationFinished() => index == size;

        public override CompositeNodeState ToNextIndex()
        {
            if (isIterationFinished())
                return this;
            else
                return new AscentLoopNodeState(nodeId, lifeCycle, size, index + 1);
        }

    }
}