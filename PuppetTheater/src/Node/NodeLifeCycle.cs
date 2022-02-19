namespace Viento.PuppetTheater.Node {
    public enum NodeLifeCycle {
        Ready,
        Running,
        Success,
        Failed
    }

    public static class NodeLifeCycleExtensions {
        public static bool isFinished(this NodeLifeCycle lifeCycle) =>
            lifeCycle > NodeLifeCycle.Running;

        public static bool isSucceeded(this NodeLifeCycle lifeCycle) =>
            lifeCycle == NodeLifeCycle.Success;
    }
}