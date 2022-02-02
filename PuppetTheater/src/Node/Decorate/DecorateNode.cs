using Viento.PuppetTheater.Puppet;

namespace Viento.PuppetTheater.Node
{
    /// <summary>
    /// A [BehaviorNode] which decorates traversing between `parent node` and `child node`
    /// </summary>
    public abstract class DecorateNode : BehaviorNode
    {
        public readonly BehaviorNode child;
        internal DecorateNode(string nodeId, BehaviorNode child) : base(nodeId)
        {
            this.child = child;
        }

        protected override NodeLifeCycle ExecuteInternal(
            string puppetId,
            IPuppetController puppetController,
            TraversalState traversalState,
            long currentMillis) => throw new System.NotImplementedException(
                "Library error: Decorate node couldn't be executed");
    }
}
