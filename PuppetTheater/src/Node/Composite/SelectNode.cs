using System.Collections.Generic;
using Viento.PuppetTheater.Base;

namespace Viento.PuppetTheater.Node
{
    /// <summary>
    /// <para>This class is Behavior Node which composite children.</para> 
    /// <para>This class iterates executing child until child return true.</para>
    /// </summary>
    public class SelectNode : CompositeBaseNode
    {
        public SelectNode(
            string name,
            List<IBehaviorNode> children
            ) : base(name, children)
        {
        }

        protected override bool OnExecute(BehaviorContext context)
        {
            foreach (var child in children)
            {
                if (child.Execute(context))
                    return true;
            }
            return false;
        }
    }
}
