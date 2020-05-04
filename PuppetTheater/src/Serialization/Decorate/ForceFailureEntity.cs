namespace Viento.PuppetTheater.Serialization
{
    /// <summary>
    /// This class data entity class for ForceFailureNode.
    /// </summary>
    public class ForceFailureEntity
    {
        public readonly string name;
        public readonly string child;

        public ForceFailureEntity(string name, string child)
        {
            this.name = name;
            this.child = child;
        }
    }
}
