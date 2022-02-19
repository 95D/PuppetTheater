namespace Viento.PuppetTheater.Node {
    public enum NodeLifeCycle {
        Ready,
        Running,
        Success,
        Failed
    }

    public static class NodeLifeCycleExtensions {
        public static bool IsFinished(this NodeLifeCycle lifeCycle) =>
            lifeCycle > NodeLifeCycle.Running;

        public static bool IsSucceeded(this NodeLifeCycle lifeCycle) =>
            lifeCycle == NodeLifeCycle.Success;
    }
}