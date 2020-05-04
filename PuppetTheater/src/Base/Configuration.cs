using Newtonsoft.Json;

namespace Viento.PuppetTheater.Base
{
    public class Configuration
    {
        public readonly bool isTimeLimit;
        public readonly float timeout;
        public readonly bool isHopLimit;
        public readonly int hopout;

        public static Configuration CreateInstance(string json)
        {
            return JsonConvert.DeserializeObject(json) as Configuration;
        }
    }
}
