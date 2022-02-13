using System;

namespace Viento.PuppetTheater.Node
{
    /// <summary>
    /// A [CompositeNodeState] which is iterated by picking up from random range.
    /// The random-order is endless running until arrive in break
    /// </summary>
    public class RandomPickLoopNodeState : CompositeNodeState
    {
        public readonly int size;

        private readonly int index;
        public override int GetIndex() => index;

        private readonly Random random = new Random();

        private int next { get => random.Next(0, size); }

        public RandomPickLoopNodeState(
            int size, 
            string nodeId, 
            NodeLifeCycle lifeCycle) : base(nodeId, lifeCycle)
        {
            this.size = size;
            index = next;
        }

        private RandomPickLoopNodeState(
            string nodeId,
            NodeLifeCycle lifeCycle,
            int size,
            int index,
            Random random) : base(nodeId, lifeCycle)
        {
            this.size = size;
            this.index = index;
            this.random = random;
        }

        public override NodeState UpdateCurrentLifeCycle(NodeLifeCycle nextLifeCycle) =>
            new RandomPickLoopNodeState(nodeId, nextLifeCycle, size, index, random);

        public override bool isIterationFinished() => false;

        public override CompositeNodeState ToNextIndex()
        {
            if (isIterationFinished())
            {
                return this;
            }
            else
            {
                return new RandomPickLoopNodeState(nodeId, lifeCycle, size, next, random);
            }
        }

    }
}