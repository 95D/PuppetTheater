using Viento.PuppetTheater.Base;

namespace Viento.PuppetTheater.Node
{
    /// <summary>
    /// This class is BehaviorNode for processing child and always return false.
    /// </summary>
    public class ForceFailureNode : BehaviorNode
    {
        public readonly IBehaviorNode child;

        public ForceFailureNode(
            string name,
            IBehaviorNode child
            ) : base(name)
        {
            this.child = child;
        }

        protected override bool OnExecute(BehaviorContext context)
        {
            child.Execute(context);
            return false;
        }
    }
}
