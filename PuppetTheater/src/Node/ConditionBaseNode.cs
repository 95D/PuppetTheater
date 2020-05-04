using Viento.PuppetTheater.API;

namespace Viento.PuppetTheater.Node
{
    public abstract class ConditionBaseNode : BehaviorNode
    {
        public readonly OnCheckCondition inference;
        
        internal ConditionBaseNode(
            string name,
            OnCheckCondition onConditionCheck
            ) : base(name)
        {
            this.inference = onConditionCheck;
        }
    }
}
