using Viento.PuppetTheater.Base;

namespace Viento.PuppetTheater.Node
{
    /// <summary>
    /// This class is BehaviorNode for publishing action for agent who runs the behavior tree.
    /// </summary>
    public class ActionNode : BehaviorNode
    {
        public readonly string eventId;
        public ActionNode(string name, string eventId) : base(name)
        {
            this.eventId = eventId;
        }

        protected override bool OnExecute(BehaviorContext context)
        {
            context.agentChannel.PublishAction(context.agentId, eventId);
            return true;
        }
    }
}
