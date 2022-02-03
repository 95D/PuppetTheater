namespace Viento.PuppetTheater.Serialization
{
    /// <summary>
    /// This class data entity class for StochasticNode.
    /// </summary>
    public class StochasticEntity : INodeEntity
    {
        public const string TYPE = "stochastic";
        public float threshold;
        public INodeEntity child;
        public string nodeId { get; set; }
        public string type { get; set; }
    }
}
