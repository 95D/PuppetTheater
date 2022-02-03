namespace Viento.PuppetTheater.Serialization
{
    /// <summary>
    /// This class data entity class for UntilNode.
    /// </summary>
    public class UntilEntity : INodeEntity
    {
        public const string TYPE = "until";
        public string assertionId;
        public INodeEntity thenChild;
        public string nodeId { get; set; }
        public string type { get; set; }
    }
}
