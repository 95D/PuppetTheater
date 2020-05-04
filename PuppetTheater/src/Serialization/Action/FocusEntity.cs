using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehaviorTree.Serialization.Action
{
    /// <summary>
    /// This class data entity class for FocusNode.
    /// </summary>
    public class FocusEntity
    {
        public readonly string name;
        public readonly string filterId;
        public readonly string priorityId;
        public readonly int count = 1;

        public FocusEntity(
            string name,
            string filterId,
            string priorityId,
            int count
            )
        {
            this.name = name;
            this.filterId = filterId;
            this.priorityId = priorityId;
            this.count = count;
        }
    }
}
