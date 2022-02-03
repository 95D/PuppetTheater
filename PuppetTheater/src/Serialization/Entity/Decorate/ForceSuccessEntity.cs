namespace Viento.PuppetTheater.Serialization
{
    /// <summary>
    /// This class data entity class for [ForceSuccessNode]
    /// </summary>
    public class ForceSuccessEntity : INodeEntity
    {
        public const string TYPE = "force-success";
        public INodeEntity child;
        public string nodeId { get; set; }
        public string type { get; set; }
    }
}
