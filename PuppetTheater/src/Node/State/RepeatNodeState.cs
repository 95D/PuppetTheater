namespace Viento.PuppetTheater.Node
{
    /// <summary>
    /// A [NodeState] which iterate [Count]
    /// </summary>
    public sealed class RepeatNodeState : NodeState
    {
        public readonly int count;
        public readonly int currentAttemptCount;
        public RepeatNodeState(
            int count, 
            string nodeId, 
            NodeLifeCycle lifeCycle) : base(nodeId, lifeCycle)
        {
            this.count = count;
            this.currentAttemptCount = 0;
        }

        private RepeatNodeState(
            int count, 
            int currentAttemptCount,
            string nodeId, 
            NodeLifeCycle lifeCycle) : base(nodeId, lifeCycle)
        {
            this.count = count;
            this.currentAttemptCount = currentAttemptCount;
        }

        public override NodeState UpdateCurrentLifeCycle(NodeLifeCycle nextLifeCycle) =>
            new RepeatNodeState(count, currentAttemptCount, nodeId, nextLifeCycle);

        public RepeatNodeState ToNextCount() =>
            new RepeatNodeState(count, currentAttemptCount + 1, nodeId, lifeCycle);

        public bool isIterationFinished() => currentAttemptCount >= count;
    }
}