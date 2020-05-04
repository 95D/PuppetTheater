using Viento.PuppetTheater.API;
using Viento.PuppetTheater.Base;

namespace Viento.PuppetTheater.Node
{
    /// <summary>
    /// This class is BehaviorNode for changing agent's focus.
    /// </summary>
    public class FocusNode : BehaviorNode
    {
        public readonly OnFocus focusOn;
        public readonly int count;

        public FocusNode(
            string name,
            OnFocus onFocus,
            int count
            ) : base(name)
        {
            this.focusOn = onFocus;
            this.count = count;
        }

        protected override bool OnExecute(BehaviorContext context)
        {
            var immutableList = focusOn(count);
            context.agentChannel.FocusTargets(context.agentId, immutableList);
            return true;
        }
    }
}
