namespace Viento.PuppetTheater.Serialization
{
    /// <summary>
    /// This class data entity class for StochasticNode.
    /// </summary>
    public class StochasticEntity
    {
        public readonly string name;
        public readonly float threshold;
        public readonly string child;

        public StochasticEntity(string name, float threshold, string child)
        {
            this.name = name;
            this.threshold = threshold;
            this.child = child;
        }
    }
}
