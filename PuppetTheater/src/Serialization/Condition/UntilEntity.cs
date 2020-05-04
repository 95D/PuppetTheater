namespace Viento.PuppetTheater.Serialization
{
    /// <summary>
    /// This class data entity class for UntilNode.
    /// </summary>
    public class UntilEntity
    {
        public readonly string name;
        public readonly string conditionFuncName;
        public readonly string thenChild;

        public UntilEntity(
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
