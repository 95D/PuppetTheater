using System.Collections.Generic;
using System.Collections.Immutable;

namespace Viento.PuppetTheater.Agent
{
    public class Agent
    {
        public readonly int agentId;
        readonly Queue<AgentAction> actionQueue;

        public ImmutableList<int> currentTargets;

        public void Publish(string eventId)
        {
            actionQueue.Enqueue(new AgentAction(currentTargets, eventId));
        }
    }
}
