using Viento.PuppetTheater.Base;
using Viento.PuppetTheater.Exception;

namespace Viento.PuppetTheater.Node
{
    /// <summary>
    /// This class is BehaviorNode for canceling current behavior tree process.
    /// </summary>
    class CancelNode : BehaviorNode
    {
        public const string TERMINATE = "!!!CANCEL!!!";
        public CancelNode() : base(TERMINATE)
        {
        }

        protected override bool OnExecute(BehaviorContext context)
        {
            throw new CancelBehaviorException(context, CancelBehaviorException.Cause.Command);
        }
    }
}
