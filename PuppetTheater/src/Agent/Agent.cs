using System.Collections.Generic;
using System.Collections.Immutable;

namespace Viento.PuppetTheater.Agent
{
    /// <summary>
    /// This class is used for storing data about agent who is processed by behavior tree.
    /// </summary>
    public class Agent
    {
        public readonly int agentId;
        public readonly Queue<AgentAction> actionQueue;
        public ImmutableList<int> currentTargets;
    }
}
