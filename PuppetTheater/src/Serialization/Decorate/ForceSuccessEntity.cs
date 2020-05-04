namespace Viento.PuppetTheater.Serialization
{
    /// <summary>
    /// This class data entity class for ForceSuccessNode.
    /// </summary>
    public class ForceSuccessEntity
    {
        public readonly string name;
        public readonly string child;

        public ForceSuccessEntity(string name, string child)
        {
            this.name = name;
            this.child = child;
        }
    }
}
