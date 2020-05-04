using System.Collections.Generic;

namespace Viento.PuppetTheater.Node
{
    public abstract class CompositeBaseNode : BehaviorNode
    {
        public readonly List<IBehaviorNode> children;

        public CompositeBaseNode(
            string name,
            List<IBehaviorNode> children
            ) : base(name)
        {
            this.children = children;
        }
    }
}
