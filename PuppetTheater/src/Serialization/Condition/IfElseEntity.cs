using Viento.PuppetTheater.Utility;

namespace Viento.PuppetTheater.Serialization
{
    /// <summary>
    /// This class data entity class for IfElseNode.
    /// </summary>
    public class IfElseEntity : DataClass<IfElseEntity>
    {
        public readonly string name;
        public readonly string assertionId;
        public readonly string thenChild;
        public readonly string elseChild;

        public IfElseEntity(
            string name,
            string assertionId,
            string thenChild,
            string elseChild
            )
        {
            this.name = name;
            this.assertionId = assertionId;
            this.thenChild = thenChild;
            this.elseChild = elseChild;
        }
    }
}
