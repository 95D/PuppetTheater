using Viento.PuppetTheater.Utility;

namespace Viento.PuppetTheater.Serialization
{
    /// <summary>
    /// This class data entity class for UntilNode.
    /// </summary>
    public class UntilEntity : DataClass<UntilEntity>
    {
        public readonly string name;
        public readonly string assertionId;
        public readonly string thenChild;

        public UntilEntity(
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
