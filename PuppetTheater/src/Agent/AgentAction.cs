using System.Collections.Immutable;

namespace Viento.PuppetTheater.Agent
{
    public class AgentAction
    {
        public readonly ImmutableList<int> targets;
        public readonly string eventId;

        public AgentAction(ImmutableList<int> targets, string eventId)
        {
            this.targets = targets;
            this.eventId = eventId;
        }
    }
}
