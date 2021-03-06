﻿using System.Collections.Generic;
using System.Collections.Immutable;

namespace Viento.PuppetTheater.Agent
{
    /// <summary>
    /// This class collects agent instances and publishes event from behavior tree.
    /// </summary>
    public class AgentChannel
    {
        private readonly Dictionary<int, Agent> agentMap = new Dictionary<int, Agent>();

        public AgentChannel() { }

        public void PutAgent(Agent agent)
        {
            agentMap.Add(agent.agentId.GetHashCode(), agent);
        }
        
        public void FocusTargets(string agentId, ImmutableList<int> targets)
        {
            agentMap[agentId.GetHashCode()].currentTargets = targets;
        }

        public void PublishAction(string agentId, string eventId)
        {
            var agent = agentMap[agentId.GetHashCode()];
            agent.actionQueue.Enqueue(new AgentAction(agent.currentTargets, eventId));
        }

        public bool HasAction(string agentId)
        {
            return agentMap[agentId.GetHashCode()].actionQueue.Count == 0;
        }

        public AgentAction SubscribeAction(string agentId)
        {
            return HasAction(agentId)
                ? agentMap[agentId.GetHashCode()].actionQueue.Dequeue()
                : null;
        }
    }
}
