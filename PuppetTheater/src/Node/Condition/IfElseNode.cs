using Viento.PuppetTheater.API;
using Viento.PuppetTheater.Base;

namespace Viento.PuppetTheater.Node
{
    /// <summary>
    /// This class is BehaviorNode for processing child then or else according to condition.
    /// </summary>
    public class IfElseNode : ConditionBaseNode
    {
        public readonly IBehaviorNode thenChild;
        public readonly IBehaviorNode elseChild;

        public IfElseNode(
            string name,
            OnCheckCondition onCheckCondition,
            IBehaviorNode thenChild, 
            IBehaviorNode elseChild
            ) : base(name, onCheckCondition)
        {
            this.thenChild = thenChild;
            this.elseChild = elseChild;
        }

        protected override bool OnExecute(BehaviorContext context)
        {
            if (inference(context))
                return thenChild.Execute(context);
            else
                return elseChild.Execute(context);

        }
    }
}
