namespace Viento.PuppetTheater.Node
{
    /// <summary>
    /// A enum for current [Puppet] instance's action life-cycle
    /// </summary>
    public enum ActionLifeCycle
    {
        Start,
        Running,
        Success,
        Failed
    }

    /// <summary>
    /// A extensions for [ActionLifeCycle]
    /// </summary>
    public static class ActionLifeCycleExtensions
    {
        public static NodeLifeCycle toNodeLifeCycle(this ActionLifeCycle lifeCycle)
        {
            switch (lifeCycle)
            {
                case ActionLifeCycle.Start:
                    return NodeLifeCycle.Start;
                case ActionLifeCycle.Running:
                    return NodeLifeCycle.Running;
                case ActionLifeCycle.Success:
                    return NodeLifeCycle.Success;
                case ActionLifeCycle.Failed:
                    return NodeLifeCycle.Failed;
            }
            throw new System.InvalidOperationException("Invalid action type!");
        }
    }
}