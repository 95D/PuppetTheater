namespace Viento.PuppetTheater.Serialization
{
    /// <summary>
    /// This class data entity class for ActionNode.
    /// </summary>
    public class ActionEntity : INodeEntity
    {
        public const string TYPE = "action";
        public string actionId;
        public long timeoutDurationMillis;
        public string nodeId { get; set; }
        public string type { get; set; }
    }
}
