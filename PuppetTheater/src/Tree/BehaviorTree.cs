using Viento.PuppetTheater.Node;
using System.Collections.Generic;

namespace Viento.PuppetTheater.Tree
{
    /// <summary>
    /// A single model class for holding behavior trees
    /// </summary>
    public class BehaviorTree
    {
        private readonly IReadOnlyDictionary<string, BehaviorNode> nodeDictionary;

        public BehaviorTree(IReadOnlyDictionary<string, BehaviorNode> nodeDictionary)
        {
            this.nodeDictionary = nodeDictionary;
        }

        public BehaviorNode GetBehaviorNode(string nodeId) => nodeDictionary[nodeId];
        public BehaviorNode GetCurrentNode(TraversalState state) =>
            nodeDictionary[state.currentNodeState.nodeId];
        public BehaviorNode GetParentNode(TraversalState state) =>
            nodeDictionary[state.PopNode().currentNodeState.nodeId];
    }
}