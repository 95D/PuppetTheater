using System.Collections.Generic;

namespace Viento.PuppetTheater.Serialization
{
    /// <summary>
    /// This class data entity class for SelectNode.
    /// </summary>
    public class SelectEntity
    {
        public readonly string name;
        public readonly List<string> children;

        public SelectEntity(string name, List<string> children)
        {
            this.name = name;
            this.children = children;
        }
    }
}
