namespace Viento.PuppetTheater.Serialization
{
    /// <summary>
    /// This class data entity class for behavior node
    /// </summary>
    public interface INodeEntity
    {
        string nodeId { get; set; }
        string type { get; set; }
    }
}