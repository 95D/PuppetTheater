namespace Viento.PuppetTheater.Serialization
{
    /// <summary>
    /// This class data entity class for IterateNode.
    /// </summary>
    public class RepeatEntity : INodeEntity
    {
        public const string TYPE = "iterate";
        public int count;
        public INodeEntity child;
        public string nodeId { get; set; }
        public string type { get; set; }
    }
}
