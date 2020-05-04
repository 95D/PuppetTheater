using Viento.PuppetTheater.API;
using Viento.PuppetTheater.Base;

namespace Viento.PuppetTheater.Node
{
    /// <summary>
    /// This class is BehaviorNode for processing child until condition is false.
    /// </summary>
    public class UntilNode : ConditionBaseNode
    {
        public readonly IBehaviorNode untilChild;

        public UntilNode(
            string name,
            OnCheckCondition onCheckCondition,
            IBehaviorNode thenChild
            ) : base(name, onCheckCondition)
        {
            this.untilChild = thenChild;
        }

        protected override bool OnExecute(BehaviorContext context)
        {
            bool isSuccess = false;
            while(inference(context))
            {
                untilChild.Execute(context);
                isSuccess = true;
            }
            return isSuccess;
        }
    }
}
