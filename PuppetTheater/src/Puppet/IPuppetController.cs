using Viento.PuppetTheater.Node;
namespace Viento.PuppetTheater.Puppet
{
    /// <summary>
    /// A API for implementation bridge between `Behavior tree` and `Game data`
    /// </summary>
    public interface IPuppetController
    {
        bool Assert(string puppetId, string assertionId);
        ActionLifeCycle RequestAction(string puppetId, string actionId);
        ActionLifeCycle GetCurrentActionState(string puppetId, string actionId);
        void CancelAction(string puppetId, string actionId);
    }
}