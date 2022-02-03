namespace Viento.PuppetTheater.Serialization
{
    /// <summary>
    /// This class data entity class for [IfElseNode]
    /// </summary>
    public class IfElseEntity : INodeEntity
    {
        public const string TYPE = "if-else";
        public string assertionId;
        public INodeEntity thenChild;
        public INodeEntity elseChild;
        public string nodeId { get; set; }
        public string type { get; set; }
    }
}