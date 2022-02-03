namespace Viento.PuppetTheater.Serialization
{
    /// <summary>
    /// This class data entity class for [ForceFailureNode]
    /// </summary>
    public class ForceFailureEntity : INodeEntity
    {
        public const string TYPE = "force-failure";
        public INodeEntity child;
        public string nodeId { get; set; }
        public string type { get; set; }
    }
}
