using Viento.PuppetTheater.Base;

namespace Viento.PuppetTheater.Node
{
    /// <summary>
    /// This class is BehaviorNode for processing child and do invert return value
    /// </summary>
    public class InvertNode : BehaviorNode
    {
        public readonly IBehaviorNode child;

        public InvertNode(
            string name,
            IBehaviorNode child
            ) : base(name)
        {
            this.child = child;
        }

        protected override bool OnExecute(BehaviorContext context)
        {
            return !child.Execute(context);
        }
    }
}
