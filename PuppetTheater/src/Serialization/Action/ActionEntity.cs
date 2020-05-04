namespace Viento.PuppetTheater.Serialization
{
    /// <summary>
    /// This class data entity class for ActionNode.
    /// </summary>
    public class ActionEntity
    {
        public readonly string name;
        public readonly string eventId;

        public ActionEntity(string name, string eventId)
        {
            this.name = name;
            this.eventId = eventId;
        }
    }
}
