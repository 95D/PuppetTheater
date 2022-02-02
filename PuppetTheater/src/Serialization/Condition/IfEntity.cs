using Viento.PuppetTheater.Utility;

namespace Viento.PuppetTheater.Serialization
{
    /// <summary>
    /// This class data entity class for IfNode
    /// </summary>
    public class IfEntity : DataClass<IfEntity>
    {
        public readonly string name;
        public readonly string assertionId;
        public readonly string thenChild;

        public IfEntity(
            string name,
            string assertionId,
            string thenChild
            )
        {
            this.name = name;
            this.assertionId = assertionId;
            this.thenChild = thenChild;
        }
    }
}
