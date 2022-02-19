namespace Viento.PuppetTheater.Node {
    /// <summary>
    /// A [NodeState] has timeout limitation
    /// </summary>
    public class TimeoutNodeState : NodeState
    {
        public static readonly long MILLIS_NOT_STARTED = -1;
        public readonly long startMillis;
        public readonly bool isStarting;

        public TimeoutNodeState(
            string nodeId,
            NodeLifeCycle lifeCycle
        ) : base(nodeId, lifeCycle)
        {
            this.startMillis = MILLIS_NOT_STARTED;
            this.isStarting = false;
        }

        private TimeoutNodeState(
            long startMillis, 
            string nodeId, 
            NodeLifeCycle lifeCycle,
            bool isStarting = false) : base(nodeId, lifeCycle)
        {
            this.startMillis = startMillis;
        }

        public override NodeState UpdateCurrentLifeCycle(NodeLifeCycle nextLifeCycle) =>
            new TimeoutNodeState(startMillis, nodeId, nextLifeCycle);  

        public TimeoutNodeState Start(long currentMillis, NodeLifeCycle lifeCycle) =>
            new TimeoutNodeState(currentMillis, nodeId, lifeCycle, isStarting: true);
        
        public bool isTimeout(long currentMillis, long duration) =>
            startMillis == MILLIS_NOT_STARTED ||
            currentMillis - startMillis >= duration;
    }
}