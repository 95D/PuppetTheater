using Viento.PuppetTheater.Utility;

namespace Viento.PuppetTheater.Serialization
{
    /// <summary>
    /// This class data entity class for FocusNode.
    /// </summary>
    public class FocusEntity : DataClass<FocusEntity>
    {
        public readonly string name;
        public readonly string filterId;
        public readonly string priorityId;
        public readonly int count = 1;

        public FocusEntity(string name, string filterId, string priorityId, int count)
        {
            this.name = name;
            this.filterId = filterId;
            this.priorityId = priorityId;
            this.count = count;
        }
    }
}
