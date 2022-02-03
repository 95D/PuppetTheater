namespace Viento.PuppetTheater.Serialization
{
    /// <summary>
    /// This class data entity class for InvertNode.
    /// </summary>
    public class InvertEntity : INodeEntity
    {
        public const string TYPE = "invert";
        public INodeEntity child;
        public string nodeId { get; set; }
        public string type { get; set; }
    }
}
