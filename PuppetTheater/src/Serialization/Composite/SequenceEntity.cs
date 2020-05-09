using System.Collections.Generic;
using Viento.PuppetTheater.Utility;

namespace Viento.PuppetTheater.Serialization
{
    /// <summary>
    /// This class data entity class for SeqenceNode.
    /// </summary>
    public class SequenceEntity : DataClass<SequenceEntity>
    {
        public readonly string name;
        public readonly List<string> children;

        public SequenceEntity(string name, List<string> children)
        {
            this.name = name;
            this.children = children;
        }
    }
}
