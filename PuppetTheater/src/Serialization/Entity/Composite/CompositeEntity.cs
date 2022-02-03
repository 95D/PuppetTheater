using System.Collections.Generic;
namespace Viento.PuppetTheater.Serialization
{
    public class CompositeEntity : INodeEntity
    {
        public const string TYPE = "composite";

        public string permutate;
        public string category;
        public List<INodeEntity> children;
        public string nodeId { get; set; }
        public string type { get; set; }
    }
}