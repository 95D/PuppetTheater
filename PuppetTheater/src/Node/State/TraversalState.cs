using System.Collections.Immutable;
using System.Linq;

namespace Viento.PuppetTheater.Node
{
    /// <summary>
    /// A state collection class for manaing current travarsal state which is consist of [NodeState]
    /// </summary>
    public struct TraversalState
    {
        private readonly ImmutableList<NodeState> nodeStack;

        public NodeState currentNodeState { get => nodeStack.Last(); }

        public TraversalState(NodeState topNode)
        {
            nodeStack = ImmutableList<NodeState>.Empty.Add(topNode);
        }

        private TraversalState(ImmutableList<NodeState> nodeStack)
        {
            this.nodeStack = nodeStack;
        }

        public TraversalState PushNode(NodeState state) => new TraversalState(state);

        public TraversalState UpdateCurrentNodeLifeCycle(NodeLifeCycle next) =>
            new TraversalState(
                nodeStack.Replace(
                    currentNodeState,
                    currentNodeState.UpdateCurrentLifeCycle(next)));

        public TraversalState UpdateCurrentNode(NodeState next) =>
            new TraversalState(
                nodeStack.Replace(currentNodeState, next));

        public TraversalState PopNode() => new TraversalState(nodeStack.Remove(currentNodeState));

        public TraversalState Reset() => new TraversalState(
            ImmutableList<NodeState>.Empty.Add(nodeStack[0]));

        public string GetStackTrace() => string.Join(
            separator: " => ",
            values: nodeStack.Select(it =>
            string.Format("[{0}, <{1}>]", it.nodeId, it.lifeCycle.ToString())));
    }
}