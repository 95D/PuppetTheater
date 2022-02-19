using Viento.PuppetTheater.Exception;
using Viento.PuppetTheater.Puppet;

namespace Viento.PuppetTheater.Node
{
    /// <summary>
    /// A [LeafNode] for throwing cancellation exception
    /// </summary>
    public sealed class CancelNode : LeafNode
    {
        public CancelNode(string nodeId) : base(nodeId)
        {
        }

        public override TraversalState TraverseDown(
            string puppetId,
            IPuppetController puppetController,
            TraversalState traversalState,
            long currentMillis)
            => throw new CancelBehaviorException(puppetId, traversalState, CancelBehaviorException.Cause.Command);

        protected override NodeLifeCycle ExecuteInternal(
            string puppetId,
            IPuppetController puppetController,
            TraversalState traversalState,
            long currentMillis) => throw new System.NotImplementedException(
                "Library error: Terminate node couldn't be executed");
    }
}
