﻿using System.Collections.Generic;
using Viento.PuppetTheater.Puppet;

namespace Viento.PuppetTheater.Node
{
    /// <summary>
    /// A [BehaviorNode] which has children nodes
    /// </summary>
    public class CompositeNode : BehaviorNode
    {
        public readonly PermutateCategory permutateType;
        public readonly CompositeCategory category;
        public readonly List<BehaviorNode> children;

        public CompositeNode(
            string nodeId,
            List<BehaviorNode> children,
            PermutateCategory permuateType,
            CompositeCategory category) : base(nodeId)
        {
            this.children = children;
            this.permutateType = permuateType;
            this.category = category;
        }

        public override NodeState CreateNodeStateAsReady()
        {
            int size = children.Count;
            NodeLifeCycle ready = NodeLifeCycle.Ready;
            return permutateType switch {
                PermutateCategory.Ascending => new AscentLoopNodeState(size, nodeId, ready),
                PermutateCategory.OrderedRandom => 
                    new RandomOrderLoopNodeState(size, nodeId, ready),
                PermutateCategory.InfiniteRandom =>
                    new RandomPickLoopNodeState(size, nodeId, ready),
                _ => throw new System.NotImplementedException(
                        string.Format("Couldn't find composite node state for [0]", permutateType))
            };
        }

        public override TraversalState TraverseUp(
            string puppetId,
            IPuppetController puppetController,
            TraversalState traversalState,
            NodeState childNodeState)
        {
            var currentNodeState =
                (traversalState.currentNodeState as CompositeNodeState).ToNextIndex();
            if (childNodeState.lifeCycle == category.breakState)
            {
                return traversalState.UpdateCurrentNode(
                    currentNodeState.UpdateCurrentLifeCycle(category.breakState));
            }
            else if (currentNodeState.isIterationFinished())
            {
                return traversalState.UpdateCurrentNode(
                    currentNodeState.UpdateCurrentLifeCycle(category.finallyState));
            }
            else
            {
                return traversalState.UpdateCurrentNode(currentNodeState).PushNode(
                    children[currentNodeState.GetIndex()].CreateNodeStateAsReady()
                );
            }
        }

        public override TraversalState TraverseDown(
            string puppetId,
            IPuppetController puppetController,
            TraversalState traversalState,
            long currentMillis)
        {
            var currentNodeState = traversalState.currentNodeState as CompositeNodeState;
            return traversalState
                .UpdateCurrentNodeLifeCycle(NodeLifeCycle.Running)
                .PushNode(children[currentNodeState.GetIndex()].CreateNodeStateAsReady());
        }

        protected override NodeLifeCycle ExecuteInternal(
            string puppetId,
            IPuppetController puppetController,
            TraversalState traversalState,
            long currentMillis) => NodeLifeCycle.Running;
    }
}
