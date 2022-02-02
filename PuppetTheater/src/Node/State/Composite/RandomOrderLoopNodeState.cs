using System;
using System.Collections.Generic;
using Viento.PuppetTheater.Utility;

namespace Viento.PuppetTheater.Node
{
    /// <summary>
    /// A [CompositeNodeState] which is iterated by determined-random-order.
    /// The random-order is determined at construct this state
    /// </summary>
    public class RandomOrderLoopNodeState : CompositeNodeState
    {
        public readonly IReadOnlyList<int> permutation;
        private readonly int _index;
        public override int index { get; }

        public RandomOrderLoopNodeState(
            int size, 
            string nodeId, 
            NodeLifeCycle lifeCycle) : base(nodeId, lifeCycle)
        {
            index = 0;
            permutation = new RandomPermutation(new Random()).Next(0, size);
        }

        private RandomOrderLoopNodeState(
            string nodeId,
            NodeLifeCycle lifeCycle,
            int index, IReadOnlyList<int> permutation) : base(nodeId, lifeCycle)
        {
            this.index = index;
            this.permutation = permutation;
        }

        public override NodeState UpdateCurrentLifeCycle(NodeLifeCycle nextLifeCycle) =>
            new RandomOrderLoopNodeState(nodeId, nextLifeCycle, index, permutation);

        public override bool isIterationFinished() => index == permutation.Count;

        public override CompositeNodeState ToNextIndex()
        {
            if (isIterationFinished())
            {
                return this;
            }
            else
            {
                return new RandomOrderLoopNodeState(nodeId, lifeCycle, index + 1, permutation);
            }
        }

    }
}