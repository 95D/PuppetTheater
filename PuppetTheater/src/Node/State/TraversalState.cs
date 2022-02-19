using System.Collections.Immutable;
using System.Linq;

namespace Viento.PuppetTheater.Node
{
    /// <summary>
    /// A state collection class for manaing current travarsal state which is consist of [NodeState]
    /// </summary>
    public struct TraversalState
    {
        private readonly NodeState topNode;
        private readonly ImmutableList<NodeState> nodeStack;

        public bool isEnqueuedNewCommand
        {
            get => currentNodeState is TimeoutNodeState timeoutNodeState &&
                    timeoutNodeState.isStarting;
        }

        public bool isRunningCommand
        {
            get => currentNodeState is TimeoutNodeState && 
                currentNodeState.lifeCycle.IsFinished();
        }

        public NodeState currentNodeState
        {
            get
            {
                return nodeStack.Last();
            }
        }

        public TraversalState(NodeState topNode)
        {
            this.topNode = topNode;
            nodeStack = ImmutableList<NodeState>.Empty.Add(topNode);
        }

        private TraversalState(NodeState topNode, ImmutableList<NodeState> nodeStack)
        {
            this.topNode = topNode;
            this.nodeStack = nodeStack;
        }

        public TraversalState PushNode(NodeState state) => new TraversalState(
            topNode,
            nodeStack.Add(state));

        public TraversalState UpdateCurrentNodeLifeCycle(NodeLifeCycle next) =>
            new TraversalState(
                topNode,
                nodeStack.Replace(
                    currentNodeState,
                    currentNodeState.UpdateCurrentLifeCycle(next)));

        public TraversalState UpdateCurrentNode(NodeState next) =>
            new TraversalState(
                topNode,
                nodeStack.Replace(currentNodeState, next));

        public TraversalState PopNode()
        {
            var nextNodeStack = nodeStack.Remove(currentNodeState);
            if (nextNodeStack.IsEmpty)
            {
                return Reset();
            }
            else
            {
                return new TraversalState(
                    topNode,
                    nextNodeStack);
            }
        }

        public TraversalState Reset() => new TraversalState(
            topNode,
            ImmutableList<NodeState>.Empty.Add(topNode));

        public string GetStackTrace() => string.Join(
            separator: " => ",
            values: nodeStack.Select(x =>
                string.Format(
                    "[{0}|{1}, <{2}>]",
                    x.nodeId,
                    x.GetType().Name,
                    x.lifeCycle.ToString())));
    }
}