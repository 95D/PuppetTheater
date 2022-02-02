using Viento.PuppetTheater.Utility;

namespace Viento.PuppetTheater.Serialization
{
    /// <summary>
    /// This class data entity class for ActionNode.
    /// </summary>
    public class ActionEntity : DataClass<ActionEntity>
    {
        public readonly string name;
        public readonly string actionId;
        public readonly long timeoutDurationMillis;

        public ActionEntity(string name, string actionId, long timeoutDurationMillis)
        {
            this.name = name;
            this.actionId = actionId;
            this.timeoutDurationMillis = timeoutDurationMillis;
        }
    }
}
