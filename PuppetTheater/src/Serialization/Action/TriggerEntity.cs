using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehaviorTree.Serialization.Action
{
    /// <summary>
    /// This class data entity class for TriggerNode.
    /// </summary>
    public class TriggerEntity
    {
        public readonly string name;
        public readonly string eventId;
        public readonly string agentId;

        public TriggerEntity(
            string name,
            string eventId,
            string agentId
            )
        {
            this.name = name;
            this.eventId = eventId;
            this.agentId = agentId;
        }
    }
}
