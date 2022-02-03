using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace Viento.PuppetTheater.Serialization
{
    /// <summary>
    /// A [CustomCreationConverter] to convert [JsonObject] into [INodeEntity]
    /// </summary>
    public class NodeEntityConverter : CustomCreationConverter<INodeEntity>
    {
        private readonly NodeEntityCreator nodeEntityCreator = new NodeEntityCreator();

        public override INodeEntity Create(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(
            JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer)
        {
            // Load JObject from stream
            JObject jObject = JObject.Load(reader);

            // Create target object based on JObject
            var target = Create(objectType, jObject);

            // Populate the object properties
            serializer.Populate(jObject.CreateReader(), target);

            return target;
        }

        private INodeEntity Create(
            Type objectType, JObject jobject
        ) => nodeEntityCreator.Create((string)jobject.Property("type"));
    }
}