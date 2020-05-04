using Viento.PuppetTheater.API;
using Viento.PuppetTheater.Base;

namespace Viento.PuppetTheater.Node
{
    /// <summary>
    /// This class is BehaviorNode for processing child then according to condition.
    /// </summary>
    public class IfNode : ConditionBaseNode
    {

        public readonly IBehaviorNode thenChild;

        public IfNode(
            string name,
            OnCheckCondition onCheckCondition,
            IBehaviorNode thenChild
            ) : base(name, onCheckCondition)
        {
            this.thenChild = thenChild;
        }

        protected override bool OnExecute(BehaviorContext context)
        {
            if (inference(context))
                return thenChild.Execute(context);
            else
                return false;
        }
    }
}
