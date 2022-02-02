using Viento.PuppetTheater.Puppet;

namespace Viento.PuppetTheater.Node
{
    /// <summary>
    /// A [BehaviorNode] which has not any child node.
    /// This node is commonly used for operating something and returning result
    /// </summary>
    public abstract class LeafNode : BehaviorNode
    {
        internal LeafNode(string nodeId) : base(nodeId)
        {
        }

        public override TraversalState TraverseUp(
            string puppetId,
            IPuppetController puppetController,
            TraversalState traversalState,
            NodeState childNodeState) => throw new System.NotImplementedException(
                "Library error: Leaf node couldn't be traversed to upper");
    }
}
