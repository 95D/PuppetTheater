namespace Viento.PuppetTheater.Serialization
{
    /// <summary>
    /// This class data entity class for IfNode
    /// </summary>
    public class IfEntity
    {
        public readonly string name;
        public readonly string conditionFuncName;
        public readonly string thenChild;

        public IfEntity(
            string name,
            string conditionFuncName,
            string thenChild
            )
        {
            this.name = name;
            this.conditionFuncName = conditionFuncName;
            this.thenChild = thenChild;
        }
    }
}
