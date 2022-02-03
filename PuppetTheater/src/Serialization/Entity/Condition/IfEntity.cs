namespace Viento.PuppetTheater.Serialization
{
    /// <summary>
    /// This class data entity class for IfNode
    /// </summary>
    public class IfEntity : INodeEntity
    {
        public const string TYPE = "if";
        public string assertionId;
        public INodeEntity thenChild;
        public string nodeId { get; set; }
        public string type { get; set; }
    }
}
