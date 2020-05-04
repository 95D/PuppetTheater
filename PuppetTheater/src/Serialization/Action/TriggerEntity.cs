namespace Viento.PuppetTheater.Serialization
{
    /// <summary>
    /// This class data entity class for TriggerNode.
    /// </summary>
    public class TriggerEntity
    {
        public readonly string name;
        public readonly string eventId;
        public readonly string agentId;

        public TriggerEntity(
            string name,
            string eventId,
            string agentId
            )
        {
            this.name = name;
            this.eventId = eventId;
            this.agentId = agentId;
        }
    }
}
