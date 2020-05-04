using Viento.PuppetTheater.Base;

namespace Viento.PuppetTheater.Node
{
    /// <summary>
    /// This interface is for the BehaviorNode class.
    /// </summary>
    public interface IBehaviorNode
    {
        string BehaviorId { get; }
        bool Execute(BehaviorContext context);
    }
}
