using Viento.PuppetTheater.Utility;

namespace Viento.PuppetTheater.Serialization
{
    /// <summary>
    /// This class data entity class for IfElseNode.
    /// </summary>
    public class IfElseEntity : DataClass<IfElseEntity>
    {
        public readonly string name;
        public readonly string conditionFuncName;
        public readonly string thenChild;
        public readonly string elseChild;

        public IfElseEntity(
            string name,
            string conditionFuncName,
            string thenChild,
            string elseChild
            )
        {
            this.name = name;
            this.conditionFuncName = conditionFuncName;
            this.thenChild = thenChild;
            this.elseChild = elseChild;
        }
    }
}
