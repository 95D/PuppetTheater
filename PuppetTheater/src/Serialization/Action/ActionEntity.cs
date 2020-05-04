using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehaviorTree.Serialization.Action
{
    /// <summary>
    /// This class data entity class for ActionNode.
    /// </summary>
    public class ActionEntity
    {
        public readonly string name;
        public readonly string eventId;

        public ActionEntity(string name, string eventId)
        {
            this.name = name;
            this.eventId = eventId;
        }
    }
}
