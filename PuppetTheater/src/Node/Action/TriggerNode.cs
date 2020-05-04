using System;
using System.Collections.Generic;
using Viento.PuppetTheater.Base;

namespace Viento.PuppetTheater.Node
{
    /// <summary>
    /// This class is BehaviorNode for publishing action for specific agent.
    /// </summary>
    public class TriggerNode : BehaviorNode
    {
        public readonly string agentId;
        public readonly string eventId;
        public TriggerNode(
            string name, 
            string agentId, 
            string eventId
            ) : base(name)
        {
            this.agentId = agentId;
            this.eventId = eventId;
        }

        protected override bool OnExecute(BehaviorContext context)
        {
            context.agentChannel.PublishAction(agentId, eventId);
            return true;
        }
    }
}
