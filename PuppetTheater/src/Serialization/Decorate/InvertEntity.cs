namespace Viento.PuppetTheater.Serialization
{
    /// <summary>
    /// This class data entity class for InvertNode.
    /// </summary>
    public class InvertEntity
    {
        public readonly string name;
        public readonly string child;

        public InvertEntity(string name, string child)
        {
            this.name = name;
            this.child = child;
        }
    }
}
