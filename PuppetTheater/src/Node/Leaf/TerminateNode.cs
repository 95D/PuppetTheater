using Viento.PuppetTheater.Puppet;

namespace Viento.PuppetTheater.Node
{
    /// <summary>
    /// A [LeafNode] for resetting traversal state 
    /// </summary>
    public sealed class TerminateNode : LeafNode
    {
        public TerminateNode(string nodeId) : base(nodeId)
        {
        }

        public override TraversalState TraverseDown(
            string puppetId,
            IPuppetController puppetController,
            TraversalState traversalState,
            long currentMillis)
            => traversalState.Reset();

        protected override NodeLifeCycle ExecuteInternal(
            string puppetId,
            IPuppetController puppetController,
            TraversalState traversalState,
            long currentMillis) => throw new System.NotImplementedException(
                "Library error: Terminate node couldn't be executed");
    }
}
