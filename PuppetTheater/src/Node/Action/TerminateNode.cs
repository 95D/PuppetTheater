using Viento.PuppetTheater.Base;

namespace Viento.PuppetTheater.Node
{
    /// <summary>
    /// This class is BehaviorNode which do nothing and return true.
    /// </summary>
    public class TerminateNode : BehaviorNode
    {
        public const string TERMINATE = "!!!TERMINATE!!!";
        public TerminateNode() : base(TERMINATE)
        {
        }

        protected override bool OnExecute(BehaviorContext context)
        {
            return true;
        }
    }
}
