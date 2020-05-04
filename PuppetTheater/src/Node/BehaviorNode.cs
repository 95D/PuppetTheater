using Viento.PuppetTheater.Base;

namespace Viento.PuppetTheater.Node
{
    /// <summary>
    /// This class is a unit of the Behavior tree.
    /// </summary>
    public abstract class BehaviorNode : IBehaviorNode
    {
        private readonly string _behaviorId;
        public virtual string BehaviorId { get => _behaviorId ; }
        public BehaviorNode(string behaviorId)
        {
            this._behaviorId = behaviorId;
        }

        public bool Execute(BehaviorContext context)
        {
            context.OnExecuteStart(BehaviorId);
            bool result = OnExecute(context);
            context.OnExecuteEnd();
            return result;
        }

        protected abstract bool OnExecute(BehaviorContext context);

        public override int GetHashCode()
        {
            return BehaviorId.GetHashCode();
        }
    }
}
