using Viento.PuppetTheater.Utility;

namespace Viento.PuppetTheater.Serialization
{
    /// <summary>
    /// This class data entity class for IterateNode.
    /// </summary>
    public class IterateEntity : DataClass<IterateEntity>
    {
        public readonly string name;
        public readonly int range;
        public readonly string child;

        public IterateEntity(string name, int range, string child)
        {
            this.name = name;
            this.range = range;
            this.child = child;
        }
    }
}
