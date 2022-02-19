namespace Viento.PuppetTheater.Node
{
    /// <summary>
    /// A enum for current [Puppet] instance's action life-cycle
    /// </summary>
    public enum ActionLifeCycle
    {
        Running,
        Success,
        Failed
    }

    /// <summary>
    /// A extensions for [ActionLifeCycle]
    /// </summary>
    public static class ActionLifeCycleExtensions
    {
        public static NodeLifeCycle toNodeLifeCycle(this ActionLifeCycle lifeCycle) =>
            lifeCycle switch
            {
                ActionLifeCycle.Running => NodeLifeCycle.Running,
                ActionLifeCycle.Success => NodeLifeCycle.Success,
                ActionLifeCycle.Failed => NodeLifeCycle.Failed,
                _ => throw new System.InvalidOperationException("Invalid action type!")
            };
    }
}