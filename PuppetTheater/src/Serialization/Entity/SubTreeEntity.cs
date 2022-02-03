namespace Viento.PuppetTheater.Serialization
{
    /// <summary>
    /// This class data entity class for referencing built-in sub behavior tree
    /// </summary>
    public class SubTreeEntity : INodeEntity
    {
        public const string TYPE = "sub-tree";
        public string refNodeId;
        public string nodeId { get; set; }
        public string type { get; set; }
    }
}
